namespace BaiTapCSharp_MVC.Models;

public class SchoolDataViewModel
{
    public List<Student> Students { get; set; } = [];
    public List<Teacher> Teachers { get; set; } = [];
    public List<Course> Courses { get; set; } = [];
    public List<CourseEnrollmentViewModel> Enrollments { get; set; } = [];
}

public class CourseEnrollmentViewModel
{
    public string CourseName { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
}
