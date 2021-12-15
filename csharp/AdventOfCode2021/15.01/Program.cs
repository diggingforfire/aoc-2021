// hello Dijkstra my old friend

var grid = File.ReadAllLines("input.txt")
    .Select((line, y) => line.Select((c, x) => new Node
    {
        Label = int.Parse(c.ToString()),
        Y = y,
        X = x,
    }).ToArray()).ToArray();

(int x, int y)[] adjacents = { (-1, 0), (1, 0), (0, -1), (0, 1) };

grid[0][0].Distance = 0;

foreach (var node in grid.SelectMany(n => n))
{
    var current = grid.SelectMany(n => n).Where(n => !n.Visited).OrderBy(n => n.Distance).FirstOrDefault();
  
    current.Visited = true;

    var unvisitedNeighbours = GetUnvisitedNeighbours(current);

    foreach (var neighbour in unvisitedNeighbours)
    {
        var distance = neighbour.Label + current.Distance;
        neighbour.Distance = Math.Min(distance, neighbour.Distance);
    }
}

Console.WriteLine(grid[grid.Length - 1][grid[0].Length - 1].Distance);

Node[] GetUnvisitedNeighbours(Node current1)
{
    var unvisitedNeighbours = adjacents.Select(adjacent =>
    {
        int newY = current1.Y + adjacent.y;
        int newX = current1.X + adjacent.x;
        if (newX >= 0 && newX < grid[0].Length && newY >= 0 && newY < grid.Length)
        {
            var node = grid[newY][newX];
            if (!node.Visited) return node;
        }

        return null;
    }).Where(n => n != null).ToArray();
    return unvisitedNeighbours;
}

class Node
{
    public int Label { get; set; }
    public bool Visited { get; set; }
    public int Distance { get; set; } = int.MaxValue;
    public int X { get; set; }
    public int Y { get; set; }
}