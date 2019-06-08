using System;
using System.Collections.Generic;
using System.Linq;

public class GradeSchool
{
    private readonly IDictionary<int, IList<string>> roster;

    public GradeSchool() =>
        roster = new Dictionary<int, IList<string>>();

    public void Add(string student, int grade)
    {
        roster.TryGetValue(grade, out var students);

        if (students is null)
            students = new List<string>();

        students.Add(student);
        roster[grade] = students
            .OrderBy(s => s)
            .ToList();
    }

    public IEnumerable<string> Roster() =>
        roster
            .OrderBy(r => r.Key)
            .SelectMany(r => r.Value);

    public IEnumerable<string> Grade(int grade) =>
        roster
            .Where(r => r.Key == grade)
            .SelectMany(r => r.Value);
}