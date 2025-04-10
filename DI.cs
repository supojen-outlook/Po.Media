using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Po.Media.Implementation;
using Po.Media.Validation;

namespace Po.Media;

public static class DI
{
    public static IServiceCollection AddMediaService(this IServiceCollection services, IConfiguration configuration)
    {

        // description - 
        services.Configure<S3Settings>(configuration.GetSection("S3"));

        // description - 
        services.AddScoped<AmazonS3Client>(provider =>
        {
            var opts = provider.GetService<IOptions<S3Settings>>()?.Value;
            if (opts is null)
                throw new NullReferenceException("Should set the s3 configuration!");
            return new AmazonS3Client(opts.AccessKey,opts.SecretKey, new AmazonS3Config()
            {
                ServiceURL = opts.Url
            });
        });

        // description - 
        services.AddFileValidator();

        // description - 
        services.AddTransient<IMediaService, MediaService>();
        
        return services;
    }
}