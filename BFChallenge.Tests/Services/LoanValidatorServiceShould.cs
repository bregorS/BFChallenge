using BFChallenge.Domain;
using BFChallenge.Services;
using FluentAssertions;

namespace BFChallenge.Tests.Services;

[TestFixture]
public class LoanValidatorServiceShould
{
    // Could also check the error message below is as expected just in case there are any mistakes

    [TestCase(1_500_001, false)]
    [TestCase(1_000_000, true)]
    [TestCase(100_000, true)]
    [TestCase(99_999, false)]
    public void ReturnExpectedResultForLoanValue(decimal loanValue, bool isValid)
    {
        var sut = new LoanValidatorService();

        var loanApplication = new LoanApplication(loanValue, loanValue * 2, 950);

        var result = sut.Validate(loanApplication);
        result.result.Should().Be(isValid);
    }

    [TestCase(1_700_000, 950, true)]
    [TestCase(1_700_000, 949, false)]
    [TestCase(1_650_000, 950, false)]
    [TestCase(1_650_000, 950, false)]
    [TestCase(1_666_667, 950, true)]
    public void ReturnExpectedResultForLoanValueOverOneMillion(decimal ltv, int creditScore, bool isValid)
    {
        var sut = new LoanValidatorService();

        var loanApplication = new LoanApplication(1_000_000, ltv, creditScore);

        var result = sut.Validate(loanApplication);
        result.result.Should().Be(isValid);
    }


    [TestCase(1_700_000, 750, true)]
    [TestCase(1_700_000, 751, true)]
    [TestCase(1_700_000, 749, false)]
    [TestCase(1_250_000, 800, true)]
    [TestCase(1_250_000, 801, true)]
    [TestCase(1_250_000, 799, false)]
    [TestCase(1_120_000, 900, true)]
    [TestCase(1_120_000, 901, true)]
    [TestCase(1_120_000, 899, false)]
    [TestCase(1_110_000, 900, false)]
    public void ReturnExpectedResultForLoanLessThanOneMillion(decimal ltv, int creditScore, bool isValid)
    {
        var sut = new LoanValidatorService();

        var loanApplication = new LoanApplication(999_999, ltv, creditScore);

        var result = sut.Validate(loanApplication);
        result.result.Should().Be(isValid);
    }
}
