using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace KF.Host.RestApiConfigurations;

public class OpenApiConfiguration
{
    public static void Configure(OpenApiOptions options)
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            document.Info = new OpenApiInfo
            {
                Title = "KeyForge API",
                Version = "v1"
            };
            return Task.CompletedTask;
        });
    }
}