namespace BaiTapCSharp_MVC.Models
{
    public class Course
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        public ICollection<ClassSection> ClassSections { get; set; } = new List<ClassSection>();
    }
}
