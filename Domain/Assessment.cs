
public class Assessment
{
    public GradeType Type { get; }
    public double Score { get; }

    public Assessment(GradeType type, double score)
    {
        Type = type;
        Score = Math.Clamp(score, 0, 100);
    }

    public override string ToString() => $"{Type}: {Score:0.00}";
}