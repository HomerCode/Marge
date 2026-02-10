using System.Web;
using System.Web.Hosting;
using ImageMagick;

namespace Marge.Optimiser;

public class ImageOptimisationHandler : IHttpHandler
{
    public bool IsReusable => false;

    public void ProcessRequest(HttpContext context)
    {
        var query = new Query(context.Request.QueryString);

        using var image = new MagickImage(
            HostingEnvironment.MapPath(context.Request.Path)
        )
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
    }
}
