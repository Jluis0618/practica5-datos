namespace University.App.Domain;

public class Group
{
    public string GroupCode { get; }
    public List<Student> Students { get; } = new();

    public Group(string groupCode) => GroupCode = groupCode.Trim();

    public double PassRate(double passing = 70.0)
    {
        if (Students.Count == 0) return 0;
        int pass = Students.Count(s => s.FinalGrade() >= passing);
        return (double)pass * 100.0 / Students.Count;
    }

    public string Report()
    {
        var lines = new List<string> { $"Grupo {GroupCode} - Estudiantes: {Students.Count}" };
        foreach (var s in Students.OrderBy(s => s.FullName))
        {
            var grades = s.Assessments.Count == 0 ? "Sin notas" : string.Join(", ", s.Assessments.Select(a => a.ToString()));
            lines.Add($" - {s.Summary()} | {grades}");
        }
        lines.Add($"% Aprobados (≥70): {PassRate():0.00}%");
        return string.Join(Environment.NewLine, lines);
    }
}
