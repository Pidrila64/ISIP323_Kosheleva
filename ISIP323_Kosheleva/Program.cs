using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityManagementSystem
{
    public enum Department
    {
        ComputerScience, Mathematics, Physics, Engineering, Business, Arts
    }

    public class Person
    {
        public string Name { get; protected set; }
        public int Age { get; protected set; }
        public string ContactInfo { get; protected set; }

        public Person(string name, int age, string contactInfo)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя не может быть пустым");
            if (age <= 0 || age > 120) throw new ArgumentException("Возраст должен быть от 1 до 120 лет");
            if (string.IsNullOrWhiteSpace(contactInfo)) throw new ArgumentException("Контактная информация не может быть пустой");

            Name = name;
            Age = age;
            ContactInfo = contactInfo;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Имя: {Name}\nВозраст: {Age}\nКонтактная информация: {ContactInfo}");
        }
    }

    public class Student : Person
    {
        private static int nextStudentId = 1;
        public int StudentId { get; private set; }
        public List<Course> Courses { get; private set; }
        public Dictionary<Course, List<double>> Grades { get; private set; }
        public double AverageGrade { get; private set; }

        public Student(string name, int age, string contactInfo) : base(name, age, contactInfo)
        {
            StudentId = nextStudentId++;
            Courses = new List<Course>();
            Grades = new Dictionary<Course, List<double>>();
            AverageGrade = 0.0;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Студент ID: {StudentId}");
            base.DisplayInfo();
            Console.WriteLine($"Средний балл: {AverageGrade:F2}\nКоличество курсов: {Courses.Count}");
        }

        public bool EnrollInCourse(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            if (Courses.Contains(course)) return false;

            if (course.AddStudent(this))
            {
                Courses.Add(course);
                Grades[course] = new List<double>();
                return true;
            }
            return false;
        }

        public bool LeaveCourse(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            if (!Courses.Contains(course)) return false;

            if (course.RemoveStudent(this))
            {
                Courses.Remove(course);
                Grades.Remove(course);
                CalculateAverageGrade();
                return true;
            }
            return false;
        }

        public void AddGrade(Course course, double grade)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            if (grade < 0 || grade > 100) throw new ArgumentException("Оценка должна быть от 0 до 100");
            if (!Courses.Contains(course)) throw new InvalidOperationException("Студент не записан на этот курс");

            if (!Grades.ContainsKey(course)) Grades[course] = new List<double>();
            Grades[course].Add(grade);
            CalculateAverageGrade();
        }

        public void CalculateAverageGrade()
        {
            if (Grades.Count == 0) { AverageGrade = 0; return; }

            double total = 0;
            int count = 0;
            foreach (var courseGrades in Grades.Values)
            {
                total += courseGrades.Sum();
                count += courseGrades.Count;
            }
            AverageGrade = count > 0 ? total / count : 0;
        }
    }

    public class Teacher : Person
    {
        private static int nextTeacherId = 1;
        public int TeacherId { get; private set; }
        public Department Department { get; private set; }
        public List<Course> TeachingCourses { get; private set; }

        public Teacher(string name, int age, string contactInfo, Department department) : base(name, age, contactInfo)
        {
            TeacherId = nextTeacherId++;
            Department = department;
            TeachingCourses = new List<Course>();
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Преподаватель ID: {TeacherId}");
            base.DisplayInfo();
            Console.WriteLine($"Отдел: {Department}\nКоличество преподаваемых курсов: {TeachingCourses.Count}");
        }

        public void AddCourse(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            if (TeachingCourses.Contains(course)) return;

            course.Instructor = this;
            TeachingCourses.Add(course);
        }

        public void GradeStudent(Student student, Course course, double grade)
        {
            if (student == null || course == null) throw new ArgumentNullException();
            if (!TeachingCourses.Contains(course)) throw new InvalidOperationException("Преподаватель не ведет этот курс");
            if (!course.EnrolledStudents.Contains(student)) throw new InvalidOperationException("Студент не записан на этот курс");

            student.AddGrade(course, grade);
        }
    }

    public class Course
    {
        private static int nextCourseId = 1;
        public int CourseId { get; private set; }
        public string CourseName { get; private set; }
        public string Description { get; private set; }
        public Teacher Instructor { get; set; }
        public List<Student> EnrolledStudents { get; private set; }
        public int MaxStudents { get; private set; }

        public Course(string courseName, string description, int maxStudents)
        {
            if (string.IsNullOrWhiteSpace(courseName)) throw new ArgumentException("Название курса не может быть пустым");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Описание курса не может быть пустым");
            if (maxStudents <= 0) throw new ArgumentException("Максимальное количество студентов должно быть положительным");

            CourseId = nextCourseId++;
            CourseName = courseName;
            Description = description;
            MaxStudents = maxStudents;
            EnrolledStudents = new List<Student>();
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Курс ID: {CourseId}");
            Console.WriteLine($"Название: {CourseName}");
            Console.WriteLine($"Описание: {Description}");
            Console.WriteLine($"Преподаватель: {Instructor?.Name ?? "Не назначен"}");
            Console.WriteLine($"Студентов: {EnrolledStudents.Count}/{MaxStudents}");
        }

        public bool AddStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (EnrolledStudents.Contains(student) || IsFull()) return false;

            EnrolledStudents.Add(student);
            return true;
        }

        public bool RemoveStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            return EnrolledStudents.Remove(student);
        }

        public bool IsFull() => EnrolledStudents.Count >= MaxStudents;
    }

    public class University
    {
        public List<Student> Students { get; private set; }
        public List<Teacher> Teachers { get; private set; }
        public List<Course> Courses { get; private set; }

        public University()
        {
            Students = new List<Student>();
            Teachers = new List<Teacher>();
            Courses = new List<Course>();
        }

        public void AddStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            Students.Add(student);
        }

        public void AddTeacher(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));
            Teachers.Add(teacher);
        }

        public void AddCourse(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            Courses.Add(course);
        }

        public Student FindStudentById(int studentId) => Students.FirstOrDefault(s => s.StudentId == studentId);
        public Teacher FindTeacherById(int teacherId) => Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
        public Course FindCourseById(int courseId) => Courses.FirstOrDefault(c => c.CourseId == courseId);

        // Добавленные методы для отображения
        public void DisplayAllStudents()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("В университете нет студентов");
                return;
            }

            Console.WriteLine("Все студенты университета:");
            foreach (var student in Students.OrderBy(s => s.StudentId))
            {
                student.DisplayInfo();
                Console.WriteLine("---");
            }
        }

        public void DisplayAllTeachers()
        {
            if (Teachers.Count == 0)
            {
                Console.WriteLine("В университете нет преподавателей");
                return;
            }

            Console.WriteLine("Все преподаватели университета:");
            foreach (var teacher in Teachers.OrderBy(t => t.TeacherId))
            {
                teacher.DisplayInfo();
                Console.WriteLine("---");
            }
        }

        public void DisplayAllCourses()
        {
            if (Courses.Count == 0)
            {
                Console.WriteLine("В университете нет курсов");
                return;
            }

            Console.WriteLine("Все курсы университета:");
            foreach (var course in Courses.OrderBy(c => c.CourseId))
            {
                course.DisplayCourseInfo();
                Console.WriteLine("---");
            }
        }
    }

    public class MenuManager
    {
        private University university = new University();

        public void DisplayMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ УНИВЕРСИТЕТОМ ===");
                Console.WriteLine("1. Управление студентами\n2. Управление преподавателями\n3. Управление курсами\n4. Запись на курсы\n0. Выход");
                Console.Write("Выберите опцию: ");

                switch (Console.ReadLine())
                {
                    case "1": HandleStudentManagement(); break;
                    case "2": HandleTeacherManagement(); break;
                    case "3": HandleCourseManagement(); break;
                    case "4": HandleEnrollment(); break;
                    case "0": return;
                    default: WaitForKey("Неверный выбор"); break;
                }
            }
        }

        private void HandleStudentManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ СТУДЕНТАМИ ===");
                Console.WriteLine("1. Добавить студента\n2. Просмотреть всех студентов\n0. Назад");

                switch (Console.ReadLine())
                {
                    case "1": AddStudent(); break;
                    case "2": university.DisplayAllStudents(); WaitForKey(); break;
                    case "0": return;
                    default: WaitForKey("Неверный выбор"); break;
                }
            }
        }

        private void HandleTeacherManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ ПРЕПОДАВАТЕЛЯМИ ===");
                Console.WriteLine("1. Добавить преподавателя\n2. Просмотреть всех преподавателей\n0. Назад");

                switch (Console.ReadLine())
                {
                    case "1": AddTeacher(); break;
                    case "2": university.DisplayAllTeachers(); WaitForKey(); break;
                    case "0": return;
                    default: WaitForKey("Неверный выбор"); break;
                }
            }
        }

        private void HandleCourseManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ КУРСАМИ ===");
                Console.WriteLine("1. Добавить курс\n2. Просмотреть все курсы\n0. Назад");

                switch (Console.ReadLine())
                {
                    case "1": AddCourse(); break;
                    case "2": university.DisplayAllCourses(); WaitForKey(); break;
                    case "0": return;
                    default: WaitForKey("Неверный выбор"); break;
                }
            }
        }

        private void HandleEnrollment()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАПИСЬ НА КУРСЫ ===");

            try
            {
                Console.Write("Введите ID студента: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId)) { WaitForKey("Неверный формат ID"); return; }

                Console.Write("Введите ID курса: ");
                if (!int.TryParse(Console.ReadLine(), out int courseId)) { WaitForKey("Неверный формат ID"); return; }

                var student = university.FindStudentById(studentId);
                var course = university.FindCourseById(courseId);

                if (student != null && course != null)
                    student.EnrollInCourse(course);
                else
                    Console.WriteLine("Студент или курс не найден");
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }

            WaitForKey();
        }

        private void AddStudent()
        {
            
        }

        private void AddTeacher()
        {
            
        }

        private void AddCourse()
        {
            
        }

        private void WaitForKey(string message = null)
        {
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            new MenuManager().DisplayMainMenu();
        }
    }
}