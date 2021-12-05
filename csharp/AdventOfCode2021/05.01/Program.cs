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
    })
    .Where(xys => xys.x1 == xys.x2 || xys.y1 == xys.y2);

var points = new List<(int x, int y)>();

foreach (var line in lines)
{
    for (int x1 = Math.Min(line.x1, line.x2), x2 = Math.Max(line.x1, line.x2); x1 <= x2; x1++)
    {
        for (int y1 = Math.Min(line.y1, line.y2), y2 = Math.Max(line.y1, line.y2); y1 <= y2; y1++)
        {
            points.Add((x1, y1));
        }
    }
}

var overlaps = points
    .GroupBy(p => $"{p.x}-{p.y}")
    .Where(grp => grp.Count() >= 2);
 
Console.WriteLine(overlaps.Count());