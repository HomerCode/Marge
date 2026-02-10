namespace Marge.Record.Files;

public record FileEntity
{
    public int ID { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public string Basename { get; set; }
    public int FileSize { get; set; }
    public int HitCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastAccessed { get; set; }
}