namespace BaiTapCSharp_MVC.Models;

public class ClassSection
{
    public required string Id { get; set; }
    public required string Name { get; set; }

    public required string CourseId { get; set; }
    public Course? Course { get; set; }

    public required string TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
