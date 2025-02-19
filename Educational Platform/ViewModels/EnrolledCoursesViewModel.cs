using System.ComponentModel.DataAnnotations;

namespace Educational_Platform.ViewModels
{
    public class EnrolledCoursesViewModel
    {
        public string Title { get; set; }
        public int CourseId { get; set; }

        public string Description { get; set; }

        public string TeacherEmail { get; set; }

        public double Progress { get; set; }
    }
}
