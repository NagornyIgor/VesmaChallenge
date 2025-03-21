using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Vesma.Api.Configuration;
using Vesma.Core.Configuration;
using Vesma.Data;

namespace Vesma.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<VesmaDbContext>(options =>
            options.UseInMemoryDatabase("VesmaInMemoryDb"));

        builder.Services.ConfigureCore();

        builder.Services.AddControllers();

        builder.Services.AddOpenApi();

        var app = builder.Build();

        ApiConfigurationOptions apiConfiguration =
            builder.Configuration.GetConfigurationSection<ApiConfigurationOptions>();

        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.Servers = apiConfiguration.AppServers
                .Select(url => new ScalarServer(url)).ToList();
        });

        app.UseCors(config => config
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
