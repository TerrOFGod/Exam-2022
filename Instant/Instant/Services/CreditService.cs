using Instant.Entities;
using Instant.Enums;
using Instant.Interfaces;

namespace Instant.Services;

public class CreditService
{
    private readonly ICriminalChecker _criminalChecker;

    public CreditService(ICriminalChecker criminalChecker)
    {
        _criminalChecker = criminalChecker;
    }
    
    public async Task<string> GetCreditResult(Request request)
    {
        var criminalStatus = await _criminalChecker.CheckAsync(request.IsJudged);

        var message = "";
        if (!criminalStatus.Succeeded)
        {
            request.IsJudged = criminalStatus.Succeeded;
            message = "Do not try to deceive us: our service showed that you were convicted.";
        }
        
        var result = GetScoredFromAdult(request.Adult, request.Amount, request.Deposit) +
                     GetScoredFromJudging(request.IsJudged) +
                     GetScoredFromEmployment(request.Employment, request.Adult) +
                     GetScoresFromPurpose(request.Purpose) +
                     GetScoresFromDeposit(request.Deposit, request.CarAge) +
                     GetScoresFromOtherCredits(request.HasOtherCredits, request.Purpose) +
                     GetScoresFromAmount(request.Amount);
        return result switch
        {
            < 80 => $"{message}You have been denied a loan because your credit score is {result}",
            >= 80 and < 84 =>
                $"{message}You can get a loan with an interest rate of 30% since your credit score is {result}",
            >= 84 and < 88 =>
                $"{message}You can get a loan with an interest rate of 26% since your credit score is {result}",
            >= 88 and < 92 =>
                $"{message}You can get a loan with an interest rate of 22% since your credit score is {result}",
            >= 92 and < 96 =>
                $"{message}You can get a loan with an interest rate of 19% since your credit score is {result}",
            >= 96 and < 100 =>
                $"{message}You can get a loan with an interest rate of 15% since your credit score is {result}",
            100 => $"You can get a loan with an interest rate of 12.5% since your credit score is {result}",
            _ => $"{message}Credit score > 100"
        };
    }

    private int GetScoredFromAdult(int adult, int amount, DepositEnum depositEnum) =>
        adult switch
        {
            >= 21 and <= 28 => amount switch
            {
                < 1000000 => 12,
                >= 1000000 and <= 3000000 => 9,
                _ => 0
            },
            >= 29 and <= 59 => 14,
            >= 60 and <= 72 => depositEnum == DepositEnum.None ? 0 : 8,
            _ => 0
        };

    private int GetScoredFromJudging(bool isReallyJudged) => isReallyJudged ? 0 : 15;

    private int GetScoredFromEmployment(EmploymentEnum employmentEnum, int adult)
    {
        
        return employmentEnum switch
        {
            EmploymentEnum.ContractLaborCodeRusFed => 14,
            EmploymentEnum.IndividualEntrepreneur => 12,
            EmploymentEnum.Freelancer => 8,
            EmploymentEnum.Retiree => adult < 70 ? 5 : 0,
            _ => 0
        };
    }

    private int GetScoresFromPurpose(PurposeEnum purposeEnum) =>
        purposeEnum switch
        {
            PurposeEnum.Consumer => 14,
            PurposeEnum.Realty => 8,
            PurposeEnum.Recrediting => 12,
            _ => 0
        };

    private int GetScoresFromDeposit(DepositEnum depositEnum, int carAge) =>
        depositEnum switch
        {
            DepositEnum.Retiree => 14,
            DepositEnum.Car => carAge <= 3 ? 8 : 3,
            DepositEnum.Guarantee => 12,
            _ => 0
        };

    private int GetScoresFromOtherCredits(bool otherCredits, PurposeEnum purposeEnum)
    {
        if (otherCredits) return 0;
        return purposeEnum == PurposeEnum.Recrediting ? 0 : 15;
    }

    private int GetScoresFromAmount(int amount) =>
        amount switch
        {
            >= 0 and < 1000000 => 12,
            >= 1000000 and < 5000000 => 14,
            >= 5000000 and < 10000000 => 8,
            _ => 0
        };
}