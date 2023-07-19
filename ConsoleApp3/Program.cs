// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;


public class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();
        int option;

        do
        {
            Console.WriteLine("1. Show students");
            Console.WriteLine("2. Add student");
            Console.WriteLine("3. Update student");
            Console.WriteLine("4. Remove student");
            Console.WriteLine("5. Find student by name");
            Console.WriteLine("0. Exit");
            Console.WriteLine("------------------------");
            Console.WriteLine("Please, select an option:");
            Console.WriteLine("------------------------");


            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("Please, enter a valid option:");
                Console.WriteLine("------------------------");
            }

            switch (option)
            {
                case 1:
                    MenuService.MenuShowStudents();
                    break;
                case 2:
                    MenuService.MenuAddStudent();
                    break;
                case 3:
                    MenuService.MenuUpdateStudent();
                    break;
                case 4:
                    MenuService.MenuRemoveStudent();
                    break;
                case 5:
                    MenuService.MenuFindStudentByName();
                    break;
                case 0:
                    Console.WriteLine("Bye");
                    break;
                default:
                    Console.WriteLine("No such option!");
                    break;
            }

        } while (option != 0);
    }
   
}

public class Student
{
    private static int counter = 0;
    public Student(string name, string surname,double grade)
    {
        Name = name;
        Surname = surname;
        Grade = grade;
        counter++;
    }
   
    public string Name { get; set; }
    public string Surname { get; set; }
    public double Grade { get; set; }
    public int ID { get; set; }
}

public class MenuService
{
    private static StudentService studentService = new StudentService();

    public static void MenuShowStudents()
    {
        var students = studentService.GetStudents();

        if (students.Count == 0)
        {
            Console.WriteLine("No students yet.");
            return;
        }

        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.ID} | Name: {student.Name} | Surname: {student.Surname} | Grade: {student.Grade}");
        }
    }

    public static void MenuAddStudent()
    {
        try
        {
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
           

            Console.WriteLine("Enter surname:");
            string surname = Console.ReadLine();
           

            Console.WriteLine("Enter grade:");
            double grade = double.Parse(Console.ReadLine());

            studentService.AddStudent(name, surname, grade);
          
            
            Console.WriteLine("Added student successfuly!");
           
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oops, error. {ex.Message}");
        }
    }

    public static void MenuUpdateStudent()
    {
        Console.WriteLine("Please enter ID");
        int ID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please enter new Name");
        var name = Console.ReadLine();
        Console.WriteLine("Please enter new Surname");
        var surname = Console.ReadLine();
        Console.WriteLine("Please enter new Grade");
        var grade = double.Parse(Console.ReadLine());

       

       
    }

    public static void MenuRemoveStudent()
    {
        try
        {
            Console.WriteLine("Enter student's ID:");
            int id= Convert.ToInt32(Console.ReadLine());

            studentService.RemoveStudent(id);

            Console.WriteLine("Deleted student successfuly!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oops, error. {ex.Message}");
        }
    }

    public static void MenuFindStudentByName()
    {
        try
        {
            Console.WriteLine("Please enter student's name");
            string Name = Console.ReadLine();
            studentService.FindStudentByName(Name);
            Console.WriteLine("Student finded:)");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Oops,error. {ex.Message} ");
        }
    
        
    }
}







public class StudentService
{
    private List<Student> students;

    public StudentService()
    {
        students = new();
    }

    public List<Student> GetStudents()
    {
        return students;
    }

    public void AddStudent(string name, string surname, double grade)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("Name can not be null");
        }
        if(string.IsNullOrEmpty(surname))
        {
            throw new Exception("Surname can not be null");
        }
        if (grade < 0) throw new Exception("Grade can not be less than 0");
        var student = new Student(name, surname, grade);

        students.Add(student);
        
          
    }

    public void RemoveStudent(int id)
    {
        if (id < 0) throw new Exception("Student's Id can not be less than 0");

        var existingStudent = students.FirstOrDefault(x => x.ID == id);

        if (existingStudent == null) throw new Exception("Not found!");

        students = students.Where(x => x.ID != id).ToList();
    }
    public void FindStudentByName(string name)
    {
        var existingStudent = students.FirstOrDefault(x =>x.Name == name);
        if (existingStudent == null) throw new Exception("Not found");
        students = students.Where(x => x.Name != name).ToList();
    }
    public void UpDateStudent(int id)
    {
        
    }
}



