using System.Data.Common;

namespace Lab1;

public interface ICourse
{
    public string Name { get; set; }
    public int ID { get; set; }
    public string Type { get; set; }
    Dictionary<int, Student> Students { get; set; }
    Dictionary<int, Teacher> Teachers { get; set; }
}

public class OnlineCourse : ICourse
{
    public string Name { get; set; }
    public int ID { get; set; }
    public string Type { get; set; }
    public string Platform { get; set; }
    public string URL { get; set; }
    public Dictionary<int, Student> Students { get; set; } = new();
    public Dictionary<int, Teacher> Teachers { get; set; } = new();
    public OnlineCourse(string name, string platform, string url, int id)
    {
        Name = name;
        Platform = platform;
        URL = url;
        ID = id;
        Type = "Online";
    }
}
public class OfflineCourse : ICourse
{
    public string Name { get; set; }
    public int ID { get; set; }
    public string LectureHall { get; set; }
    public string CourseAdress { get; set; }
    public string Type { get; set; }
    public Dictionary<int, Student> Students { get; set; } = new();
    public Dictionary<int, Teacher> Teachers { get; set; } = new();
    public OfflineCourse(string name, string lecturehall, string courseadress, int id)
    {
        Name = name;
        LectureHall = lecturehall;
        CourseAdress = courseadress;
        ID = id;
        Type = "Offline";
    }
}
public class CourseStorage
{
    public Dictionary<int, ICourse> _courseStorage = new();
    private int _lastID = 1;

    public OnlineCourse AddOnlineCourse(string name, string platform, string url)
    {
        var onlineCourse = new OnlineCourse(name, platform, url, _lastID++);
        _courseStorage.Add(onlineCourse.ID, onlineCourse);
        return onlineCourse;
    }
    public OfflineCourse AddOfflineCourse(string name, string lecturehall, string courseadress)
    {
        var offlineCourse = new OfflineCourse(name, lecturehall, courseadress, _lastID++);
        _courseStorage.Add(offlineCourse.ID, offlineCourse);
        return offlineCourse;
    }
    public void AddStudentOnCourse(int CourseID, IPerson person)
    {
        if (person is not Student student)
        {
            throw new Exception("Is not student");
        }
        var course = _courseStorage[CourseID];
        course.Students.Add(student.ID, student);
    }
    public void AddTeacherOnCourse(int CourseID, IPerson person)
    {
        if (person is not Teacher teacher)
        {
            throw new Exception("Is not teacher");
        }
        var course = _courseStorage[CourseID];
        teacher.TeacherCourses.Add(CourseID, course);
        course.Teachers.Add(teacher.ID, teacher);
    }
    public Dictionary<int, Student> GetStudentByCourseID(int CourseID)
    {
        if (!_courseStorage.ContainsKey(CourseID))
        {
            throw new Exception("Course not found");
        }
        var course = _courseStorage[CourseID];
        if (course.Students.Count() == 0)
        {
            Console.WriteLine("Course have no students");
        }
        else
        {
            Console.WriteLine($"На курсе с id: {CourseID} числятся:");
            foreach (var student in course.Students.Values)
            {
                Console.WriteLine($"ID: {student.ID} Name: {student.Name} Type: {student.Age}");
            }
        }
        return course.Students;
    }
    public void RemoveCourse(int id)
    {
        if (!_courseStorage.ContainsKey(id))
        {
            throw new Exception("Course not found");
        }
        var course = _courseStorage[id];
        foreach (var teacher in course.Teachers.Values)
        {
            teacher.TeacherCourses.Remove(id);
        }
        _courseStorage.Remove(id);
    }
}