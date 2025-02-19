using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.Repositories.Interfaces;

namespace Educational_Platform.Repositories
{
    public class CourseRepo : ICourseRepo
    {
        ApplicationContext context;


        public CourseRepo(ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(Course obj)
        {
            context.Add(obj);
        }
        
        public void Update(Course obj)
        {
            context.Update(obj);
        }
        
        public void Delete(int Id)
        {
            context.Remove(GetById(Id));
        }

        public List<Course> GetAll()
        {
            return context.Courses.ToList();
        }
        
        public Course GetById(int Id)
        {
            return context.Courses.FirstOrDefault(c => c.CourseId == Id);
        }

        
        public Course GetByName(string CourseName)
        {
            return context.Courses.FirstOrDefault(c=>c.Title == CourseName); 
        }

        public void Save()
        {
            context.SaveChanges();  
        }
    }
}
