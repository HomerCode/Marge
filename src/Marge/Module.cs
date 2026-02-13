using System.Web;

namespace Marge;

public class CoreRequestManagement : IDisposable
{
    public void Dispose() { }

    // public void 
}


public class Module : IHttpModule
{

    public void Dispose() { }

    public static void Register(HttpApplication context)
    {
        Data.Manager.Init();
        Cache.Manager.Init();

        context.BeginRequest += new EventHandler((sender, e) =>
        {
            var request = context.Request;

            var segmentDetails = request.Url.Segments
                .Where(segment => segment.Contains("."))
                .Select(segment => segment
                    .Split('.')
                    .Select((value, index) => new { value, index })
                    .Aggregate(new Dictionary<string, string> { }, (acc, item) =>
                    {
                        acc[
                            item.index switch
                            {
                                0 => "basename",
                                1 => "extension",
                                _ => ""
                            }
                        ] = item.value;

                        return acc;
                    })
                )
                .Last();

            var isAcceptedExtension = Config.Extensions
                .Union(Config.Inclusions)
                .Where(ext => !Config.Exclusions.Contains(ext))
                .Contains(segmentDetails["extension"]);

            var hasQueryString = context.Request.QueryString.Count > 0;

            if (!isAcceptedExtension || !hasQueryString) return;

            context.Context.Items["file"] = segmentDetails;

            var handler = new Handler();
            handler.ProcessRequest(context.Context);
            context.Response.End();
        });

        // _isInitialised = true;
    }

    public void Init(HttpApplication context)
    {
        // if (!_isInitialised) 
        Register(context);
    }
}
