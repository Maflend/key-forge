namespace KF.Host.Domain;

public record Payment
{
    public Guid Id { get; private set; }

    public Guid AccountId { get; private set; }
    public Account Account { get; private set; }

    public Money Money { get; private set; }
    public string Provider { get; private set; }

    public PaymentStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public Guid? SubscriptionId { get; private set; }
    public Subscription? Subscription { get; private set; }

    public void Complete(DateTime completedAt)
    {
        Status = PaymentStatus.Completed;
        CompletedAt = completedAt;
    }

    public static Payment New(Guid accountId, Money money, string provider, DateTime createdAt)
    {
        return new Payment
        {
            AccountId = accountId,
            Money = money,
            Provider = provider,
            Status = PaymentStatus.Pending,
            CreatedAt = createdAt
        };
    }
}