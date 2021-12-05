var lines =
    (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => line.Split("->"))
    .Select(parts => new { a = parts[0].Trim().Split(","), b = parts[1].Trim().Split(",") })
    .Select(xys => new
    {
        x1 = int.Parse(xys.a[0]),
        y1 = int.Parse(xys.a[1]),
        x2 = int.Parse(xys.b[0]),
        y2 = int.Parse(xys.b[1])
    });

var points = new List<(int x, int y)>();

foreach (var line in lines)
{
    for (
        int x1 = line.x1, x2 = line.x2, y1 = line.y1, y2 = line.y2; 
        Math.Min(x1, x2) <= Math.Max(x1, x2) && Math.Min(y1, y2) <= Math.Max(y1, y2); )
    {
        points.Add((x1, y1));
        if (x1 == x2 && y1 == y2) break;
        if (x1 < x2) x1++; else if (x1 > x2) x1--;
        if (y1 < y2) y1++; else if (y1 > y2) y1--;
    }
}

var overlaps = points
    .GroupBy(p => $"{p.x}-{p.y}")
    .Where(grp => grp.Count() >= 2);

Console.WriteLine(overlaps.Count());