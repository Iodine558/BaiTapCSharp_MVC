namespace BaiTapCSharp_MVC.Models;

public class Enrollment
{
    public int Id { get; set; }

    public required string StudentId { get; set; }
    public Student? Student { get; set; }

    public required string ClassSectionId { get; set; }
    public ClassSection? ClassSection { get; set; }
}
