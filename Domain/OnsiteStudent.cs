
public class OnsiteStudent : Student
{
    public OnsiteStudent(string id, string fullName) : base(id, fullName) { }

    public override string Modality => "Presencial";

    public override double FinalGrade() => base.FinalGrade();
}
