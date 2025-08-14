# SeniorLearn

SeniorLearn is an education platform designed to replace paper and Excel-based record keeping with a role-aware system for managing courses, lessons, enrolments, and subscriptions.
The platform is designed to be intutive and easy to use.

## Overview
- Administrators can register new users and assign roles.
- User roles: **Standard**, **Professional**, **Honorary**.
- Standard members can enrol in classes with an active subscription.
- Professional members have a 3‑month trial; they remain professional while teaching. Professional members can also enrol in courses and lessons, but not their own.
- Honorary members have lifelong membership; they hold the same authorisation as Professional members allowing them to create courses and lessons.

## Technology Stack Used
- ASP.NET Core MVC
- Entity Framework Core
- Identity Framework
- JWT Authentication
- Microsoft SQL Server
- MongoDB
- LINQ

## Requirements
- .NET 8 SDK
- SQL Server
- Visual Studio / VS Code / Rider
- Git

## Getting Started
1. Clone the repository:
```
git clone https://github.com/sp0389/SeniorLearn.git
```
2. Change to the cloned directory.
```
cd SeniorLearn
```
3. Run database migrations:
```
dotnet ef database update
```
4. Start the application:
```
dotnet run
```
## Usage
- Access the web app via https://localhost:7241 (or configured port).
- Login as Administrator to manage users and roles.
- Create courses/lessons as Professional or Honorary.

## Deployment
```
dotnet publish -c Release
```
Deploy to Azure, IIS, or containerize with Docker.

## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Commit changes.
4. Open a Pull Request.

## Screenshots
<img width="1507" height="1277" alt="Image" src="https://github.com/user-attachments/assets/7eafad39-2eda-4f34-acb3-4b0f074f924f" /> 
<img width="1245" height="1042" alt="Screenshot 2025-08-14 115419" src="https://github.com/user-attachments/assets/cf9d9d95-380d-49ff-a136-6d28592e4c0f" />
<img width="1232" height="1274" alt="Screenshot 2025-08-14 115830" src="https://github.com/user-attachments/assets/99bb7908-910e-4ae4-9b57-a7b073cc4487" />
<img width="1236" height="1259" alt="Screenshot 2025-08-14 120833" src="https://github.com/user-attachments/assets/956cb526-30d1-4e15-a3d1-fb9e141716ec" />
<img width="1228" height="1258" alt="Screenshot 2025-08-14 122140" src="https://github.com/user-attachments/assets/62628bf8-7ba7-43e4-b234-d61e98e428aa" />
<img width="1232" height="1269" alt="Screenshot 2025-08-14 122305" src="https://github.com/user-attachments/assets/a4d5373e-e6a5-4e9c-bb01-5ba43e0603c3" />



