using BFChallenge.Domain;

namespace BFChallenge.Repositories;

public interface ILoanApplicationRepository
{
    IList<LoanApplication> GetAll();
    void Save(LoanApplication loanApplication);
}