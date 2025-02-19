using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.Repositories.Interfaces;

namespace Educational_Platform.Repositories
{
    public class LessonRepo : ILessonRepo
    {
        ApplicationContext context;
        ICourseRepo courseRepo; 

        public LessonRepo(ApplicationContext context, ICourseRepo courseRepo)
        {
            this.context = context;
            this.courseRepo = courseRepo;
        }

        public void Add(Lesson obj)
        {
            context.Add(obj);
        }

        public void Update(Lesson obj)
        {
            context.Update(obj);
        }

        public void Delete(int Id)
        {
            context.Remove(GetById(Id));
        }


        public Lesson GetById(int Id)
        {
            return context.Lessons.FirstOrDefault(l=>l.LessonId == Id);  
        }
        
        
        public List<Lesson> GetByCourseName(string courseName)
        {
            Course course = courseRepo.GetByName(courseName);
            return context.Lessons.Where(l => l.CourseId == course.CourseId).ToList(); 
        }


        public void Save()
        {
            context.SaveChanges();
        }
    }
}
