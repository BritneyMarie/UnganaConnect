using Azure.Storage.Blobs;
using DotNetEnv;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using UnganaConnect.Data;
using UnganaConnect.Frontend.Services;


if (File.Exists(".env")) Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Clear default logging providers
builder.Logging.ClearProviders();

// Configure Serilog
var logConfig = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console();
if (builder.Environment.IsDevelopment())
    logConfig.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day);
Log.Logger = logConfig.CreateLogger();

// Route ILogger<> to Serilog
builder.Host.UseSerilog();


//configuration blob storage
var conn = Environment.GetEnvironmentVariable("AzureBlobStorage");
if (!string.IsNullOrEmpty(conn))
{
    builder.Services.AddSingleton(x => new BlobServiceClient(conn));
    builder.Services.AddScoped<UnganaConnect.Frontend.Services.AzureBlobService>();
}


//configuration of ConnectionString
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
builder.Services.AddDbContext<UnganaConnectDbContext>(options =>
    options.UseNpgsql(connectionString));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();



// Set default culture to South Africa
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-ZA");
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<UnganaConnectDbContext>();
    db.Database.Migrate();
}
catch (Exception ex)
{
    Log.Warning(ex, "Database migration failed — check DefaultConnection");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


if (app.Environment.IsDevelopment())
    app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSerilogRequestLogging();
app.UseRequestLocalization();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();