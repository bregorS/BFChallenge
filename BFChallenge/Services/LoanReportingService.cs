using System.Text;
using BFChallenge.Domain;
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
        var reportOutput = new StringBuilder();

        var applications = this.repository.GetAll();

        reportOutput.AppendLine($"Total loan applications to date:{applications.Count}");
        reportOutput.AppendLine($"Total approved loan applications to date:{applications.Count(l => l.Status.Equals(LoanApplication.LoanStatusApproved, StringComparison.InvariantCultureIgnoreCase))}");
        reportOutput.AppendLine($"Total declined loan applications to date:{applications.Count(l => l.Status.Equals(LoanApplication.LoanStatusDeclined, StringComparison.InvariantCultureIgnoreCase))}");
        reportOutput.AppendLine($"Total value of loans written to date:{applications.Where(l => l.Status.Equals(LoanApplication.LoanStatusApproved, StringComparison.InvariantCultureIgnoreCase)).Sum(l=> l.LoanAmount)}");
        reportOutput.AppendLine($"Mean average Loan to Value of all application to date:{applications.Average(l=> l.LoanToValue)}");

        return reportOutput.ToString(); 
    }
}
