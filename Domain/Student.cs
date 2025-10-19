namespace University.App.Domain;

public abstract class Student : Person
{
    protected readonly List<Assessment> _assessments = new();

    protected Student(string id, string fullName) : base(id, fullName) { }

    public IReadOnlyList<Assessment> Assessments => _assessments.AsReadOnly();

    public void AddAssessment(Assessment a) => _assessments.Add(a);

    public double Average() => _assessments.Count == 0 ? 0 : _assessments.Average(a => a.Score);

    public abstract string Modality { get; }

    public virtual double FinalGrade() => Average();

    public virtual string Summary()
        => $"{FullName} [{Modality}] - Evaluaciones: {_assessments.Count}, Promedio: {FinalGrade():0.00}";
}
