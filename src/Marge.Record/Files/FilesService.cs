using System.Diagnostics;
using Dapper;
using Marge.Record.Files;

namespace Marge.Record;

public class FileService
{
    public List<FileEntity> GetFiles() =>
        [.. Database.Connection.Query<FileEntity>("SELECT * FROM Files")];

    public FileEntity GetFile(string path) =>
        Database.Connection.QueryFirst<FileEntity>("SELECT * FROM Files WHERE Path = @Path", new { Path = path });

    public FileEntity AddFile(string path, string extension, string basename, int fileSize)
    {
        Database.Connection.Execute(@"
            INSERT INTO Files 
                (Path, Extension, Basename, FileSize) 
            VALUES 
                (@path, @extension, @basename, @fileSize)
            ",
            new { path, extension, basename, fileSize }
        );

        return GetFile(path);
    }

    public FileEntity RequestFile(string path)
    {
        var entity = Database.Connection.QueryFirst<FileEntity>("SELECT * FROM Files WHERE Path = @Path", new { Path = path });

        entity.HitCount++;

        Database.Connection.Execute(@"
            UPDATE Files SET HitCount = @hitCount WHERE Path = @path 
        ", new { hitCount = entity.HitCount, path });

        return entity;
    }
}