
using Lab1;
namespace Lab1.Tests;

public class UnitTest1
{
    [Fact]
    public void TestOnlineCourseAsserts()
    {
        var storage = new CourseStorage();
        var course = storage.AddOnlineCourse("ML", "Zoom", "https://zoom/123/1233");

        Assert.Equal(1, course.ID);
        Assert.Equal("ML", course.Name);
        Assert.Equal("Online", course.Type);
        Assert.Same(course, storage._courseStorage[course.ID]);
    }

    [Fact]
    public void TestAddOfflineCourseAsserts()
    {
        var storage = new CourseStorage();
        var course = storage.AddOfflineCourse("IT", "Audience 211", "Kronva 49");

        Assert.Equal(1, course.ID);
        Assert.Equal("IT", course.Name);
        Assert.Equal("Offline", course.Type);
        Assert.Same(course, storage._courseStorage[course.ID]);
    }

    [Fact]
    public void AddStudentOnCourseAsserts()
    {
        var personStorage = new PersonStorage();
        var courseStorage = new CourseStorage();
        var student = personStorage.AddStudent("Леха", 12);
        var course = courseStorage.AddOnlineCourse("ML", "Zoom", "https://zoom/123");

        courseStorage.AddStudentOnCourse(course.ID, student);

        var students = courseStorage.GetStudentByCourseID(course.ID);
        Assert.True(students.ContainsKey(student.ID));
        Assert.Same(student, students[student.ID]);
    }

    [Fact]
    public void AddTeacherOnCourseTest()
    {
        var personStorage = new PersonStorage();
        var courseStorage = new CourseStorage();
        var teacher = personStorage.AddTeacher("Leha", 21);
        var course = courseStorage.AddOfflineCourse("IT", "Audience 211", "Kronva 49");

        courseStorage.AddTeacherOnCourse(course.ID, teacher);

        Assert.True(teacher.TeacherCourses.ContainsKey(course.ID));
        Assert.Same(course, teacher.TeacherCourses[course.ID]);
        Assert.True(course.Teachers.ContainsKey(teacher.ID));
        Assert.Same(teacher, course.Teachers[teacher.ID]);
    }

    [Fact]
    public void RemoveCourseTest()
    {
        var personStorage = new PersonStorage();
        var courseStorage = new CourseStorage();
        var teacher = personStorage.AddTeacher("Leha", 21);
        var course = courseStorage.AddOfflineCourse("IT", "Audience 211", "Kronva 49");

        courseStorage.AddTeacherOnCourse(course.ID, teacher);
        courseStorage.RemoveCourse(course.ID);

        Assert.False(courseStorage._courseStorage.ContainsKey(course.ID));
        Assert.False(teacher.TeacherCourses.ContainsKey(course.ID));
    }

    [Fact]
    public void GetStudentByCourseIDTest()
    {
        var storage = new CourseStorage();

        Assert.Throws<Exception>(() => storage.GetStudentByCourseID(999));
    }

    [Fact]
    public void GetCoursesByTeacherIDTest()
    {
        var storage = new PersonStorage();

        Assert.Throws<Exception>(() => storage.GetCoursesByTeacherID(999));
    }
}
