using System.Web;
using System.Web.Hosting;
using ImageMagick;
using Marge.Data.AccessLog;
using Marge.Optimiser;

namespace Marge;

public class Handler : IHttpHandler
{
    public bool IsReusable => false;

    public void ProcessRequest(HttpContext context)
    {
        var query = new Query(context.Request.QueryString);

        var filename = Hasher.Utility.Convert(context.Request.Url.PathAndQuery);

        var cachePath = HostingEnvironment.MapPath($"~/images/.cache/{filename}");
        if (File.Exists(cachePath))
        {
            context.Response.WriteFile(cachePath);
            context.Response.End();
        }

        using var image = new MagickImage(HostingEnvironment.MapPath(context.Request.Path))
        {
            Format = query.Format switch
            {
                "png" => MagickFormat.Png,
                "jpg" => MagickFormat.Jpg,
                "jxl" => MagickFormat.Jxl,
                "jpeg" => MagickFormat.Jpeg,
                "avif" => MagickFormat.Avif,
                "webp" => MagickFormat.WebP,
                _ => MagickFormat.WebP
            },
            Quality = query.Quality
        };

        image.Resize(query.Width ?? image.Width, query.Height ?? image.Height);

        using var cacheFile = File.Create(cachePath);

        image.Write(cacheFile);

        AccessLogRepository.Add(cachePath);

        context.Response.WriteFile(cachePath);
    }
}
