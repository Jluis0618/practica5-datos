using University.App.Common;
using University.App.Domain;

namespace University.App.Services;

public class CourseService : ICourseService
{
    private readonly List<Course> _courses = new();

    public IReadOnlyList<Course> ListCourses() => _courses.AsReadOnly();

    public OperationResult<Course> CreateCourse(string code, string name)
    {
        if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
            return OperationResult<Course>.Fail("Código y nombre son obligatorios.");
        if (_courses.Any(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase)))
            return OperationResult<Course>.Fail("Ya existe un curso con ese código.");

        var course = new Course(code, name);
        _courses.Add(course);
        return OperationResult<Course>.Ok(course, "Curso creado.");
    }

    public OperationResult<Group> CreateGroup(string courseCode, string groupCode)
    {
        var course = _courses.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));
        if (course is null) return OperationResult<Group>.Fail("Curso no encontrado.");

        if (course.Groups.Any(g => g.GroupCode.Equals(groupCode, StringComparison.OrdinalIgnoreCase)))
            return OperationResult<Group>.Fail("Ya existe ese grupo en el curso.");

        var group = new Group(groupCode);
        course.Groups.Add(group);
        return OperationResult<Group>.Ok(group, "Grupo creado.");
    }

    public OperationResult<Student> AddStudent(string courseCode, string groupCode, string id, string name, bool isOnsite)
    {
        var group = FindGroup(courseCode, groupCode);
        if (group is null) return OperationResult<Student>.Fail("Curso/grupo no encontrado.");

        if (group.Students.Any(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
            return OperationResult<Student>.Fail("Ya existe un estudiante con ese Id en el grupo.");

        Student s = isOnsite ? new OnsiteStudent(id, name) : new RemoteStudent(id, name);
        group.Students.Add(s);
        return OperationResult<Student>.Ok(s, "Estudiante agregado.");
    }

    public OperationResult<bool> AddGrade(string courseCode, string groupCode, string studentId, GradeType type, double score)
    {
        var group = FindGroup(courseCode, groupCode);
        if (group is null) return OperationResult<bool>.Fail("Curso/grupo no encontrado.");

        var student = group.Students.FirstOrDefault(s => s.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));
        if (student is null) return OperationResult<bool>.Fail("Estudiante no encontrado.");

        student.AddAssessment(new Assessment(type, score));
        return OperationResult<bool>.Ok(true, "Calificación registrada.");
    }

    public OperationResult<string> PrintGroupReport(string courseCode, string groupCode)
    {
        var group = FindGroup(courseCode, groupCode);
        if (group is null) return OperationResult<string>.Fail("Curso/grupo no encontrado.");
        return OperationResult<string>.Ok(group.Report(), "Reporte generado.");
    }

    public OperationResult<double> GetPassRate(string courseCode, string groupCode, double passing = 70.0)
    {
        var group = FindGroup(courseCode, groupCode);
        if (group is null) return OperationResult<double>.Fail("Curso/grupo no encontrado.");
        return OperationResult<double>.Ok(group.PassRate(passing), "Cálculo de aprobados.");
    }

    private Group? FindGroup(string courseCode, string groupCode)
        => _courses.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase))
                   ?.Groups.FirstOrDefault(g => g.GroupCode.Equals(groupCode, StringComparison.OrdinalIgnoreCase));
}
