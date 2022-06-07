using Instant.Entities;
using Instant.Enums;
using Instant.Interfaces;

namespace Instant.Services;

public class CriminalChecker: ICriminalChecker
{
    public async Task<CriminalStatus> CheckAsync(bool isJugged)
    {
        var number = Random.Shared.Next(1);
        
        var criminalStatus = new CriminalStatus
        {
            Succeeded = true,
            Errors = new List<string>()
        };
        
        switch (number)
        {
            case 0:
                criminalStatus.Succeeded = false;
                criminalStatus.Errors.Add("Passport error: passport was not found");
                break;
            case 1 when isJugged:
                criminalStatus.Succeeded = true;
                criminalStatus.Errors.Add("Certificate error: certificate was not found");
                break;
        }

        return await Task.FromResult(criminalStatus);
    }
}