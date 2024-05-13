using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auth;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add Cors
        builder.Services.AddCors(x =>
        {
            x.AddDefaultPolicy(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });
        });

        // Configure Database
        builder.Services.AddDbContext<AuthDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString(typeof(AuthDbContext).Name)))
                        .AddScoped<IDbContext>(x => x.GetService<AuthDbContext>()!);

        // Configure Identity
        builder.ConfigureIdentity();

        // Add Controllers
        builder.Services.AddControllersWithViews();

        // Add Mediator
        builder.Services.AddMediator(x =>
        {
            x.ServiceLifetime = ServiceLifetime.Scoped;
        });
    }

    public static void ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseHsts();

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseStaticFiles();

        app.UseIdentityServer();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
