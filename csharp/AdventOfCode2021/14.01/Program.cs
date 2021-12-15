var input = File.ReadAllLines("input.txt");
string template = input[0];
var rules = input
    .Skip(2)
    .Select(line => line.Split(" -> "))
    .Select(parts => new { Source = parts[0], Destination = parts[1] })
    .ToDictionary(rule => rule.Source, rule => rule.Destination);

int stepCount = 10;

for (int i = 0; i < stepCount; i++)
{
    var pairs = template.Skip(1).Select((c, i) => $"{template[i]}{c}").ToArray();
    var modifiedPairs = pairs.Select(pair => $"{rules[pair]}{pair[1]}").ToArray();
    template = template[0] + string.Join("", modifiedPairs);
}

var groups = template.GroupBy(c => c).OrderBy(g => g.Count()).ToArray();
var leastCommonCount = groups[0].Count();
var mostCommonCount = groups[^1].Count();

Console.WriteLine(mostCommonCount - leastCommonCount);