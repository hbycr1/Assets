using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureInfrastructure
{
	public static void ConfigureInfrstructure(this WebApplicationBuilder builder)
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
		builder.Services.AddDbContext<APIDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString(typeof(APIDbContext).Name)))
						.AddScoped<IDbContext>(x => x.GetService<APIDbContext>()!);

		// Add Controllers
		builder.Services.AddControllers()
						.AddNewtonsoftJson(o => o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

		// Add Mediator
		builder.Services.AddMediator(x =>
		{
			x.ServiceLifetime = ServiceLifetime.Scoped;
		});
	}

	public static void ConfigureIdentityServer(this WebApplicationBuilder builder)
	{
		// Add Identity Auth
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
						.AddJwtBearer(options =>
						{
							options.RequireHttpsMetadata = true;
							options.Audience = builder.Configuration["Clients:API:Id"];
							options.Authority = builder.Configuration["Clients:API:Authority"];
							options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
						});
	}
}
