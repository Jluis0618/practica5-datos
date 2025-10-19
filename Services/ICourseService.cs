
public interface ICourseService
{
    OperationResult<Course> CreateCourse(string code, string name);
    OperationResult<Group> CreateGroup(string courseCode, string groupCode);
    OperationResult<Student> AddStudent(string courseCode, string groupCode, string id, string name, bool isOnsite);
    OperationResult<bool> AddGrade(string courseCode, string groupCode, string studentId, GradeType type, double score);
    OperationResult<string> PrintGroupReport(string courseCode, string groupCode);
    OperationResult<double> GetPassRate(string courseCode, string groupCode, double passing = 70.0);
    IReadOnlyList<Course> ListCourses();
}
