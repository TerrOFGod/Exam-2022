namespace LoansIssuance.Constants;

public static class Constants
{
    public const int NameMinLength = 1;
    public const int NameMaxLength = 100;

    public const int SurnameMinLength = 1;
    public const int SurnameMaxLength = 100;

    public const int PatronymicMinLength = 0;
    public const int PatronymicMaxLength = 100;

    public const int PassportSeriesLength = 4;
    public const int PassportNumberLength = 6;
    public const int PassportIssuerMinLength = 1;
    public const int PassportIssuerMaxLength = 100;
    public const int PassportRegInformationMinLength = 1;
    public const int PassportRegInformationMaxLength = 100;

    public const int AdultMin = 21;
    public const int AdultMax = 72;

    public const int CarAgeMin = 0;
    public const int CarAgeMax = 100;

    public const int AmountMin = 1;
    public const int AmountMax = 10000000;
}