using System.Security.Cryptography;
using System.Text;
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

        using var hasher = SHA256.Create();
        var hashBytes = hasher.ComputeHash(
            Encoding.UTF8.GetBytes(context.Request.Url.PathAndQuery)
        );
        var hashHex = BitConverter.ToString(hashBytes)
            .Replace("-", "")
            .ToLower();

        if (File.Exists(HostingEnvironment.MapPath($"~/images/.cache/{hashHex}")))
        {
            context.Response.BinaryWrite(
                File.ReadAllBytes(
                    HostingEnvironment.MapPath($"~/images/.cache/{hashHex}")
                )
            );

            context.Response.End();
        }

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

        using var hashFile = File.Create(
            HostingEnvironment.MapPath(
                $"~/images/.cache/{hashHex}"
            )
        );

        image.Write(hashFile);

        context.Response.BinaryWrite(
            image.ToByteArray()
        );
    }
}
