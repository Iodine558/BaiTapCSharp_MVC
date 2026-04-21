namespace BaiTapCSharp_MVC.Models
{
    public class Student
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
