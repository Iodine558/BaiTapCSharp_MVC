namespace BaiTapCSharp_MVC.Models
{
    public class Teacher
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }

        public ICollection<ClassSection> ClassSections { get; set; } = new List<ClassSection>();
    }
}
