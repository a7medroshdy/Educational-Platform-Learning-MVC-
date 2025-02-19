using Educational_Platform.Models;

namespace Educational_Platform.Repositories.Interfaces
{
    public interface ICourseRepo
    {
        public void Add(Course obj);

        public void Update(Course obj);

        public void Delete(int Id);

        public List<Course> GetAll();

        public Course GetById(int Id);
        public Course GetByName(string CourseName);

        public void Save();
    }
}
