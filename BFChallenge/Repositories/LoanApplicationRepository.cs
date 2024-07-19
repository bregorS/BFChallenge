using BFChallenge.Domain;

namespace BFChallenge.Repositories;

/// <summary>
/// Would normally be some sort of database
/// </summary>
internal class LoanApplicationRepository : ILoanApplicationRepository
{
    private List<LoanApplication> _loanApplications = new();

    public void Save(LoanApplication loanApplication)
    {
        _loanApplications.Add(loanApplication);
    }

    public IList<LoanApplication> GetAll()
    {
        return _loanApplications;
    }
}
