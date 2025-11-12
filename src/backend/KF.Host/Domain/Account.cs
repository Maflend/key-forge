namespace KF.Host.Domain;

public class Account
{
    public Guid Id { get; set; }
    public string TelegramId { get; set; }
    public decimal Balance { get; set; }
    
    public List<Subscription> TimeSubscriptions { get; set; } = [];
}