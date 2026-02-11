namespace Marge.Cache;

public static class Manager
{
    private static Timer? _timer;

    public static void Init()
    {
        _timer ??= new Timer(
            state =>
            {
                Console.WriteLine("[MARGE]: Cache System Check!");

                var service = new Record.FileService();

                var files = service.GetFiles();
                Console.WriteLine(files.Count());
            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(10)
        );
    }
}
