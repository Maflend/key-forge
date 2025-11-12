namespace KF.Host.Domain;

public class Subscription
{
    public Guid Id { get; set; }
    public DateTime ExpiresAt { get; set; }
    public Key Key { get; set; }
    public Guid KeyId { get; set; }
    public Guid AccountId { get; set; }
    public Guid PaymentId { get; set; }
}