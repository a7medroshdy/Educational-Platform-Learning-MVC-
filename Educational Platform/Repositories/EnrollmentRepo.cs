using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.Repositories.Interfaces;

namespace Educational_Platform.Repositories
{
    public class EnrollmentRepo:IEnrollmentRepo
    {
        ApplicationContext context;
        public EnrollmentRepo(ApplicationContext context)
        {
            this.context = context; 
        }

        public void Add(Enrollment obj)
        {
            context.Add(obj);
        }
        public void Update(Enrollment obj)
        {
            context.Update(obj);
        }
        public void Delete(int Id)
        {
            context.Remove(GetById(Id));
        }

        public Enrollment GetById(int Id)
        {
            return context.Enrollment.FirstOrDefault(e => e.EnrollmentId == Id);
        }
        public Enrollment GetByCourseIdAndStudentId(int CourseId,string StudentId)
        {
            return context.Enrollment.FirstOrDefault(e => e.StudentId == StudentId && e.CourseId == CourseId);
        }
        public List<Enrollment> GetAll()
        {
            return context.Enrollment.ToList();
        }

        public List<Course> GetByStudentId(string StudentId)
        {
            return context.Enrollment.Where(e=>e.StudentId==StudentId).Select(e=>e.Course).ToList();
        }
        
        public void Save()
        {
            context.SaveChanges();
        }

        public bool IsStudentEnrolledInCourse(int CourseId, string StudentId)
        {
            return context.Enrollment.Any(e=>e.CourseId==CourseId && e.StudentId==StudentId);   
        }
    }
}
