using System.Diagnostics;
using System.Text;

var input = File.ReadAllLines("input.txt");
string template = input[0];
var rules = input
    .Skip(2)
    .Select(line => line.Split(" -> "))
    .Select(parts => new { Source = parts[0], Destination = parts[1] })
    .ToDictionary(rule => rule.Source, rule => rule.Destination);

int stepCount = 20;
var sw = Stopwatch.StartNew();


int chunkSize = 10;
int total = 0;

for (int c = 0; c < template.Length / chunkSize; c++)
{
    var templateChunk = template.Substring(Math.Max(chunkSize * c - 1, 0), chunkSize + 1);
    Console.WriteLine(templateChunk);
    var polymer = new StringBuilder();
    polymer.Append(templateChunk);



    for (int i = 0; i < stepCount; i++)
    {
        Console.WriteLine($"Step {i + 1}\t\t\t {sw.ElapsedMilliseconds}ms elapsed");
        StringBuilder sb = new StringBuilder();
        sb.Append(templateChunk[0]);
        //var polymerSpan = polymer.ToString().AsSpan();
        for (int counter = 0; counter < polymer.Length - 1; counter++)
        {
            string pair = $"{polymer[counter]}{polymer[counter + 1]}";
            sb.Append(rules[pair]);
            sb.Append(pair[1]);
        }

        polymer.Clear();
        polymer = sb;

        GC.Collect();
        //GC.WaitForPendingFinalizers();
        //var pairs = template.Skip(1).Select((c, i) => $"{template[i]}{c}").ToArray();
        //var modifiedPairs = pairs.Select(pair => $"{rules[pair]}{pair[1]}").ToArray();
        //template = template[0] + string.Join("", modifiedPairs);
    }

    var groups = polymer.ToString().GroupBy(c => c).OrderBy(g => g.Count()).ToArray();
    var leastCommonCount = groups[0].Count();
    var mostCommonCount = groups[^1].Count();
    sw.Stop();

    Console.WriteLine($"{sw.ElapsedMilliseconds}ms");

    total += mostCommonCount - leastCommonCount;
}

Console.WriteLine(total);
//class MegaStringBuilder
//{
//    private readonly List<StringBuilder> stringBuilders = new List<StringBuilder>();
//    private int currentIndex = 0;

//    public MegaStringBuilder()
//    {
//        stringBuilders.Add(new StringBuilder());
//    }

//    public void Append(char c)
//    {
//        if (Current.Length == Current.MaxCapacity)
//        {
//            stringBuilders.Add(new StringBuilder(Int32.MaxValue));
//            currentIndex++;
//        }

//        Current.Append(c);
//    }
//    public void Append(string s)
//    {
//        if (Current.Length + s.Length > Current.MaxCapacity)
//        {
//            stringBuilders.Add(new StringBuilder(Int32.MaxValue));
//            currentIndex++;
//        }

//        Current.Append(s);
//    }

//    public void Clear()
//    {
//        foreach (var sb in stringBuilders) sb.Clear();
//    }
//    public override string ToString()
//    {
//        return String.Join("", stringBuilders.Select(sb => sb.ToString()));
//    }

//    public char this[int index] => Current[index];

//    public int Length => stringBuilders.Sum(s => s.Length);

//    private StringBuilder Current => stringBuilders[currentIndex];
//}