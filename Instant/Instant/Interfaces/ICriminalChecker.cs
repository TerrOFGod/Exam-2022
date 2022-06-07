using Instant.Entities;
using Instant.Enums;

namespace Instant.Interfaces;

public interface ICriminalChecker
{
    Task<CriminalStatus> CheckAsync(bool isJugged);
}