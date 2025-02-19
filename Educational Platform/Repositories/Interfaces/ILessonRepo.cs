using Educational_Platform.Models;

namespace Educational_Platform.Repositories.Interfaces
{
    public interface ILessonRepo
    {
        public void Add(Lesson obj);
        public void Update(Lesson obj);
        public void Delete(int Id);

        public Lesson GetById(int Id);
        public List<Lesson> GetByCourseName(string courseName);

        public void Save();

    }
}
