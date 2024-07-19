using BFChallenge.Domain;

namespace BFChallenge.Services;

internal class LoanValidatorService : ILoanValidatorService
{
    public (bool result, string errorMessage) Validate(LoanApplication application) 
    {
        if (application.LoanAmount > 1_000_000 || application.LoanAmount < 100_000)
            return (false, "Loan amount must between £100,000 and £1.5 million");

        return (true, string.Empty);
    }
}


