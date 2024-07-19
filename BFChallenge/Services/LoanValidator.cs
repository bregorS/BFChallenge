using BFChallenge.Domain;
using FluentValidation;

namespace BFChallenge.Services;

public class LoanValidatorService : ILoanValidatorService
{
    private readonly LoanApplicationValidator _validator = new();

    public (bool result, string errorMessage) Validate(LoanApplication application)
    {
        var result = _validator.Validate(application);

        return (result.IsValid, result.IsValid ? string.Empty : string.Join("\n", result.Errors.Select(e => e.ErrorMessage)));
    }
}

internal class LoanApplicationValidator : AbstractValidator<LoanApplication>
{
    public LoanApplicationValidator()
    {
        RuleFor(l => l.LoanAmount)
            .InclusiveBetween(100_000, 1_500_000)
            .WithMessage("Loan amount must between £100,000 and £1.5 million");

        RuleFor(l => l)
            .Must(l => l.LoanToValue <= 0.6 && l.CreditScore >= 950)
            .When(l => l.LoanAmount >= 1_000_000)
            .WithMessage("For loans of £1 million+ LTV must be 60% or less and the credit score 950+");

        Include(new LoansUnderAMillionValidator());
    }
}

internal class LoansUnderAMillionValidator : AbstractValidator<LoanApplication>
{
    public LoansUnderAMillionValidator()
    {
        RuleFor(l => l.CreditScore)
            .GreaterThanOrEqualTo(750)
            .WithMessage("Credit score must be 750 for loan of less than £1m and LTV < 60%")
            .When(l => l.LoanAmount < 1_000_000)
            .When(l => l.LoanToValue < 0.6);

        RuleFor(l => l.CreditScore)
            .GreaterThanOrEqualTo(800)
            .WithMessage("Credit score must be 800 for loan of less than £1m and LTV < 80%")
            .When(l => l.LoanAmount < 1_000_000)
            .When(l => l.LoanToValue >= 0.6 && l.LoanToValue <= 0.8);

        RuleFor(l => l.CreditScore)
            .GreaterThanOrEqualTo(900)
            .WithMessage("Credit score must be 900 for loan of less than £1m and LTV < 90%")
            .When(l => l.LoanAmount < 1_000_000)
            .When(l => l.LoanToValue >= 0.8 && l.LoanToValue <= 0.9);

        RuleFor(l => l.LoanToValue)
            .LessThan(.9)
            .WithMessage("LTV must be less than 90% for loans of less than £1m")
            .When(l => l.LoanAmount < 1_000_000);
    }
}

