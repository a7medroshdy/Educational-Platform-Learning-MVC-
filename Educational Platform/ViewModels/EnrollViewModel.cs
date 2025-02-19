using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Educational_Platform.Models;

namespace Educational_Platform.ViewModels
{
    public class EnrollViewModel
    {

        [Required]
        [Display(Name = "Student Email")]
        [DataType(DataType.EmailAddress)]
        public string StudentEmail { get; set; }


        [Required]
        [ForeignKey("Course")]
        [Display(Name = "Course Id")]
        public IList<Course> Courses { get; set; } = new List<Course>();

    }
}
