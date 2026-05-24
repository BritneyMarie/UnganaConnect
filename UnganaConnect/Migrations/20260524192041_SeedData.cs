using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UnganaConnect.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Duration", "Enrolled", "Level", "Progress", "Rating", "Status", "ThumbnailContentType", "ThumbnailFileName", "ThumbnailUrl", "Title" },
                values: new object[,]
                {
                    { 1, "Digital Skills", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "A foundational course covering essential digital skills for civil society organisations, including internet safety, email communication, and basic productivity tools.", "4 weeks", 24, "Beginner", 0, 4.5, "available", "", "", "", "Introduction to Digital Literacy" },
                    { 2, "Data & Analytics", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Learn how to collect, store, and analyse data effectively. Covers spreadsheets, databases, and reporting tools tailored for non-profit organisations.", "6 weeks", 18, "Intermediate", 0, 4.2000000000000002, "available", "", "", "", "Data Management for NGOs" },
                    { 3, "Capacity Building", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Master the art of writing compelling grant proposals. Includes templates, case studies, and peer review exercises for African CSOs.", "3 weeks", 32, "Intermediate", 0, 4.7999999999999998, "available", "", "", "", "Grant Writing and Proposal Development" },
                    { 4, "Security", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Protect your organisation from digital threats. Covers password management, phishing awareness, secure communication, and data privacy compliance.", "2 weeks", 15, "Beginner", 0, 4.5999999999999996, "available", "", "", "", "Cybersecurity Essentials for CSOs" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Agenda", "Date", "Description", "Duration", "Format", "Instructor", "Level", "Location", "Materials", "MaxParticipants", "Participants", "Price", "Tags", "Time", "Title", "Type" },
                values: new object[,]
                {
                    { 1, new List<string>(), new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), "A hands-on workshop exploring how African CSOs can leverage ICT tools for greater community impact. Topics include digital fundraising, online collaboration, and social media strategy.", "Full Day", "Hybrid", "Ungana-Afrika Team", "All Levels", "Johannesburg, South Africa", new List<string>(), 50, 12, "Free", new List<string>(), new TimeSpan(0, 9, 0, 0, 0), "ICT for Development Workshop 2025", "Workshop" },
                    { 2, new List<string>(), new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Understanding data protection regulations and best practices for African civil society organisations handling sensitive community data.", "2 Hours", "Online", "Dr. Naledi Khumalo", "Intermediate", "Online (Zoom)", new List<string>(), 100, 8, "Free", new List<string>(), new TimeSpan(0, 14, 0, 0, 0), "Data Privacy in African Civil Society", "Webinar" }
                });

            migrationBuilder.InsertData(
                table: "ForumCategories",
                columns: new[] { "Id", "Color", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "#3B82F6", "Open discussions about ICT in civil society", "General Discussion" },
                    { 2, "#10B981", "Questions and help with courses and modules", "Training Support" },
                    { 3, "#F59E0B", "Share and learn organisational best practices", "Best Practices" },
                    { 4, "#8B5CF6", "Discuss upcoming events and networking opportunities", "Events & Networking" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "CreatedAt", "Email", "FirstName", "LastName", "Location", "Organization", "PasswordHash", "Phone", "ProfilePictureUrl", "Role", "UpdatedAt", "Website" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), "Platform administrator for UnganaConnect.", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "admin@unganaconnect.org", "Admin", "User", "Johannesburg, South Africa", "Ungana-Afrika", "6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=", null, null, "Admin", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"), "A sample CSO member exploring training opportunities.", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "member@unganaconnect.org", "Demo", "Member", "Cape Town, South Africa", "African Digital Initiative", "q+LT7VQZ4aIpPANKazdaYi/1pg5aww8pxGEiCJj/3Zc=", null, null, "Member", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "AllowComments", "AuthorId", "Category", "Content", "CreatedAt", "Excerpt", "FeaturedImageUrl", "IsFeatured", "PublishedAt", "Status", "Tags", "Title", "UpdatedAt", "Views" },
                values: new object[,]
                {
                    { 1, true, new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), "Technology", "Digital transformation is no longer optional for civil society organisations in Africa. As communities increasingly rely on digital services, CSOs must adapt to remain effective advocates and service providers.\n\nUnganaConnect was built to bridge this gap, providing accessible training, resource sharing, and community engagement tools specifically designed for African civil society.\n\nKey areas where digital tools make the biggest impact include donor reporting, community outreach, internal collaboration, and impact measurement.", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "How digital tools are reshaping the way African civil society organisations operate and deliver impact.", null, true, new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Published", "digital-transformation,CSOs,Africa", "Empowering African CSOs Through Digital Transformation", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), 45 },
                    { 2, true, new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), "Resources", "Running a non-profit with limited resources does not mean you have to sacrifice productivity. Here are five free tools that can transform how your organisation operates:\n\n1. Google Workspace for Nonprofits - email, docs, and cloud storage\n2. Canva - professional design without a designer\n3. Trello - project management and task tracking\n4. Mailchimp - email marketing for up to 500 contacts\n5. UnganaConnect - training and capacity building for African CSOs", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Essential free digital tools that can help non-profits work smarter with limited budgets.", null, false, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Published", "tools,NGO,productivity,free", "5 Free Tools Every NGO Should Be Using", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), 28 }
                });

            migrationBuilder.InsertData(
                table: "ForumTopics",
                columns: new[] { "Id", "Author", "AuthorOrg", "AuthorRole", "CategoryId", "Content", "CreatedAt", "Excerpt", "IsAnswered", "IsPinned", "Replies", "Tags", "Title", "Views" },
                values: new object[,]
                {
                    { 1, "Admin User", "Ungana-Afrika", "Admin", 1, "Welcome to the UnganaConnect community forum. This is a space for African CSOs to share knowledge, ask questions, and collaborate. Feel free to introduce yourself and your organisation!", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Welcome to the UnganaConnect community forum.", false, true, 0, "welcome,community", "Welcome to UnganaConnect Forum!", 0 },
                    { 2, "Demo Member", "African Digital Initiative", "Member", 2, "I just finished the Digital Literacy course and wanted to share some tips that helped me. First, take the quizzes after each module rather than waiting until the end. Second, the resource library has great supplementary materials.", new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Tips and advice for completing the Digital Literacy course.", false, false, 0, "digital-literacy,tips,courses", "Tips for completing the Digital Literacy course", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ForumCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ForumCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ForumTopics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ForumTopics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-8901-bcde-f12345678901"));

            migrationBuilder.DeleteData(
                table: "ForumCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ForumCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"));
        }
    }
}
