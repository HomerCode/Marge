using System.Web;

namespace Marge;

public class Module : IHttpModule
{
    private static readonly List<string> _extensions = ["webp", "avif", "png", "jpeg", "jpg"];

    public void Dispose()
    {
    }

    public void Init(HttpApplication context)
    {
        Record.Manager.Init();

        context.BeginRequest += (sender, e) =>
        {
            var lastSegment = context.Request.Url.Segments.Last();
            if (!lastSegment.Contains(".") && _extensions.Contains(lastSegment.Split('.').Last())) return;

            context.Context.Items["file.extension"] = lastSegment.Split('.').Last();
            context.Context.Items["file.basename"] = lastSegment.Split('.').First();
        };
    }
}
