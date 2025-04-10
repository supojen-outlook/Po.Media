namespace Po.Media;

public record struct DeleteOption
{
    /// <summary>
    /// 存處路徑
    /// </summary>
    public string Directory { get; init; }

    /// <summary>
    /// 檔案名稱
    /// </summary>
    public string Name { get; init; }
}