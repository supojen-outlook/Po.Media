using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using Po.Media.Validation;

namespace Po.Media.Implementation;

internal class MediaService : IMediaService
{
    private readonly AmazonS3Client _client;
    private readonly FileValidatorFactory _fileValidatorFactory;
    private readonly S3Settings _config;

    public MediaService(
        AmazonS3Client client,
        FileValidatorFactory fileValidatorFactory,
        IOptions<S3Settings> config)
    {
        _client = client;
        _fileValidatorFactory = fileValidatorFactory;
        _config = config.Value;
    }

    /// <summary>
    ///  刪除資源
    /// </summary>
    /// <param name="url"></param>
    public async Task DeleteAsync(string url)
    {
        var elements = url.Split('/');
 
        var request = new DeleteObjectRequest()
        {
            BucketName = elements[^2],
            Key = elements[^1]
        };

        await _client.DeleteObjectAsync(request);
    }
    
    /// <summary>
    /// 刪除資源
    /// </summary>
    /// <param name="options"></param>
    public async Task DeleteAsync(DeleteOption options)
    {
        var request = new DeleteObjectRequest()
        {
            BucketName = options.Directory,
            Key = options.Name
        };

        await _client.DeleteObjectAsync(request);
    }
    
    
    /// <summary>
    /// 上傳檔案
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<string> UploadAsync(MemoryStream stream, UploadOption options)
    {
        // processing - 取得 File Validator
        var validator = _fileValidatorFactory.Invoke(options.Type);
        
        // processing - 驗證檔案格式是否正確
        if (validator.Validate(stream) is false)
            throw new Exception("Wrong File Format");
        
        // processing - 上傳檔案
        var request = new TransferUtilityUploadRequest
        {
            BucketName = options.Directory,
            Key = options.Name,
            InputStream = stream,
            StorageClass = S3StorageClass.StandardInfrequentAccess,
            CannedACL = S3CannedACL.PublicRead
        };
        var fileTransferUtility = new TransferUtility(_client);
        await fileTransferUtility.UploadAsync(request);

        // return - url
        return $"{_config.CDN}/{options.Directory}/{options.Name}";
    }
    
}