using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.Repositories.Interfaces;

namespace Educational_Platform.Repositories
{
    public class QuestionRepo : IQuestionRepo
    {
        ApplicationContext context;
        IQuizRepo quizRepo;
        ICourseRepo courseRepo;
        public QuestionRepo(ApplicationContext context , IQuizRepo quizRepo , ICourseRepo courseRepo)
        {
            this.context = context;
            this.quizRepo = quizRepo;
            this.courseRepo = courseRepo;
        }

        public void Add(Question obj)
        {
            context.Add(obj);
        }

        public void Delete(int QuestionId)
        {
            context.Remove(GetById(QuestionId));
        }

        public Question GetById(int QuestionId)
        {
            return context.Questions.FirstOrDefault(q => q.QuestionId == QuestionId);
        }
        
        public List<Question> GetByQuizName(string QuizName , string CourseName)
        {
            Quiz quiz = quizRepo.GetByName(QuizName , CourseName);
            return context.Questions.Where(q => q.QuizId == quiz.QuizId).ToList();
        }

        public void Save()
        {
            context.SaveChanges();  
        }
    }
}
