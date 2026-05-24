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
    SeedData(db);
}
catch (Exception ex)
{
    Log.Warning(ex, "Database migration or seeding failed — check DefaultConnection");
}

static void SeedData(UnganaConnectDbContext db)
{
    if (db.Users.Any()) return;

    static string Hash(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
    }

    var now = DateTime.UtcNow;

    var admin = new UnganaConnect.Models.User.User
    {
        Email = "admin@unganaconnect.org",
        PasswordHash = Hash("Admin@123"),
        FirstName = "Admin",
        LastName = "User",
        Role = "Admin",
        Bio = "Platform administrator for UnganaConnect.",
        Organization = "Ungana-Afrika",
        Location = "Johannesburg, South Africa",
        CreatedAt = now,
        UpdatedAt = now
    };

    var member = new UnganaConnect.Models.User.User
    {
        Email = "member@unganaconnect.org",
        PasswordHash = Hash("Member@123"),
        FirstName = "Demo",
        LastName = "Member",
        Role = "Member",
        Bio = "A sample CSO member exploring training opportunities.",
        Organization = "African Digital Initiative",
        Location = "Cape Town, South Africa",
        CreatedAt = now,
        UpdatedAt = now
    };

    db.Users.AddRange(admin, member);

    db.Courses.AddRange(
        new UnganaConnect.Models.Course.Course { Title = "Introduction to Digital Literacy", Description = "A foundational course covering essential digital skills for civil society organisations, including internet safety, email communication, and basic productivity tools.", Category = "Digital Skills", Duration = "4 weeks", Level = "Beginner", Rating = 4.5, Enrolled = 24, Status = "available", CreatedAt = now },
        new UnganaConnect.Models.Course.Course { Title = "Data Management for NGOs", Description = "Learn how to collect, store, and analyse data effectively. Covers spreadsheets, databases, and reporting tools tailored for non-profit organisations.", Category = "Data & Analytics", Duration = "6 weeks", Level = "Intermediate", Rating = 4.2, Enrolled = 18, Status = "available", CreatedAt = now },
        new UnganaConnect.Models.Course.Course { Title = "Grant Writing and Proposal Development", Description = "Master the art of writing compelling grant proposals. Includes templates, case studies, and peer review exercises for African CSOs.", Category = "Capacity Building", Duration = "3 weeks", Level = "Intermediate", Rating = 4.8, Enrolled = 32, Status = "available", CreatedAt = now },
        new UnganaConnect.Models.Course.Course { Title = "Cybersecurity Essentials for CSOs", Description = "Protect your organisation from digital threats. Covers password management, phishing awareness, secure communication, and data privacy compliance.", Category = "Security", Duration = "2 weeks", Level = "Beginner", Rating = 4.6, Enrolled = 15, Status = "available", CreatedAt = now }
    );

    var cat1 = new UnganaConnect.Frontend.Models.ForumCategory { Name = "General Discussion", Description = "Open discussions about ICT in civil society", Color = "#3B82F6" };
    var cat2 = new UnganaConnect.Frontend.Models.ForumCategory { Name = "Training Support", Description = "Questions and help with courses and modules", Color = "#10B981" };
    db.ForumCategories.AddRange(cat1, cat2,
        new UnganaConnect.Frontend.Models.ForumCategory { Name = "Best Practices", Description = "Share and learn organisational best practices", Color = "#F59E0B" },
        new UnganaConnect.Frontend.Models.ForumCategory { Name = "Events & Networking", Description = "Discuss upcoming events and networking opportunities", Color = "#8B5CF6" }
    );

    db.ForumTopics.AddRange(
        new UnganaConnect.Frontend.Models.ForumTopic { Title = "Welcome to UnganaConnect Forum!", Content = "Welcome to the UnganaConnect community forum. This is a space for African CSOs to share knowledge, ask questions, and collaborate.", Author = "Admin User", AuthorRole = "Admin", AuthorOrg = "Ungana-Afrika", Category = cat1, CreatedAt = now, IsPinned = true, Tags = "welcome,community", Excerpt = "Welcome to the UnganaConnect community forum." },
        new UnganaConnect.Frontend.Models.ForumTopic { Title = "Tips for completing the Digital Literacy course", Content = "I just finished the Digital Literacy course and wanted to share some tips. Take the quizzes after each module rather than waiting until the end.", Author = "Demo Member", AuthorRole = "Member", AuthorOrg = "African Digital Initiative", Category = cat2, CreatedAt = now, Tags = "digital-literacy,tips", Excerpt = "Tips for completing the Digital Literacy course." }
    );

    db.Events.AddRange(
        new UnganaConnect.Frontend.Models.Event { Title = "ICT for Development Workshop 2025", Type = "Workshop", Format = "Hybrid", Date = new DateTime(2025, 12, 15, 0, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(9, 0, 0), Duration = "Full Day", Price = "Free", Instructor = "Ungana-Afrika Team", Participants = 12, MaxParticipants = 50, Level = "All Levels", Location = "Johannesburg, South Africa", Description = "A hands-on workshop exploring how African CSOs can leverage ICT tools for greater community impact." },
        new UnganaConnect.Frontend.Models.Event { Title = "Data Privacy in African Civil Society", Type = "Webinar", Format = "Online", Date = new DateTime(2025, 11, 20, 0, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(14, 0, 0), Duration = "2 Hours", Price = "Free", Instructor = "Dr. Naledi Khumalo", Participants = 8, MaxParticipants = 100, Level = "Intermediate", Location = "Online (Zoom)", Description = "Understanding data protection regulations and best practices for African CSOs handling sensitive community data." }
    );

    db.BlogPosts.AddRange(
        new UnganaConnect.Models.Blog.BlogPost { Title = "Empowering African CSOs Through Digital Transformation", Content = "Digital transformation is no longer optional for civil society organisations in Africa. UnganaConnect was built to bridge this gap, providing accessible training, resource sharing, and community engagement tools.", Excerpt = "How digital tools are reshaping African civil society organisations.", Category = "Technology", Tags = "digital-transformation,CSOs,Africa", Status = "Published", Views = 45, IsFeatured = true, AuthorId = admin.Id, CreatedAt = now, UpdatedAt = now, PublishedAt = now },
        new UnganaConnect.Models.Blog.BlogPost { Title = "5 Free Tools Every NGO Should Be Using", Content = "Running a non-profit with limited resources does not mean you have to sacrifice productivity. Google Workspace, Canva, Trello, Mailchimp, and UnganaConnect can transform how your organisation operates.", Excerpt = "Essential free digital tools for non-profits.", Category = "Resources", Tags = "tools,NGO,productivity", Status = "Published", Views = 28, AuthorId = admin.Id, CreatedAt = now, UpdatedAt = now, PublishedAt = now }
    );

    db.SaveChanges();
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