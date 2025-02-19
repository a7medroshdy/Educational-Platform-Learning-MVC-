using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educational_Platform.Models
{
    public class Course
    {
        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage =("Title is required"))]
        [RegularExpression(@"^.{2,20}$",ErrorMessage ="Title must be 2-20 characters")]
        public string Title { get; set; }


        [Required(ErrorMessage =("Description is required"))]
        [RegularExpression(@"^.{3,300}$",ErrorMessage ="Description must be 3-300 characters")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Teacher Email is required")]
        [Display(Name = "Teacher Email")]
        public string TeacherEmail { get; set; }


        [Required(ErrorMessage ="Teacher id is required")]
        [ForeignKey("ApplicationUser")]
        public string TeacherId { get; set; }


        public ApplicationUser ApplicationUser { get; set; }

        public IList<Lesson> Lessons { get; set; } = new List<Lesson>();
        public IList<Quiz>? Quizzes { get; set; } 
        public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
