using System.ComponentModel.DataAnnotations;
using LoansIssuance.Enums;

namespace Instant.Dto;

public class Request
{
    [MinLength(LoansIssuance.Constants.Constants.NameMinLength)]
    [MaxLength(LoansIssuance.Constants.Constants.NameMaxLength)]
    public string Name { get; set; }
    
    [MinLength(LoansIssuance.Constants.Constants.SurnameMinLength)]
    [MaxLength(LoansIssuance.Constants.Constants.SurnameMaxLength)]
    public string Surname { get; set; }
    
    [MinLength(LoansIssuance.Constants.Constants.PatronymicMinLength)]
    [MaxLength(LoansIssuance.Constants.Constants.PatronymicMaxLength)]
    public string Patronymic { get; set; }
    
    [MinLength(LoansIssuance.Constants.Constants.PassportSeriesLength)]
    [MaxLength(LoansIssuance.Constants.Constants.PassportSeriesLength)]
    public string PassportSeries { get; set; }
    
    [MinLength(LoansIssuance.Constants.Constants.PassportNumberLength)]
    [MaxLength(LoansIssuance.Constants.Constants.PassportNumberLength)]
    public string PassportNumber { get; set; }
    
    [MinLength(LoansIssuance.Constants.Constants.PassportIssuerMinLength)]
    [MaxLength(LoansIssuance.Constants.Constants.PassportIssuerMaxLength)]
    public string PassportIssuer { get; set; }
    
    public DateTime PassportIssueDate { get; set; }
    
    [MinLength(LoansIssuance.Constants.Constants.PassportRegInformationMinLength)]
    [MaxLength(LoansIssuance.Constants.Constants.PassportRegInformationMaxLength)]
    public string PassportRegInformation { get; set; }
    
    [Range(LoansIssuance.Constants.Constants.AdultMin, LoansIssuance.Constants.Constants.AdultMax)]
    public int Adult { get; set; }
    
    public bool IsJudged { get; set; }
    
    public EmploymentEnum Employment { get; set; }
    
    public PurposeEnum Purpose { get; set; }
    
    public DepositEnum Deposit { get; set; }
    
    [Range(LoansIssuance.Constants.Constants.CarAgeMin, LoansIssuance.Constants.Constants.CarAgeMax)]
    public int CarAge { get; set; }
    
    public bool HasOtherCredits { get; set; }
    
    [Range(LoansIssuance.Constants.Constants.AmountMin, LoansIssuance.Constants.Constants.AmountMax)]
    public int Amount { get; set; }
}