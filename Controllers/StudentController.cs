using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prajjwal_Ghimire_SMS.Data;
using Prajjwal_Ghimire_SMS.Models;

namespace Prajjwal_Ghimire_SMS.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(IRepository<Student> studentRepository, IRepository<Course> courseRepository, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepository.GetAllAsync();
            return View(students);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = new SelectList(await _courseRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Student student, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "students");
                    Directory.CreateDirectory(uploadsFolder);
                    
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    
                    student.ImagePath = "/images/students/" + uniqueFileName;
                }
                
                await _studentRepository.AddAsync(student);
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Courses = new SelectList(await _courseRepository.GetAllAsync(), "Id", "Name", student.CourseId);
            return View(student);
        }

        // GET: Student/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            
            ViewBag.Courses = new SelectList(await _courseRepository.GetAllAsync(), "Id", "Name", student.CourseId);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Student student, IFormFile imageFile)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(student.ImagePath))
                    {
                        string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, student.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "students");
                    Directory.CreateDirectory(uploadsFolder);
                    
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    
                    student.ImagePath = "/images/students/" + uniqueFileName;
                }
                
                await _studentRepository.UpdateAsync(student);
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Courses = new SelectList(await _courseRepository.GetAllAsync(), "Id", "Name", student.CourseId);
            return View(student);
        }

        // GET: Student/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            
            // Delete image if exists
            if (student != null && !string.IsNullOrEmpty(student.ImagePath))
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, student.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            
            await _studentRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
