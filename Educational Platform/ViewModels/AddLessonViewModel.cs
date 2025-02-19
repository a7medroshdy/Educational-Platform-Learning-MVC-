using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Educational_Platform.ViewModels
{
    public class AddLessonViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [RegularExpression(@"^.{3,20}$", ErrorMessage = "Title must be 3-20 characters and must contain only letters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Content is required")]
        [RegularExpression(@"^.{3,300}$", ErrorMessage = "Title must be 3-300 characters and must contain only letters")]
        public string Content { get; set; }


        [Display(Name = "Course Title")]
        [Required(ErrorMessage = "Course name is required")]
        [RegularExpression(@"^.{2,20}$",ErrorMessage="Course name must be 2-20 characters")]
        public string CourseName { get; set; }
    }
}
