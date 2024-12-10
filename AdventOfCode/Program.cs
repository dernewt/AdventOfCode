using AdventOfCode;
using static System.Reflection.BindingFlags;

var days = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(t => t.GetTypes())
    .Where(t => t.Namespace == nameof(AdventOfCode) && t.Name.StartsWith("Day"))
    .OrderBy(t => t.Name)
    .Select(t => t.GetMethod(nameof(Day1.Run), Public|Static)!);

foreach (var day in days)
{
    Console.WriteLine($"{day.DeclaringType!.Name}: {day.Invoke(null, null)}");
}


