var heightMap = File.ReadAllLines("input.txt")
    .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

(int x, int y)[] adjacents = { (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1) };

var lowPoints = new List<int>();
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
        if (point < lowestNeighbour) lowPoints.Add(point);
    }
}
var result = lowPoints.Sum(p => p + 1);
Console.WriteLine(result);