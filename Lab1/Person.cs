using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Cryptography;

namespace Lab1;

public interface IPerson
{
    string Name { get; set; }
    int Age { get; set; }
    int ID { get; set; }
}

public class Student : IPerson
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int ID { get; set; }
    public Student(string name, int age, int id)
    {
        Name = name;
        Age = age;
        ID = id;
    }
}

public class Teacher : IPerson
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int ID { get; set; }
    public Dictionary<int, ICourse> TeacherCourses { get; set; } = new();
    public Teacher(string name, int age, int id)
    {
        Name = name;
        Age = age;
        ID = id;
    }
}

public class PersonStorage
{
    public Dictionary<int, IPerson> _person = new();
    public int _lastID = 1;
    public Student AddStudent(string name, int age)
    {
        var student = new Student(name, age, _lastID++);
        _person.Add(student.ID, student);
        return student;
    }
    public Teacher AddTeacher(string name, int age)
    {
        var teacher = new Teacher(name, age, _lastID++);
        _person.Add(teacher.ID, teacher);
        return teacher;
    }
    public Dictionary<int, ICourse> GetCoursesByTeacherID(int id)
    {
        if (!_person.ContainsKey(id))
        {
            throw new Exception("Person not found");
        }
        var person = _person[id];
        if (person is not Teacher teacher)
        {
            throw new Exception("Person is not teacher");
        }
        if (teacher.TeacherCourses.Count() == 0)
        {
            Console.WriteLine("No courses found");
        }
        else
        {
            Console.WriteLine($"Учитель с {id} id преподает:");
            foreach(var course in teacher.TeacherCourses)
            {
                Console.WriteLine($"ID: {course.Value.ID} Name: {course.Value.Name} Type: {course.Value.Type}");
            }
        }
        return teacher.TeacherCourses;
    }
}