using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UnganaConnect.Frontend.Models;
using UnganaConnect.Models.Blog;
using UnganaConnect.Models.Course;
using UnganaConnect.Models.User;

namespace UnganaConnect.Data
{
    public class UnganaConnectDbContext : DbContext
    {
        public UnganaConnectDbContext(DbContextOptions<UnganaConnectDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrollment> Enrollments { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<ForumCategory> ForumCategories { get; set; }
        public DbSet<ForumTopic> ForumTopics { get; set; }
        public DbSet<ForumReply> ForumReplies { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            var memberId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901");
            var now = new DateTime(2025, 10, 30, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    Email = "admin@unganaconnect.org",
                    PasswordHash = HashPassword("Admin@123"),
                    FirstName = "Admin",
                    LastName = "User",
                    Role = "Admin",
                    Bio = "Platform administrator for UnganaConnect.",
                    Organization = "Ungana-Afrika",
                    Location = "Johannesburg, South Africa",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new User
                {
                    Id = memberId,
                    Email = "member@unganaconnect.org",
                    PasswordHash = HashPassword("Member@123"),
                    FirstName = "Demo",
                    LastName = "Member",
                    Role = "Member",
                    Bio = "A sample CSO member exploring training opportunities.",
                    Organization = "African Digital Initiative",
                    Location = "Cape Town, South Africa",
                    CreatedAt = now,
                    UpdatedAt = now
                }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "Introduction to Digital Literacy",
                    Description = "A foundational course covering essential digital skills for civil society organisations, including internet safety, email communication, and basic productivity tools.",
                    Category = "Digital Skills",
                    Duration = "4 weeks",
                    Level = "Beginner",
                    Rating = 4.5,
                    Enrolled = 24,
                    Status = "available",
                    CreatedAt = now
                },
                new Course
                {
                    Id = 2,
                    Title = "Data Management for NGOs",
                    Description = "Learn how to collect, store, and analyse data effectively. Covers spreadsheets, databases, and reporting tools tailored for non-profit organisations.",
                    Category = "Data & Analytics",
                    Duration = "6 weeks",
                    Level = "Intermediate",
                    Rating = 4.2,
                    Enrolled = 18,
                    Status = "available",
                    CreatedAt = now
                },
                new Course
                {
                    Id = 3,
                    Title = "Grant Writing and Proposal Development",
                    Description = "Master the art of writing compelling grant proposals. Includes templates, case studies, and peer review exercises for African CSOs.",
                    Category = "Capacity Building",
                    Duration = "3 weeks",
                    Level = "Intermediate",
                    Rating = 4.8,
                    Enrolled = 32,
                    Status = "available",
                    CreatedAt = now
                },
                new Course
                {
                    Id = 4,
                    Title = "Cybersecurity Essentials for CSOs",
                    Description = "Protect your organisation from digital threats. Covers password management, phishing awareness, secure communication, and data privacy compliance.",
                    Category = "Security",
                    Duration = "2 weeks",
                    Level = "Beginner",
                    Rating = 4.6,
                    Enrolled = 15,
                    Status = "available",
                    CreatedAt = now
                }
            );

            modelBuilder.Entity<ForumCategory>().HasData(
                new ForumCategory { Id = 1, Name = "General Discussion", Description = "Open discussions about ICT in civil society", Color = "#3B82F6" },
                new ForumCategory { Id = 2, Name = "Training Support", Description = "Questions and help with courses and modules", Color = "#10B981" },
                new ForumCategory { Id = 3, Name = "Best Practices", Description = "Share and learn organisational best practices", Color = "#F59E0B" },
                new ForumCategory { Id = 4, Name = "Events & Networking", Description = "Discuss upcoming events and networking opportunities", Color = "#8B5CF6" }
            );

            modelBuilder.Entity<ForumTopic>().HasData(
                new ForumTopic
                {
                    Id = 1,
                    Title = "Welcome to UnganaConnect Forum!",
                    Content = "Welcome to the UnganaConnect community forum. This is a space for African CSOs to share knowledge, ask questions, and collaborate. Feel free to introduce yourself and your organisation!",
                    Author = "Admin User",
                    AuthorRole = "Admin",
                    AuthorOrg = "Ungana-Afrika",
                    CategoryId = 1,
                    CreatedAt = now,
                    IsPinned = true,
                    Tags = "welcome,community",
                    Excerpt = "Welcome to the UnganaConnect community forum."
                },
                new ForumTopic
                {
                    Id = 2,
                    Title = "Tips for completing the Digital Literacy course",
                    Content = "I just finished the Digital Literacy course and wanted to share some tips that helped me. First, take the quizzes after each module rather than waiting until the end. Second, the resource library has great supplementary materials.",
                    Author = "Demo Member",
                    AuthorRole = "Member",
                    AuthorOrg = "African Digital Initiative",
                    CategoryId = 2,
                    CreatedAt = now.AddDays(1),
                    Tags = "digital-literacy,tips,courses",
                    Excerpt = "Tips and advice for completing the Digital Literacy course."
                }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "ICT for Development Workshop 2025",
                    Type = "Workshop",
                    Format = "Hybrid",
                    Date = new DateTime(2025, 12, 15, 0, 0, 0, DateTimeKind.Utc),
                    Time = new TimeSpan(9, 0, 0),
                    Duration = "Full Day",
                    Price = "Free",
                    Instructor = "Ungana-Afrika Team",
                    Participants = 12,
                    MaxParticipants = 50,
                    Level = "All Levels",
                    Location = "Johannesburg, South Africa",
                    Description = "A hands-on workshop exploring how African CSOs can leverage ICT tools for greater community impact. Topics include digital fundraising, online collaboration, and social media strategy."
                },
                new Event
                {
                    Id = 2,
                    Title = "Data Privacy in African Civil Society",
                    Type = "Webinar",
                    Format = "Online",
                    Date = new DateTime(2025, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                    Time = new TimeSpan(14, 0, 0),
                    Duration = "2 Hours",
                    Price = "Free",
                    Instructor = "Dr. Naledi Khumalo",
                    Participants = 8,
                    MaxParticipants = 100,
                    Level = "Intermediate",
                    Location = "Online (Zoom)",
                    Description = "Understanding data protection regulations and best practices for African civil society organisations handling sensitive community data."
                }
            );

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "Empowering African CSOs Through Digital Transformation",
                    Content = "Digital transformation is no longer optional for civil society organisations in Africa. As communities increasingly rely on digital services, CSOs must adapt to remain effective advocates and service providers.\n\nUnganaConnect was built to bridge this gap, providing accessible training, resource sharing, and community engagement tools specifically designed for African civil society.\n\nKey areas where digital tools make the biggest impact include donor reporting, community outreach, internal collaboration, and impact measurement.",
                    Excerpt = "How digital tools are reshaping the way African civil society organisations operate and deliver impact.",
                    Category = "Technology",
                    Tags = "digital-transformation,CSOs,Africa",
                    Status = "Published",
                    Views = 45,
                    IsFeatured = true,
                    AllowComments = true,
                    AuthorId = adminId,
                    CreatedAt = now,
                    UpdatedAt = now,
                    PublishedAt = now
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "5 Free Tools Every NGO Should Be Using",
                    Content = "Running a non-profit with limited resources does not mean you have to sacrifice productivity. Here are five free tools that can transform how your organisation operates:\n\n1. Google Workspace for Nonprofits - email, docs, and cloud storage\n2. Canva - professional design without a designer\n3. Trello - project management and task tracking\n4. Mailchimp - email marketing for up to 500 contacts\n5. UnganaConnect - training and capacity building for African CSOs",
                    Excerpt = "Essential free digital tools that can help non-profits work smarter with limited budgets.",
                    Category = "Resources",
                    Tags = "tools,NGO,productivity,free",
                    Status = "Published",
                    Views = 28,
                    IsFeatured = false,
                    AllowComments = true,
                    AuthorId = adminId,
                    CreatedAt = now.AddDays(3),
                    UpdatedAt = now.AddDays(3),
                    PublishedAt = now.AddDays(3)
                }
            );
        }
    }
}
