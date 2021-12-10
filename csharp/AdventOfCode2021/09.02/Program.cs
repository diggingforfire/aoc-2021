var heightMap = File.ReadAllLines("input.txt")
    .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

(int x, int y)[] adjacents = { (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1) };
(int x, int y)[] adjacentsHoriVerti = 
    new[] { (0, -1),  (1, 0), (0, 1),  (-1, 0),};

var lowPoints = new List<(int x, int y, int value)>();
for (int y = 0; y < heightMap.Length; y++)
{
    for (int x = 0; x < heightMap[0].Length; x++)
    {
        var point = heightMap[y][x];
        var lowestNeighbour = adjacents.Select(adjacent =>
        {
            int newY = y + adjacent.y;
            int newX = x + adjacent.x;
            if (newX >= 0 && newX < heightMap[0].Length &&
                newY >= 0 && newY < heightMap.Length) return heightMap[newY][newX];
            return 9;
        }).Min();
        if (point < lowestNeighbour) lowPoints.Add((x, y, point));
    }
}

var basins = new List<HashSet<(int, int, int)>>(lowPoints.Count);

foreach (var point in lowPoints)
{
    var basinBuddies = new HashSet<(int, int, int)>();
    AddBasinBuddies(heightMap, point, (-1, -1), basinBuddies);
    basins.Add(basinBuddies);
}

var result = basins.OrderByDescending(b => b.Count).Take(3).Select(b => b.Count).Aggregate((a, b) => a * b);

void AddBasinBuddies(
    int[][] heightMap, 
    (int x, int y, int value) point, (int x, int y) previousPoint, HashSet<(int, int, int)> buddies)
{
    buddies.Add(point);
    foreach (var adjacent in adjacentsHoriVerti)
    {
        int newY = point.y + adjacent.y;
        int newX = point.x + adjacent.x;
        if (newX >= 0 && newX < heightMap[0].Length && newY >= 0 && newY < heightMap.Length)
        {
            int adjacentValue = heightMap[newY][newX];

            if (adjacentValue != 9 && 
                !buddies.TryGetValue((newX, newY, adjacentValue), out _))
            {
                AddBasinBuddies(heightMap, (newX, newY, adjacentValue), (point.x, point.y), buddies);
            }
        }
    }
}

Console.WriteLine(result);