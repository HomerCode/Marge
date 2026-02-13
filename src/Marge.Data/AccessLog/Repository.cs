using System.Diagnostics;
using Dapper;

namespace Marge.Data.AccessLog;

public class AccessLogRepository
{
    public static List<AccessLog> GetAll(string? path = null) => [..
        Database.Connection.Query<AccessLog>(
            string.IsNullOrEmpty(path) ?
                "SELECT * FROM AccessLogs" :
                "SELECT * FROM AccessLogs WHERE Path = @path",
            new { path }
        )
    ];

    public static bool Add(string name)
    {
        var numberOfRowsAffected = Database.Connection.Execute("INSERT INTO AccessLogs (Path) VALUES (@name)", new { name });

        return numberOfRowsAffected > 0;
    }
}