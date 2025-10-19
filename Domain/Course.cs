
public class Course
{
    public string Code { get; }
    public string Name { get; }
    public List<Group> Groups { get; } = new();

    public Course(string code, string name)
    {
        Code = code.Trim();
        Name = name.Trim();
    }

    public override string ToString() => $"{Name} ({Code})";
}
