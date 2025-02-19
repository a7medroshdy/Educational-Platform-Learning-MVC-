using System.ComponentModel.DataAnnotations;

namespace Educational_Platform.ViewModels
{
    public class AddCourseViewModel
    {
        [Required(ErrorMessage = ("Title is required"))]
        [RegularExpression(@"^.{2,20}$", ErrorMessage = "Title must be 2-20 characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = ("Description is required"))]
        [RegularExpression(@"^.{3,300}$", ErrorMessage = "Description must be 3-300 characters")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Teacher Email is required")]
        [Display(Name = "Teacher Email")]
        [DataType(DataType.EmailAddress)]
        public string TeacherEmail { get; set; }
    }
}
