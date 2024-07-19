using BFChallenge.Domain;

namespace BFChallenge.Services;

public interface ILoanApplicationService
{
    void Save(LoanApplication loanApplication);
}