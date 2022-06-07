using System.ComponentModel.DataAnnotations;
using Instant.Enums;

namespace Instant.Entities;

public class Request
{
    [MinLength(Constants.Constants.NameMinLength)]
    [MaxLength(Constants.Constants.NameMaxLength)]
    public string Name { get; set; }
    
    [MinLength(Constants.Constants.SurnameMinLength)]
    [MaxLength(Constants.Constants.SurnameMaxLength)]
    public string Surname { get; set; }
    
    [MinLength(Constants.Constants.PatronymicMinLength)]
    [MaxLength(Constants.Constants.PatronymicMaxLength)]
    public string Patronymic { get; set; }
    
    [MinLength(Constants.Constants.PassportSeriesLength)]
    [MaxLength(Constants.Constants.PassportSeriesLength)]
    public string PassportSeries { get; set; }
    
    [MinLength(Constants.Constants.PassportNumberLength)]
    [MaxLength(Constants.Constants.PassportNumberLength)]
    public string PassportNumber { get; set; }
    
    [MinLength(Constants.Constants.PassportIssuerMinLength)]
    [MaxLength(Constants.Constants.PassportIssuerMaxLength)]
    public string PassportIssuer { get; set; }
    
    public DateTime PassportIssueDate { get; set; }
    
    [MinLength(Constants.Constants.PassportRegInformationMinLength)]
    [MaxLength(Constants.Constants.PassportRegInformationMaxLength)]
    public string PassportRegInformation { get; set; }
    
    [Range(Constants.Constants.AdultMin, Constants.Constants.AdultMax)]
    public int Adult { get; set; }
    
    public bool IsJudged { get; set; }
    
    public EmploymentEnum Employment { get; set; }
    
    public PurposeEnum Purpose { get; set; }
    
    public DepositEnum Deposit { get; set; }
    
    [Range(Constants.Constants.CarAgeMin, Constants.Constants.CarAgeMax)]
    public int CarAge { get; set; }
    
    public bool HasOtherCredits { get; set; }
    
    [Range(Constants.Constants.AmountMin, Constants.Constants.AmountMax)]
    public int Amount { get; set; }
}