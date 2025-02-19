using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educational_Platform.Models
{
    public class Quiz
    {
        [Required]
        public int QuizId { get; set; }


        [Required(ErrorMessage ="Title is required")]
        [RegularExpression(@"^[a-zA-Z0-9\s]{3,20}$",ErrorMessage ="Title must be 3-20 characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [RegularExpression(@"^.{3,100}$",ErrorMessage = "Description must be 3-100 characters")]
        public string Description { get; set; }


        [ForeignKey("Course")]
        [Display(Name ="Course Id")]
        [Required(ErrorMessage = "Course Id is required")]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public IList<Question> Questions { get; set; } = new List<Question>();
    }
}
