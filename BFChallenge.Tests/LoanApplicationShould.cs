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
    public void UpdateStatusToDeclinedd()
    {
        var loan = new LoanApplication(100, 50, 100);
        loan.Decline();

        loan.Status.Should().Be(LoanApplication.LoanStatusDeclined);
    }
}