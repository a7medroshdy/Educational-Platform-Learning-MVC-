using Educational_Platform.Data;
using Educational_Platform.Models;

namespace Educational_Platform.Repositories.Interfaces
{
    public interface IEnrollmentRepo
    {
        public void Add(Enrollment obj);
        public void Update(Enrollment obj);
        public void Delete(int Id);
        public List<Enrollment> GetAll();
        public Enrollment GetById(int Id);
        public bool IsStudentEnrolledInCourse(int CourseId, string StudentId);
        public Enrollment GetByCourseIdAndStudentId(int CourseId, string StudentId);
        public List<Course> GetByStudentId(string StudentId);
        public void Save();

    }
}
