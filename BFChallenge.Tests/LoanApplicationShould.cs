using BFChallenge.Domain;
using FluentAssertions;

namespace BFChallenge.Tests;

[TestFixture]
public class LoanApplicationShould
{
    [Test]
    public void UpdateStatusToApproved()
    {
        var loan = new LoanApplication(100, 50, 100);
        loan.Approve();

        loan.Status.Should().Be(LoanApplication.LoanStatusApproved);
    }

    [Test]
    public void ThrowAnExceptionWhenApprovingADeclinedLoan()
    {
        var loan = new LoanApplication(100, 50, 100);
        loan.Decline();

        Action approve = () => loan.Approve();
        approve.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void UpdateStatusToDeclined()
    {
        var loan = new LoanApplication(100, 50, 100);
        loan.Decline();

        loan.Status.Should().Be(LoanApplication.LoanStatusDeclined);
    }

    [Test]
    public void ThrowAnExceptionWhenDecliningAnApprovedLoan()
    {
        var loan = new LoanApplication(100, 50, 100);
        loan.Approve();

        Action decline = () => loan.Decline();
        decline.Should().Throw<ArgumentOutOfRangeException>();
    }
}