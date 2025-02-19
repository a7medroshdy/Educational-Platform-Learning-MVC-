using Educational_Platform.Models;

namespace Educational_Platform.Repositories.Interfaces
{
    public interface IQuizRepo
    {
        public void Add(Quiz obj);
        public void Delete(int Id);
        public Quiz GetById(int Id);
        public Quiz GetByName(string Title , string CourseName);
        public List<Quiz> GetByCourseName(string CourseName);
        public void Save();

    }
}
