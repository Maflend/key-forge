using System.Diagnostics.CodeAnalysis;
using Scalar.AspNetCore;

namespace KF.Host.RestApiConfigurations;

public class ScalarCustomConfiguration
{
    [StringSyntax("css")] private const string Css =
        """
        .scalar-api-reference {
           background: linear-gradient(rgb(0 0 0 / 0%), rgb(0 0 0 / 0%)),
            url('/images/arcane.jpg') center/cover fixed !important;
            min-height: 100vh;
        }
        .scalar-api-reference {
            --scalar-background-1: #00000082;
        }
        .scalar-app .bg-sidebar-b-1 {
             background-color: #000000d1;
        }
        """;

    public static void Configure(ScalarOptions options)
    {
        options.Title = "KeyForge";
        options.Theme = ScalarTheme.Kepler;
        options.ExpandAllTags()
            .SortTagsAlphabetically()
            .SortOperationsByMethod()
            .HideDarkModeToggle()
            .HideClientButton()
            .WithCustomCss(Css);
    }
}