namespace BaiTapCSharp_MVC.Models
{
    public class Course
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
