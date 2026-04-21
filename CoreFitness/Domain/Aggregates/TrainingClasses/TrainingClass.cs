using static System.Net.WebRequestMethods;

namespace Domain.Aggregates.TrainingClasses;

public sealed class TrainingClass
{
    private TrainingClass() { }
    private TrainingClass(string id, string courseName, DateTime date, TimeSpan time, string instructor, string category)
    {
        Id = id;
        CourseName = courseName;
        Date = date;
        Time = time;
        Instructor = instructor;
        Category = category;
    }
    public string Id { get; private set; } = null!;
    public string CourseName { get; private set; } = null!;
    public DateTime Date { get; private set; }

    public TimeSpan Time { get; private set; }
    public string Instructor { get; private set; } = null!;
    public string Category { get; private set; } = null!;
}
