using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educational_Platform.ViewModels
{
    public class AddQuizViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [RegularExpression(@"^[a-zA-Z0-9\s]{3,20}$", ErrorMessage = "Title must be 3-20 characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [RegularExpression(@"^.{3,100}$", ErrorMessage = "Description must be 3-100 characters")]
        public string Description { get; set; }


        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Course Name is required")]
        [RegularExpression(@"^.{2,20}$", ErrorMessage = "Course Name must be 2-20 characters")]
        public string CourseName { get; set; }
    }
}
