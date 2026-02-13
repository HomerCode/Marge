namespace Marge.Data;

public class Manager
{
    public static void Init(string? rootPath = null)
    {
        if (rootPath != null)
            Database.RootPath = rootPath;

        Database.Prerequisites();
    }
}