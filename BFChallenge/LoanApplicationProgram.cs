using BFChallenge.Domain;
using BFChallenge.Repositories;
using BFChallenge.Services;

namespace BFChallenge;

internal class LoanApplicationProgram
{
    private ILoanValidatorService _loanValidatorService;
    private ILoanApplicationService _loanService;
    private ILoanReportingService _loanReportingService;

    public LoanApplicationProgram()
    {
        // Set up dependencies.  Would normally be registered and then supplied via DI and IoC container
        var loadRepo = new LoanApplicationRepository(); // shared in-memory repo
        _loanValidatorService = new LoanValidatorService();
        _loanService = new LoanApplicationService(loadRepo);
        _loanReportingService = new LoanReportingService(loadRepo);
    }

    public void Run()
    {
        Console.WriteLine("Welcome to BF Loan Application");
        Console.WriteLine("Press CTRL-C to exit");

        while (true)
        {
            decimal? loanAmount = null;
            while (!loanAmount.HasValue || (loanAmount.HasValue && loanAmount.Value <= 0))
            {
                loanAmount = InputHelper.GetDecimalInput("Please enter the loan amount in GBP");
            }

            decimal? assetValue = null;
            while (!assetValue.HasValue || (assetValue.HasValue && assetValue.Value <= 0))
            {
                assetValue = InputHelper.GetDecimalInput("Please enter the value of asset on which the loan is secured in GBP");
            }

            int? creditScore = null;
            while (!creditScore.HasValue || (creditScore.HasValue && (creditScore < 1 || creditScore > 999)))
            {
                creditScore = InputHelper.GetIntegerInput("Please enter applicant's credit score in the range 1 - 999");
            }

            var loanApplication = new LoanApplication(loanAmount.Value, assetValue.Value, creditScore.Value);
            var (result, errorMessage) = _loanValidatorService.Validate(loanApplication);

            if (result)
            {
                loanApplication.Approve();
                Console.WriteLine("Congratulations.  Your loan application was successful.");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                loanApplication.Decline();
                Console.WriteLine("Sorry.  Your loan application was declined.");
                Console.WriteLine(errorMessage);
                Console.WriteLine();
                Console.WriteLine();
            }

            _loanService.Save(loanApplication);
            Console.WriteLine(_loanReportingService.GenerateLoanSummaryReport());
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
