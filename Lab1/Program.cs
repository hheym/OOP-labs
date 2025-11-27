using Microsoft.VisualBasic;

namespace Lab1;

public class Program
{
    public static void Main(string[] args)
    {
        var personStorage = new PersonStorage();
        var courseStorage = new CourseStorage();
        personStorage.AddStudent("Леха", 12);
        personStorage.AddTeacher("Leha", 21);
        Console.WriteLine($"{personStorage._person[1].ID} {personStorage._person[1].Name} {personStorage._person[1].Age}");
        Console.WriteLine($"{personStorage._person[2].ID} {personStorage._person[2].Name} {personStorage._person[2].Age}");
        courseStorage.AddOfflineCourse("IT", "Audience 211", "Kronva 49");
        courseStorage.AddOnlineCourse("ML", "Zoom", "https://zoom/123/1233");
        Console.WriteLine($"{courseStorage._courseStorage[1].ID} {courseStorage._courseStorage[1].Name} {courseStorage._courseStorage[1].Type}");
        Console.WriteLine($"{courseStorage._courseStorage[2].ID} {courseStorage._courseStorage[2].Name} {courseStorage._courseStorage[2].Type}");
        courseStorage.AddTeacherOnCourse(1, personStorage._person[2]);
        courseStorage.AddStudentOnCourse(2, personStorage._person[1]);
        courseStorage.AddTeacherOnCourse(2, personStorage._person[2]);
        personStorage.GetCoursesByTeacherID(2);
        courseStorage.GetStudentByCourseID(2);
        courseStorage.RemoveCourse(2);
        personStorage.GetCoursesByTeacherID(2);
        courseStorage.GetStudentByCourseID(2);
    }
}
/*
Интерфейс курсы: Добавлять, удалять курсы, назначить преподов на курсы, информация о студентах
Интерфейс онлайн курсов: типо дистант, еще какое нибудь
Интерфейс оффлайн курсов: типо доп баллы или чето такое
Получить все курсы, которые ведет преподователь
Покрыть юнит тестами

*/