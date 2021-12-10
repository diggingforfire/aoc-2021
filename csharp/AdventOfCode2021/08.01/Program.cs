var result = File.ReadAllLines("input.txt")
    .Select(line => line.Split(" | "))
    .Select(parts => new
    {
        SignalPatterns = parts[0].Split(" "),
        OutputValue = parts[1].Split(" ")
    })
    .Select(patterns => new
    {
        patterns.SignalPatterns,
        patterns.OutputValue,
        Digits = patterns.OutputValue.Where(pattern => new[] { 2, 4, 3, 7 }.Contains(pattern.Length))
    })
    .Sum(p => p.Digits.Count());
   
Console.WriteLine(result);