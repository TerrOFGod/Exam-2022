using LoansIssuance.Enums;

namespace LoansIssuance.Services;

public class CheckJudgingService
{
    public bool IsJudged(string name,
        string surname,
        string patronymic,
        string passportSeries,
        string passportNumber,
        string passportIssuer,
        DateTime passportIssueDate, 
        string passportRegInformation,
        int adult, 
        EmploymentEnum employmentEnum,
        bool hasOtherCredits)
    {
        var random = new Random();
        return random.Next(0, 100) < 30;
    }
}