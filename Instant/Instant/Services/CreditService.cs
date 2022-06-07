using Instant.Entities;
using Instant.Enums;

namespace Instant.Services;

public class CreditService
{
    private readonly CheckJudgingService _checkJudgingService;

    public CreditService(CheckJudgingService checkJudgingService)
    {
        _checkJudgingService = checkJudgingService;
    }
    
    public string GetCreditResult(Request request)
    {
        var isReallyJudged = _checkJudgingService.IsJudged(request.Name, request.Surname, request.Patronymic,
            request.PassportSeries, request.PassportNumber, request.PassportIssuer, request.PassportIssueDate,
            request.PassportRegInformation, request.Adult, request.Employment, request.HasOtherCredits);

        var message = "";
        if (!request.IsJudged && isReallyJudged)
        {
            request.IsJudged = isReallyJudged;
            message = "Не пытайтесь нас обмануть: наш сервис показал, что вы были судимы. ";
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
            < 80 => $"{message}Вам отказано в кредите, так как ваш кредитный балл равен {result}",
            >= 80 and < 84 =>
                $"{message}Вы можете получить кредит с процентной ставкой 30%, так как ваш кредитный балл равен {result}",
            >= 84 and < 88 =>
                $"{message}Вы можете получить кредит с процентной ставкой 26%, так как ваш кредитный балл равен {result}",
            >= 88 and < 92 =>
                $"{message}Вы можете получить кредит с процентной ставкой 22%, так как ваш кредитный балл равен {result}",
            >= 92 and < 96 =>
                $"{message}Вы можете получить кредит с процентной ставкой 19%, так как ваш кредитный балл равен {result}",
            >= 96 and < 100 =>
                $"{message}Вы можете получить кредит с процентной ставкой 15%, так как ваш кредитный балл равен {result}",
            100 => $"Вы можете получить кредит с процентной ставкой 12,5%, так как ваш кредитный балл равен {result}",
            _ => $"{message}Кредитный балл > 100"
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