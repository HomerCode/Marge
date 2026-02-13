using Dapper;
using Marge.Data;
using Marge.Data.AccessLog;

namespace Marge.Cache;

public static class DataMaintenanceManager
{
    public static Timer? _timer;

    public static void Start()
    {
        _timer ??= new Timer(
            state =>
            {
                Database.Connection.Execute(@"
                    DELETE FROM AccessLogs
                    WHERE ID IN (
                        SELECT ID
                        FROM (
                            SELECT ID, 
                                ROW_NUMBER() OVER (PARTITION BY Path ORDER BY CreatedAt DESC) as RowNum
                            FROM AccessLogs
                        )
                        WHERE RowNum > 7
                    );
                ");
            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(10)
        );
    }
}

public static class FileMaintenanceManager
{
    private static Timer? _timer;

    public static void Start()
    {
        _timer ??= new Timer(
            state =>
            {
                Console.WriteLine("[MARGE]: Cache System Check!");
            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(10)
        );
    }

    public static void Stop()
    {
        _timer?.Dispose();
    }
}

public static class Manager
{
    public static void Init()
    {

    }

    public static void Clear()
    {

    }
}
