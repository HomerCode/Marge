using System.Web;

namespace Marge;

public class Module : IHttpModule
{
    public void Dispose()
    {
    }

    public void Init(HttpApplication context)
    {
        Record.Manager.Init();

        context.BeginRequest += (object sender, EventArgs e) =>
        {
            var lastSegment = context.Request.Url.Segments.Last();
            if (!lastSegment.Contains(".")) return;

            context.Context.Items["file.extension"] = lastSegment.Split('.').Last();
            context.Context.Items["file.basename"] = lastSegment.Split('.').First();
        };
    }
}
