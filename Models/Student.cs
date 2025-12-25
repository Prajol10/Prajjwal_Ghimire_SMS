using System.ComponentModel.DataAnnotations;

namespace Prajjwal_Ghimire_SMS.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Gender { get; set; }
        
        public string Address { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public string Class { get; set; }
        
        public string Section { get; set; }
        
        public string? ImagePath { get; set; }
        
        // Foreign Key
        public int CourseId { get; set; }
        
        // Navigation property
        public Course Course { get; set; }
    }
}