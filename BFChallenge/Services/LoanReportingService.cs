using BFChallenge.Repositories;

namespace BFChallenge.Services;

public class LoanReportingService : ILoanReportingService
{
    private readonly ILoanApplicationRepository repository;

    public LoanReportingService(ILoanApplicationRepository repository)
    {
        this.repository = repository;
    }

    public string GenerateLoanSummaryReport()
    {
        return string.Empty;
    }
}
