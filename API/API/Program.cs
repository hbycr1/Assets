using API;

// Init Builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureServices();

// Build Services
var app = builder.Build();

// Configure the HTTP request pipeline & run.
app.ConfigureApplication();
