using Educational_Platform.Models;

namespace Educational_Platform.Repositories.Interfaces
{
    public interface IQuestionRepo
    {
        public void Add(Question obj); 
        public void Delete(int QuestionId);
        public Question GetById(int QuestionId);
        //public Question GetByName(string QuestionName);
        public List<Question> GetByQuizName(string QuizName , string CourseName);
        public void Save();

    }
}
