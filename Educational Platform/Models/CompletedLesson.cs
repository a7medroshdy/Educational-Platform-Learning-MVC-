using Educational_Platform.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CompletedLesson
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("ApplicationUser")]
    public string StudentId { get; set; }

    [Required]
    [ForeignKey("Lesson")]
    public int LessonId { get; set; }
    public string CourseName { get; set; }

    //[Required]
    //[ForeignKey("Course")]
    //public int CourseId { get; set; }


    public Lesson Lesson { get; set; }
    //public Course Course { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
