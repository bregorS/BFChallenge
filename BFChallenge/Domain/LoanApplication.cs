namespace BFChallenge.Domain;

public class LoanApplication
{
    public const string LoanStatusPending = "PENDING";
    public const string LoanStatusApproved = "APPROVED";
    public const string LoanStatusDeclined = "DECLINED";

    public LoanApplication(decimal loanAmount, decimal assetValue, int creditScore)
    {
        Id = Guid.NewGuid();
        LoanAmount = loanAmount;
        AssetValue = assetValue;
        CreditScore = creditScore;
        Status = LoanStatusPending;
    }

    public Guid Id { get; }
    public decimal LoanAmount { get; } 
    public decimal AssetValue { get; }
    public int CreditScore { get; }
    public string Status { get; private set; }
    public double LoanToValue => Convert.ToDouble(LoanAmount / AssetValue);
    public bool IsApproved => Status.Equals(LoanStatusPending, StringComparison.InvariantCultureIgnoreCase);
    public bool IsDeclined => Status.Equals(LoanStatusDeclined, StringComparison.InvariantCultureIgnoreCase);

    public void Approve()
    {
        // Not in the spec but it is usual to protect state changes
        if (Status != LoanStatusPending) 
            throw new ArgumentOutOfRangeException(nameof(Status), "Status must be pending before approval");

        Status = LoanStatusApproved;
    }

    public void Decline()
    {
        // Not in the spec but it is usual to protect state changes
        if (Status != LoanStatusPending)
            throw new ArgumentOutOfRangeException(nameof(Status), "Status must be pending before declining");

        Status = LoanStatusDeclined;
    }
}
