// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
using University.App.Domain;
using University.App.Services;

var svc = new CourseService();

while (true)
{
    Console.WriteLine("\n=== Gestión Docente (POO: Herencia/Polimorfismo) ===");
    Console.WriteLine("1) Crear curso");
    Console.WriteLine("2) Crear grupo");
    Console.WriteLine("3) Agregar estudiante");
    Console.WriteLine("4) Registrar calificación");
    Console.WriteLine("5) Listar cursos y grupos");
    Console.WriteLine("6) Reporte por grupo");
    Console.WriteLine("7) % Aprobados por grupo (≥70)");
    Console.WriteLine("0) Salir");
    Console.Write("Opción: ");

    var opt = Console.ReadLine();
    if (opt == "0") break;

    try
    {
        switch (opt)
        {
            case "1":
                Console.Write("Código del curso: ");
                var code = Console.ReadLine() ?? "";
                Console.Write("Nombre del curso: ");
                var name = Console.ReadLine() ?? "";
                var r1 = svc.CreateCourse(code, name);
                Console.WriteLine($"{(r1.Success ? "✔" : "✖")} {r1.Message}");
                break;

            case "2":
                Console.Write("Código del curso: ");
                var c2 = Console.ReadLine() ?? "";
                Console.Write("Código del grupo: ");
                var g2 = Console.ReadLine() ?? "";
                var r2 = svc.CreateGroup(c2, g2);
                Console.WriteLine($"{(r2.Success ? "✔" : "✖")} {r2.Message}");
                break;

            case "3":
                Console.Write("Curso: ");
                var c3 = Console.ReadLine() ?? "";
                Console.Write("Grupo: ");
                var g3 = Console.ReadLine() ?? "";
                Console.Write("ID estudiante: ");
                var id = Console.ReadLine() ?? "";
                Console.Write("Nombre completo: ");
                var fn = Console.ReadLine() ?? "";
                Console.Write("Modalidad (P=Presencial / D=Distancia): ");
                var m = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
                bool onsite = m == "P";
                var r3 = svc.AddStudent(c3, g3, id, fn, onsite);
                Console.WriteLine($"{(r3.Success ? "✔" : "✖")} {r3.Message}");
                break;

            case "4":
                Console.Write("Curso: ");
                var c4 = Console.ReadLine() ?? "";
                Console.Write("Grupo: ");
                var g4 = Console.ReadLine() ?? "";
                Console.Write("ID estudiante: ");
                var id4 = Console.ReadLine() ?? "";
                Console.Write("Tipo (0=Exam,1=Practice,2=Project): ");
                if (!int.TryParse(Console.ReadLine(), out var t) || t < 0 || t > 2)
                {
                    Console.WriteLine("✖ Tipo inválido.");
                    break;
                }
                Console.Write("Nota (0-100): ");
                if (!double.TryParse(Console.ReadLine(), out var sc))
                {
                    Console.WriteLine("✖ Nota inválida.");
                    break;
                }
                var r4 = svc.AddGrade(c4, g4, id4, (GradeType)t, sc);
                Console.WriteLine($"{(r4.Success ? "✔" : "✖")} {r4.Message}");
                break;

            case "5":
                Console.WriteLine("=== Cursos ===");
                foreach (var c in svc.ListCourses())
                {
                    Console.WriteLine($"- {c}");
                    foreach (var g in c.Groups)
                        Console.WriteLine($"   • Grupo {g.GroupCode} (Estudiantes: {g.Students.Count})");
                }
                break;

            case "6":
                Console.Write("Curso: ");
                var c6 = Console.ReadLine() ?? "";
                Console.Write("Grupo: ");
                var g6 = Console.ReadLine() ?? "";
                var r6 = svc.PrintGroupReport(c6, g6);
                Console.WriteLine(r6.Success ? r6.Data : $"✖ {r6.Message}");
                break;

            case "7":
                Console.Write("Curso: ");
                var c7 = Console.ReadLine() ?? "";
                Console.Write("Grupo: ");
                var g7 = Console.ReadLine() ?? "";
                var r7 = svc.GetPassRate(c7, g7);
                Console.WriteLine(r7.Success
                    ? $"% Aprobados: {r7.Data:0.00}%"
                    : $"✖ {r7.Message}");
                break;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✖ Error inesperado: {ex.Message}");
    }
}

Console.WriteLine("Hasta luego.");
