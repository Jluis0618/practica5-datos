
public class Person
{
    public string Id { get; }
    public string FullName { get; private set; }

    public Person(string id, string fullName)
    {
        Id = id.Trim();
        FullName = fullName.Trim();
    }

    public override string ToString() => $"{FullName} ({Id})";
}