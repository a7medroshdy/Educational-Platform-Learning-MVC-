using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.Repositories.Interfaces;

namespace Educational_Platform.Repositories
{
    public class QuizRepo : IQuizRepo
    {
        ApplicationContext context;
        ICourseRepo courseRepo;

        public QuizRepo(ApplicationContext context , ICourseRepo courseRepo)
        {
            this.context = context;
            this.courseRepo = courseRepo;
        }

        public void Add(Quiz obj)
        {
            context.Add(obj);
        }

        public void Delete(int Id)
        {
            context.Remove(GetById(Id));    
        }

        public Quiz GetById(int Id)
        {
            return context.Quizzes.FirstOrDefault(q=>q.QuizId == Id);  
        }

        public Quiz GetByName(string Title , string CourseName)
        {
            Course course = courseRepo.GetByName(CourseName);
            return context.Quizzes.SingleOrDefault(q=>q.Title == Title && q.CourseId==course.CourseId);   
        }

        public List<Quiz> GetByCourseName(string CourseName)
        {
            Course course = courseRepo.GetByName(CourseName);
            return context.Quizzes.Where(q => q.CourseId == course.CourseId).ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
