namespace Po.Media;

public interface IMediaService
{
    /// <summary>
    ///  刪除資源
    /// </summary>
    /// <param name="url"></param>
    Task DeleteAsync(string url);

    /// <summary>
    /// 刪除資源
    /// </summary>
    /// <param name="options"></param>
    Task DeleteAsync(DeleteOption options);
    
    /// <summary>
    /// 上傳檔案
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    Task<string> UploadAsync(MemoryStream stream, UploadOption options);
}