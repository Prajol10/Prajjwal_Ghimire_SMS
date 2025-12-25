# Prajjwal Ghimire - Student Management System

A comprehensive Student Management System built with ASP.NET Core MVC, implementing the Repository Pattern with role-based authentication and authorization.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Installation and Setup](#installation-and-setup)
- [Usage](#usage)
- [Database Schema](#database-schema)
- [Assignment Requirements](#assignment-requirements)
- [Key Implementation Details](#key-implementation-details)
- [Learning Outcomes](#learning-outcomes)
- [Author](#author)

## Overview

This Student Management System allows administrators to manage courses and students while providing students with view-only access to the system. The application implements secure authentication, role-based authorization, and follows the repository pattern for clean data access.

## Features

### Authentication and Authorization
- User registration and login system
- Role-based access control (Admin and Student)
- Secure password hashing with ASP.NET Core Identity
- Pre-seeded admin and student accounts

### Course Management
- Admin Access: Create, Read, Update, Delete courses
- Student Access: View courses only
- Course fields: Code, Name, Description, Credits

### Student Management
- Admin Access: Complete CRUD operations
- Student Access: View all students and individual details
- Student profile image upload functionality
- Student fields: Name, Gender, Address, Phone Number, Email, Class, Section, Image
- Course enrollment (each student studies one course)

### Repository Pattern
- Generic repository interface (IRepository<T>)
- Generic repository implementation (Repository<T>)
- Dependency injection throughout the application
- Clean separation of data access and business logic

### Additional Features
- Sample data seeding (5 courses, 6 students)
- Responsive UI with Bootstrap 5
- Image upload and management
- Role-based navigation menu
- Professional home page

## Technologies Used

**Framework and Core Technologies:**
- ASP.NET Core MVC (.NET 10) - Web application framework
- Entity Framework Core - ORM for database operations
- SQLite - Lightweight database
- ASP.NET Core Identity - Authentication and authorization
- Bootstrap 5 - Responsive UI design
- Razor Pages - Server-side rendering
- Repository Pattern - Data access architecture

## Project Structure
```
Prajjwal_Ghimire_SMS/
├── Controllers/
│   ├── AccountController.cs      
│   ├── CourseController.cs       
│   ├── StudentController.cs      
│   └── HomeController.cs         
├── Data/
│   ├── ApplicationDbContext.cs   
│   ├── IRepository.cs            
│   ├── Repository.cs             
│   └── SeedData.cs               
├── Models/
│   ├── Course.cs                 
│   ├── Student.cs                
│   └── ErrorViewModel.cs         
├── ViewModels/
│   ├── LoginViewModel.cs         
│   └── RegisterViewModel.cs      
├── Views/
│   ├── Account/
│   ├── Course/
│   ├── Student/
│   ├── Home/
│   └── Shared/
├── wwwroot/
│   └── images/
│       └── students/             
├── Program.cs                     
├── appsettings.json              
└── README.md                     
```

## Installation and Setup

### Prerequisites
- .NET 8.0 SDK or higher
- Visual Studio Code or Visual Studio 2022
- Git (optional, for cloning)

### Installation Steps

**Step 1: Clone the Repository**
```bash
git clone https://github.com/Prajol10/Prajjwal_Ghimire_SMS.git
cd Prajjwal_Ghimire_SMS
```

**Step 2: Restore Dependencies**
```bash
dotnet restore
```

**Step 3: Apply Database Migrations**
```bash
dotnet ef database update
```

**Step 4: Run the Application**
```bash
dotnet run
```

**Step 5: Open in Browser**
Navigate to http://localhost:5208 or https://localhost:5209

## Usage

### Default Login Credentials

**Admin Account**
- Email: admin@sms.com
- Password: Admin@123
- Permissions: Full CRUD access to Courses and Students

**Student Account**
- Email: student@sms.com
- Password: Student@123
- Permissions: View-only access to Courses and Students

### Demo Workflow

**1. Login as Admin**
- Navigate to /Account/Login
- Use admin credentials
- Access full CRUD operations

**2. Manage Courses**
- Go to Courses and click Create New Course
- Add course details (Code, Name, Description, Credits)
- Edit or delete existing courses

**3. Manage Students**
- Go to Students and click Add New Student
- Fill in all required fields
- Upload profile image
- Assign a course
- Edit or delete students

**4. Test Student Access**
- Logout from admin account
- Login with student credentials
- Notice Create/Edit/Delete buttons are hidden
- View courses and student profiles only

## Database Schema

### Courses Table
- Id (int, Primary Key)
- Code (string) - Course code
- Name (string) - Course name
- Description (string) - Course description
- Credits (int) - Credit hours

### Students Table
- Id (int, Primary Key)
- Name (string) - Student full name
- Gender (string) - Male/Female/Other
- Address (string) - Residential address
- PhoneNumber (string) - Contact number
- Email (string) - Email address
- Class (string) - Class/Program
- Section (string) - Section
- ImagePath (string) - Profile image path
- CourseId (int, Foreign Key) - References Courses table

### Identity Tables
- AspNetUsers - User accounts
- AspNetRoles - Roles (Admin, Student)
- AspNetUserRoles - User-Role mapping

## Assignment Requirements

All assignment requirements have been successfully implemented:

- ASP.NET Core MVC Application Development
- Entity Framework Core with SQLite database
- Repository Pattern implementation
- Course and Student tables in database
- Complete CRUD operations for both entities
- All required student fields (Name, Gender, Address, Phone, Email, Class, Section, Image)
- Course-Student relationship
- Admin and Student roles
- Authentication and Authorization system
- Admin: Full CRUD access to courses and students
- Student: View-only access with image display
- Project naming convention: Prajjwal_Ghimire_SMS

## Key Implementation Details

### Repository Pattern Implementation

The repository pattern provides an abstraction layer between the data access logic and business logic:
```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
```

### Authorization Implementation

Role-based authorization ensures proper access control:
```csharp
[Authorize(Roles = "Admin")]
public async Task<IActionResult> Create(Course course)
{
    // Only accessible by Admin users
}
```

### Image Upload Management
- Images are stored in wwwroot/images/students/
- Each image receives a unique filename using GUID
- Old images are automatically deleted when updating student profiles
- Images are deleted when removing student records

## Learning Outcomes

Through this project, the following concepts were learned and implemented:

- ASP.NET Core MVC architecture and design patterns
- Entity Framework Core migrations and database relationships
- Repository Pattern for maintainable code architecture
- ASP.NET Core Identity for secure authentication
- Role-based authorization and access control
- File upload handling and management
- Dependency injection principles
- Asynchronous programming with async/await
- Razor view engine and server-side rendering
- Bootstrap framework for responsive design
- Git version control and GitHub repository management

## Development Timeline

- Day 1: Project initialization, database design, entity models, repository pattern setup, authentication system
- Day 2: CRUD operations implementation, image upload functionality, authorization, user interface development
- Day 3: Sample data seeding, comprehensive testing, bug fixes, documentation

## Author

Prajjwal Ghimire

December 2025

## Acknowledgments

This project was developed as part of an academic assignment to demonstrate proficiency in ASP.NET Core MVC development, database management, and software architecture patterns.

Special thanks to:
- ASP.NET Core Documentation
- Entity Framework Core Documentation
- Microsoft Learn Platform
- Bootstrap Framework
- The open-source community

---

This project was created for educational purposes as part of a college assignment.

Last Updated: December 2025
