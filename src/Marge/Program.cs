using System.Web;

[assembly: PreApplicationStartMethod(typeof(Marge.Program), "Entry")]

namespace Marge;

public static class Program
{
    public static void Entry()
    {
        Console.WriteLine("[MARGE]: Registering Modules");
        HttpApplication.RegisterModule(
            typeof(Marge.Module)
        );
    }
}