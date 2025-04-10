namespace Po.Media.Implementation;

internal class S3Settings
{
    /// <summary>
    /// S3 Access Key
    /// </summary>
    public required string AccessKey { get; set; }

    /// <summary>
    /// S3 Secret Key
    /// </summary>
    public required string SecretKey { get; set; }

    /// <summary>
    /// S3 Url
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    /// CDN
    /// </summary>
    public required string CDN { get; set; }
}