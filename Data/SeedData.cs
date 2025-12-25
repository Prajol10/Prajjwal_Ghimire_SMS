using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prajjwal_Ghimire_SMS.Models;

namespace Prajjwal_Ghimire_SMS.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed Roles
            string[] roleNames = { "Admin", "Student" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminEmail = "admin@sms.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Seed Sample Student User
            var studentEmail = "student@sms.com";
            var studentUser = await userManager.FindByEmailAsync(studentEmail);
            if (studentUser == null)
            {
                var student = new IdentityUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(student, "Student@123");
                await userManager.AddToRoleAsync(student, "Student");
            }

            // Seed Courses (only if no courses exist)
            if (!context.Courses.Any())
            {
                var courses = new List<Course>
                {
                    new Course
                    {
                        Code = "CS101",
                        Name = "Introduction to Programming",
                        Description = "Learn the fundamentals of programming using C# and .NET",
                        Credits = 3
                    },
                    new Course
                    {
                        Code = "CS201",
                        Name = "Data Structures and Algorithms",
                        Description = "Study of fundamental data structures and algorithmic techniques",
                        Credits = 4
                    },
                    new Course
                    {
                        Code = "CS301",
                        Name = "Database Management Systems",
                        Description = "Design and implementation of database systems",
                        Credits = 3
                    },
                    new Course
                    {
                        Code = "CS302",
                        Name = "Web Development",
                        Description = "Full-stack web development with ASP.NET Core MVC",
                        Credits = 4
                    },
                    new Course
                    {
                        Code = "CS401",
                        Name = "Software Engineering",
                        Description = "Principles and practices of software development",
                        Credits = 3
                    }
                };

                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
            }

            // Seed Students (only if no students exist)
            if (!context.Students.Any())
            {
                var courses = await context.Courses.ToListAsync();
                
                var students = new List<Student>
                {
                    new Student
                    {
                        Name = "Aayush Sharma",
                        Gender = "Male",
                        Address = "Thamel, Kathmandu",
                        PhoneNumber = "9841234567",
                        Email = "aayush.sharma@example.com",
                        Class = "Bachelor",
                        Section = "A",
                        CourseId = courses[0].Id
                    },
                    new Student
                    {
                        Name = "Srijana Thapa",
                        Gender = "Female",
                        Address = "Pulchowk, Lalitpur",
                        PhoneNumber = "9851234568",
                        Email = "srijana.thapa@example.com",
                        Class = "Bachelor",
                        Section = "A",
                        CourseId = courses[1].Id
                    },
                    new Student
                    {
                        Name = "Bibek Adhikari",
                        Gender = "Male",
                        Address = "Baneshwor, Kathmandu",
                        PhoneNumber = "9861234569",
                        Email = "bibek.adhikari@example.com",
                        Class = "Bachelor",
                        Section = "B",
                        CourseId = courses[2].Id
                    },
                    new Student
                    {
                        Name = "Kritika Rai",
                        Gender = "Female",
                        Address = "Bhaktapur",
                        PhoneNumber = "9871234570",
                        Email = "kritika.rai@example.com",
                        Class = "Bachelor",
                        Section = "B",
                        CourseId = courses[3].Id
                    },
                    new Student
                    {
                        Name = "Sujan Nepal",
                        Gender = "Male",
                        Address = "Koteshwor, Kathmandu",
                        PhoneNumber = "9881234571",
                        Email = "sujan.nepal@example.com",
                        Class = "Masters",
                        Section = "A",
                        CourseId = courses[4].Id
                    },
                    new Student
                    {
                        Name = "Priya Shrestha",
                        Gender = "Female",
                        Address = "Patan, Lalitpur",
                        PhoneNumber = "9891234572",
                        Email = "priya.shrestha@example.com",
                        Class = "Masters",
                        Section = "A",
                        CourseId = courses[0].Id
                    }
                };

                await context.Students.AddRangeAsync(students);
                await context.SaveChangesAsync();
            }
        }
    }
}
