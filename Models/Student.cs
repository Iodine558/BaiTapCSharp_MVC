namespace BaiTapCSharp_MVC.Models
{
    public class Student
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
