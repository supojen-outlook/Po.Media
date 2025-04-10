namespace Po.Media;

public record struct UploadOption
{
    public UploadOption(MediaType type, string? directory = null, string? name = null)
    {
        Type = type;
        Directory = directory ?? "default";
        Name = string.IsNullOrWhiteSpace(name) ? Guid.NewGuid().ToString() : name;
    }
    
    /// <summary>
    /// 存處路徑
    /// </summary>
    public string Directory { get; init; }

    /// <summary>
    /// 檔案名稱
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// 檔案種類
    /// </summary>
    public MediaType Type { get; init; }
}