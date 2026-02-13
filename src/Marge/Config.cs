using System.Web.Configuration;
using System.Web.Hosting;

namespace Marge;

public static class Config
{
    public static string CachePath =>
        HostingEnvironment.MapPath(
            WebConfigurationManager.AppSettings["Marge.Path"] ?? "~/images/.cache"
        );

    public static List<string> Inclusions => [..
        (WebConfigurationManager.AppSettings.Get("Marge.Extensions.Inclusions") ?? string.Empty)
            .Split(',')
    ];

    public static List<string> Exclusions => [..
        (WebConfigurationManager.AppSettings.Get("Marge.Extensions.Exclusions") ?? string.Empty)
            .Split(',')
    ];

    public static List<string> Extensions => [..
        (WebConfigurationManager.AppSettings.Get("Marge.Extensions") ?? "webp,avif,png,jpeg,jpg")
            .Split(',')
    ];
}