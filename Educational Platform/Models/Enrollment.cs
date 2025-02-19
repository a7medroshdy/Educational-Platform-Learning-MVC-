using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educational_Platform.Models
{
    public class Enrollment
    {
        [Required]
        public int EnrollmentId { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.Now;   


        [Required]
        [ForeignKey("ApplicationUser")]
        [Display(Name ="Student Id")]
        public string StudentId { get; set; }


        [Required]
        [ForeignKey("Course")]
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }

        //public int CompletedLessons { get; set; } = 0;

        public Course Course { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
