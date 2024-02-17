using System.Reflection;

namespace MiniTrello.Web.Extensions;

public static class SwaggerGenExtensions
{
    public static IServiceCollection AddCustomSwaggerGenConfiguration(this IServiceCollection services)
    {
        return services.AddSwaggerGen(c =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}