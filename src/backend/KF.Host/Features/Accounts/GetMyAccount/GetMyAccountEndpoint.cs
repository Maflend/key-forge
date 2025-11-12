using KF.Host.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KF.Host.Features.Accounts.GetMyAccount;

public class GetMyAccountEndpoint
{
    public static async Task<IResult> Handle(
        Guid id,
        DataBaseContext dbContext)
    {
        var account = await dbContext.Accounts
            .Include(ac => ac.TimeSubscriptions).ThenInclude(ts => ts.Key)
            .FirstOrDefaultAsync(ac => ac.Id == id);

        if (account is null)
            return Results.NotFound();

        return Results.Ok(account);
    }
}