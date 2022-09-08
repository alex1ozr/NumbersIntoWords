using AErmilov.NumbersIntoWords.Api.Infrastructure.Filters;
using AErmilov.NumbersIntoWords.Services.Extensions;
using Microsoft.OpenApi.Models;

namespace AErmilov.NumbersIntoWords.Api;
public sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        ConfigurePipeline(app);

        app.Run();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddMvcOptions(opt =>
            {
                opt.Filters.Add<DefaultExceptionFilter>();
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" }));

        services.AddNumbersIntoWordsServices();
    }

    private static void ConfigurePipeline(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();
    }
}