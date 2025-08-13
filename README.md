# Senior Learn

Senior Learn is an education platform designed to replace paper and Excel-based record keeping with a role-aware system for managing courses, lessons, enrolments, and subscriptions.
The platform is designed to be intutive and easy to use.

## Overview
- Administrators can register new users and assign roles.
- User roles: **Standard**, **Professional**, **Honorary**.
- Standard members can enrol in classes with an active subscription.
- Professional members have a 3‑month trial; they remain professional while teaching. Professional members can also enrol in courses and lessons, but not their own.
- Honorary members have lifelong membership; they hold the same authorisation as Professional members allowing them to create courses and lessons.

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
