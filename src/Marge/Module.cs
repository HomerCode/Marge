using System.Web;

namespace Marge;

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

            var acceptableExtensions = Config.Extensions
                .Union(Config.Inclusions)
                .Except(Config.Exclusions);

            var isAcceptedExtension = acceptableExtensions.Any(ext => request.Path.EndsWith(ext));

            var hasQueryString = context.Request.QueryString.Count > 0;

            if (!isAcceptedExtension || !hasQueryString) return;

            var handler = new Handler();
            handler.ProcessRequest(context.Context);
        });
    }

    public void Init(HttpApplication context)
    {
        Register(context);
    }
}
