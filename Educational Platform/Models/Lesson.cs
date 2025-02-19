using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educational_Platform.Models
{
    public class Lesson
    {
        [Required]
        public int LessonId { get; set; }

        
        [Required(ErrorMessage ="Title is required")]
        [RegularExpression(@"^[a-zA-Z\s]{3,20}$",ErrorMessage ="Title must be 3-20 characters and must contain only letters")]
        public string  Title { get; set; }


        [Required(ErrorMessage = "Content is required")]
        [RegularExpression(@"^.{3,300}$", ErrorMessage = "Title must be 3-300 characters and must contain only letters")]
        public string Content { get; set; }


        [ForeignKey("Course")]
        [Display(Name = "Course Id")]
        [Required(ErrorMessage ="Course Id is required")]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public IList<CompletedLesson> CompletedLessons { get; set; } = new List<CompletedLesson>();

    }
}
