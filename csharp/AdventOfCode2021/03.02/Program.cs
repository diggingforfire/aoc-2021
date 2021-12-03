var lines = (await File.ReadAllLinesAsync("input.txt")).ToArray();

var oxygen = GetRating(lines, 0, '1');
var scrubber = GetRating(lines, 1, '0');

Console.WriteLine(oxygen * scrubber);

static int GetRating(string[] lines, int group, char filter)
{
    for (int i = 0; i < lines[0].Length; i++)
    {
        var common = GetCharAtPosition(lines, i, group); // common is null if both 0 and 1 appear in equal amount
        lines = lines.Where(line => line[i] == (common ?? filter)).ToArray();
        if (lines.Length == 1) return Convert.ToInt32(lines[0], 2);
    }

    return 0;
}

static char? GetCharAtPosition(string[] input, int position, int group) 
{
    var chars = input.Select(line => line[position]).ToArray();
    var groups = chars.GroupBy(i => i).OrderByDescending(g => g.Count()).ToArray();

    if (groups[0].Count() == groups[1].Count()) return null;
    return groups[group].Key; // group 0 is most common, group 1 is least common
}