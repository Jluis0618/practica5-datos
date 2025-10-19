
public class Teacher : Person
{
    public List<Course> Courses { get; } = new();
    public Teacher(string id, string fullName) : base(id, fullName) { }
}