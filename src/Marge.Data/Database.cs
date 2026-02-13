using System.Web.Hosting;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Marge.Data;

public static class Database
{
    public static string RootPath { get; set; } = HostingEnvironment.MapPath("~/images/.cache");
    public static string DataSource { get; set; } = Path.Combine(RootPath, "marge.db");

    private static SqliteConnection CreateConnection()
    {
        using var conn = new SqliteConnection(
            $"Data Source={DataSource}"
        );

        return conn;
    }

    public static SqliteConnection Connection { get => CreateConnection(); }

    public static void Prerequisites()
    {
        Connection.Execute(@"
            CREATE TABLE IF NOT EXISTS AccessLogs (
                ID          INTEGER PRIMARY KEY,
                Path        TEXT NOT NULL,
                CreatedAt   INTEGER DEFAULT (strftime('%s', 'now'))
            );

            CREATE INDEX IF NOT EXISTS idx_path_created ON AccessLogs(Path, CreatedAt DESC);
        ");
    }
}
