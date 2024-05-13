using Infrastructure;

namespace API;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Configure Infrstructure
        builder.ConfigureInfrstructure();

        // Configure Identity Server
        builder.ConfigureIdentityServer();
    }

    public static void ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseHsts();

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
