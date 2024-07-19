using BFChallenge.Domain;

namespace BFChallenge.Services;

public interface ILoanValidatorService
{
    (bool result, string errorMessage) Validate(LoanApplication application);
}