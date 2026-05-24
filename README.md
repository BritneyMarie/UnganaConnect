# UnganaConnect

A digital capacity-building platform for African civil society organisations (CSOs), built for [Ungana-Afrika](https://ungana-afrika.org). UnganaConnect centralises training, resource sharing, community engagement, and event management into a single web application — replacing fragmented manual processes with an accessible, scalable digital hub.

**Live Demo:** *Deploying to Render — link coming soon*

---

## My Role — Britney Chauke

I served as **Business Analyst, Project Documentation Lead, and Frontend Contributor** on this five-person team during our Work-Integrated Learning (WIL) placement.

### Business Analysis

- Conducted stakeholder interviews and requirement-gathering sessions with Ungana-Afrika
- Created user personas and journey maps to prioritise development
- Documented functional specifications and acceptance criteria for each module
- Facilitated feedback loops between users and developers to refine features
- Mediated scope concerns and resolved feedback-related conflicts

### Project Documentation

- Authored the project management document (scope, WBS, Gantt charts, risk matrix)
- Maintained revision-controlled documentation across all project phases
- Produced weekly progress reports and client engagement notes
- Compiled the final submission package including technical and user manuals

### Frontend Development

- Built and refined Razor views for the blog, event, and resource modules
- Contributed to responsive UI layouts using Bootstrap CSS
- Participated in integration testing and UI bug fixes during sprint cycles
- Helped ensure accessibility and mobile-first design across pages

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core 8.0 MVC |
| Database | PostgreSQL + Entity Framework Core 9.0 |
| Cloud Storage | Azure Blob Storage |
| Frontend | Razor Views, Bootstrap CSS |
| Logging | Serilog |
| Auth | Session-based, role-based access control |
| Containerisation | Docker |
| Hosting | Koyeb |
| CI/CD | GitHub Actions |

---

## Features

- **Learning Management** — Course catalogue, module-based learning, quizzes, progress tracking, downloadable certificates
- **Resource Library** — Upload, browse, and download shared documents and files
- **Community Forum** — Categorised discussion topics with threaded replies
- **Event Management** — Training event listings, registration, and agenda tracking
- **Blog** — Article publishing with categories, tags, and featured posts
- **User Profiles** — Registration, authentication, role-based dashboards (Admin / Member)
- **Azure Integration** — Secure cloud file storage with upload validation

---

## Project Structure

```
UnganaConnect/
├── Controllers/      # MVC Controllers (Auth, Course, Blog, Event, Forum, Resource, Profile)
├── Models/           # Data models and ViewModels
├── Views/            # Razor view templates
├── Services/         # ApiService, AzureBlobService
├── Data/             # EF Core DbContext
├── Migrations/       # Database migrations
├── wwwroot/          # Static assets (CSS, JS, images)
├── Properties/       # Launch settings
├── Program.cs        # Application entry point
└── Dockerfile        # Container configuration
```

---

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- PostgreSQL instance (local or hosted — we used [Neon](https://neon.tech))
- Azure Storage account (for file uploads)

### Setup

```bash
git clone https://github.com/BritneyMarie/UnganaConnect.git
cd UnganaConnect/UnganaConnect
dotnet restore
```

Create a `.env` file in the project root:

```
DefaultConnection=Host=<host>;Database=<db>;Username=<user>;Password=<pass>;SSL Mode=Require
AzureBlobStorage=DefaultEndpointsProtocol=https;AccountName=<name>;AccountKey=<key>;EndpointSuffix=core.windows.net
```

Run migrations and start:

```bash
dotnet ef database update
dotnet run
```

### Docker

```bash
docker build -t unganaconnect .
docker run -p 8080:8080 --env-file .env unganaconnect
```

---

## Team

| Member | Role |
|--------|------|
| Terence Chuene | Group Leader, Backend Developer |
| Bhekile Mngwenya | Full Stack Developer |
| Sage | Frontend Developer |
| **Britney Chauke** | **Business Analyst, Documentation Lead, Frontend Contributor** |
| Zoleka Mashele | Project Manager |

---

## Documentation

Project management artifacts, requirements documents, and presentation materials are available in the [`docs/`](docs/) folder:

- Project Management Document
- System Overview & Technical Documentation
- Weekly Attendance Log
- Self & Peer Evaluations
- Group Presentation Slides

---

## License

This project was developed as part of a Work-Integrated Learning module. All rights reserved by the respective contributors and Ungana-Afrika.
