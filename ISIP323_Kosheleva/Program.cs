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
            
        }

        public void AddCourse(Course course)
        {
            
        }

        public void GradeStudent(Student student, Course course, double grade)
        {
            
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
            
        }

        public void AddTeacher(Teacher teacher)
        {
            
        }

        public void AddCourse(Course course)
        {
            
        }

        public Student FindStudentById(int studentId) => Students.FirstOrDefault(s => s.StudentId == studentId);
        public Teacher FindTeacherById(int teacherId) => Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
        public Course FindCourseById(int courseId) => Courses.FirstOrDefault(c => c.CourseId == courseId);

        public void DisplayAllStudents()
        {
            
        }

        public void DisplayAllTeachers()
        {
            
        }

        public void DisplayAllCourses()
        {
            
        }
    }

    public class MenuManager
    {
        private University university = new University();

        public void DisplayMainMenu()
        {
            
        }

        private void HandleStudentManagement()
        {
           
        }

        private void HandleTeacherManagement()
        {
           
        }

        private void HandleCourseManagement()
        {
            
        }

        private void HandleEnrollment()
        {
            
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