var positions =
    (await File.ReadAllTextAsync("input.txt"))
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .OrderBy(pos => pos)
    .ToArray();

var result = 
    Enumerable.Range(positions[0], positions[^1]).Min(position => positions.Sum(p => Math.Abs(p - position)));

Console.WriteLine(result);