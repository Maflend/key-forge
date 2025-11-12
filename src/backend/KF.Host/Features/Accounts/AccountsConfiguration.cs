using KF.Host.Features.Accounts.GetMyAccount;

namespace KF.Host.Features.Accounts;

public static class AccountsConfiguration
{
    extension(IEndpointRouteBuilder builder)
    {
        public IEndpointRouteBuilder MapAccountsEndpoints()
        {
            var group = builder.MapGroup("api/accounts")
                .WithTags("Аккаунты");

            group.MapGet("me", GetMyAccountEndpoint.Handle)
                .WithSummary("Получить мой аккаунт");

            return builder;
        }
    }
}