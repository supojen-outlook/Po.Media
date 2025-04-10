using Microsoft.Extensions.DependencyInjection;
using Po.Media.Validation;

namespace Po.Media;

/// <summary>
/// File Validator Factory Method
/// </summary>
internal delegate FileValidatorBase FileValidatorFactory(MediaType type);

/// <summary>
/// Dependency Injection
/// </summary>
public static class DI
{
    public static IServiceCollection AddFileValidator(this IServiceCollection services)
    {
        services.AddTransient<FileValidatorFactory>((_) => (type) =>
        {
            switch (type)
            {
                case MediaType.mp3:
                    return new Mp3Validator();
                case MediaType.mp4:
                    return new Mp4Validator();
                case MediaType.image:
                    return new ImageValidator();
                case MediaType.pdf:
                    return new PdfValidator();
                default:
                    throw new NotImplementedException();
            }
        });
        
        
        return services;
    }
}