using MongoDB.Driver;
using MongoDBApis.Models;

namespace MongoDBApis.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IStudentStoreDatabaseSettings settings, IMongoClient mongoClient) {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentCoursesCollectionName);
        }
        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        public List<Student> Get()
        {
            return _students.Find(s => true).ToList();
        }

        public Student Get(string id)
        {
            return _students.Find(s => s.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _students.DeleteOne(s => s.Id == id);
        }

        public void Update(string id, Student student)
        {
            _students.ReplaceOne(s => s.Id == id, student);
        }
    }
}
