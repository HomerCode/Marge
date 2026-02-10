using System.Web.Hosting;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Marge.Record;

public static class Database
{
    private static SqliteConnection CreateConnection()
    {
        using var conn = new SqliteConnection(
            $"Data Source={HostingEnvironment.MapPath("~/images/.cache/marge.db")}"
        );

        return conn;
    }

    public static SqliteConnection Connection { get => CreateConnection(); }

    public static void Prerequisites()
    {
        Connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Files (
                ID              INTEGER PRIMARY KEY, 
                Path            TEXT NOT NULL UNIQUE,
                Extension       TEXT NOT NULL,
                Basename        TEXT NOT NULL,
                FileSize        INTEGER NOT NULL,
                HitCount        INTEGER DEFAULT 1,
                CreatedAt       DATE DEFAULT (strftime('%s', 'now')),
                LastAccessed    DATE DEFAULT (strftime('%s', 'now'))
            );

            CREATE INDEX IF NOT EXISTS idx_last_accessed ON Files(LastAccessed);
            CREATE INDEX IF NOT EXISTS idx_path ON Files(Path);
        ");
    }
}
