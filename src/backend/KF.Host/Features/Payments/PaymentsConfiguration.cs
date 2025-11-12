using KF.Host.Features.Payments.ConfirmPayment;
using KF.Host.Features.Payments.CreatePayment;

namespace KF.Host.Features.Payments;

public static class PaymentsConfiguration
{
    extension(IEndpointRouteBuilder builder)
    {
        public IEndpointRouteBuilder MapPaymentsEndpoints()
        {
            var group = builder.MapGroup("api/payments")
                .WithTags("Пополнение счёта");

            group.MapPost("/api/payments", CreatePaymentEndpoint.Handle)
                .WithSummary("Создать платеж");

            group.MapPost("/api/payments/confirm", ConfirmPaymentEndpoint.Handle)
                .WithSummary("Подтвердить платеж");

            return builder;
        }
    }
}