using KF.Host.Domain;
using KF.Host.Persistence;
using KF.Host.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KF.Host.Features.Payments.ConfirmPayment;

public record ConfirmPaymentRequest(Guid PaymentId);

public class ConfirmPaymentEndpoint
{
    public static async Task<IResult> Handle(
        ConfirmPaymentRequest request,
        DataBaseContext dbContext,
        IOptions<SubscriptionSettings> subscriptionSettingsOptions)
    {
        var payment = await dbContext.Payments
            .Include(p => p.Account)
            .FirstOrDefaultAsync(p => p.Id == request.PaymentId);

        if (payment is null)
            return Results.NotFound("Payment not found");

        if (payment.Status != PaymentStatus.Pending)
            return Results.BadRequest("Payment already processed");

        payment.Complete(DateTime.UtcNow);

        var subscription = new Subscription
        {
            AccountId = payment.AccountId,
            ExpiresAt = DateTime.UtcNow + subscriptionSettingsOptions.Value.ExpiryTime,
            PaymentId = payment.Id
        };

        dbContext.Subscriptions.Add(subscription);
        await dbContext.SaveChangesAsync();

        return Results.Ok(new
        {
            Message = "Payment confirmed and subscription activated",
            SubscriptionId = subscription.Id,
            ExpiresAt = subscription.ExpiresAt
        });
    }
}