using BFChallenge.Domain;
using BFChallenge.Repositories;

namespace BFChallenge.Services;

public class LoanApplicationService : ILoanApplicationService
{
    private readonly ILoanApplicationRepository repository;

    public LoanApplicationService(ILoanApplicationRepository repository)
    {
        this.repository = repository;
    }

    public void Save(LoanApplication loanApplication)
    {
        repository.Save(loanApplication);
    }
}
