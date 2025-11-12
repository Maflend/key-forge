using System.ComponentModel;
using KF.Host.Domain;
using KF.Host.Persistence;

namespace KF.Host.Features.Payments.CreatePayment;

[Description("Данные для проведения платежа")]
public record CreatePaymentRequest(
    [property: Description("Сумма платежа")]
    decimal Amount,
    [property: Description("Способ оплаты")]
    Currency Currency,
    [property: Description("Источник платежа")]
    [property: DefaultValue("TELEGRAM")]
    string Provider = "TELEGRAM");

public class CreatePaymentEndpoint
{
    public static async Task<IResult> Handle(
        Guid accountId,
        CreatePaymentRequest request,
        DataBaseContext dbContext)
    {
        var account = await dbContext.Accounts.FindAsync(accountId);
        if (account is null)
            return Results.NotFound("Account not found");

        var payment = Payment.New(accountId, new Money(request.Currency, request.Amount), request.Provider, DateTime.UtcNow);

        dbContext.Payments.Add(payment);
        await dbContext.SaveChangesAsync();

        return Results.Ok(new
        {
            payment.Id,
            Amount = payment.Money.Value,
            payment.Money.Currency,
            payment.Provider,
            payment.Status,
            payment.CreatedAt
        });
    }
}