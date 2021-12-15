var input = File
    .ReadAllText("input.txt")
    .Split($"{Environment.NewLine}{Environment.NewLine}");

var dots = input[0]
    .Split(Environment.NewLine)
    .Select(l => l.Split(",").Select(int.Parse).ToArray())
    .Select(parts => (x: parts[0], y: parts[1]))
    .ToHashSet();

var folds = input[1]
    .Split(Environment.NewLine)
    .Select(l => l.Substring(11, l.Length - 11).Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries).ToArray())
    .Select(parts => new { axis = parts[0], offset = int.Parse(parts[1]) })
    .ToArray();

//var fold = folds[0];


foreach (var fold in folds)
{
    if (fold.axis == "y")
    {
        var affectedDots = dots.Where(d => d.y > fold.offset).ToArray();
        foreach (var affectedDot in affectedDots)
        {
            var foldOffset = affectedDot.y - fold.offset;
            var newDot = (x: affectedDot.x, y: fold.offset - foldOffset);
            dots.Add(newDot);
            dots.Remove(affectedDot);
        }
    }
    else
    {
        var affectedDots = dots.Where(d => d.x > fold.offset).ToArray();
        foreach (var affectedDot in affectedDots)
        {
            var foldOffset = affectedDot.x - fold.offset;
            var newDot = (x: fold.offset - foldOffset, y: affectedDot.y);
            dots.Add(newDot);
            dots.Remove(affectedDot);
        }
    }
}

int maxY = dots.Max(d => d.y);
int maxX = dots.Max(d => d.x);

for (int y = 0; y <= maxY; y++)
{
    for (int x = 0; x <= maxX; x++)
    {
        if (dots.Any(d => d.x == x && d.y == y)) Console.Write("#"); else Console.Write(".");
    }

    Console.WriteLine();
}