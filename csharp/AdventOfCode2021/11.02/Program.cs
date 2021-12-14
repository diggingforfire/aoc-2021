var grid = File.ReadAllLines("input.txt")
    .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

(int x, int y)[] adjacents = { (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1) };

for (int i = 0; ; i++)
{
    IncrementEnergyLevels();
    Flash();

    if (grid.SelectMany(energy => energy).GroupBy(e => e).Count() == 1)
    {
        Console.WriteLine(i + 1);
        break;
    }
}

void Flash(List<(int x, int y)>? alreadyFlashed = null)
{
    alreadyFlashed ??= new List<(int x, int y)>();

    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            var x1 = x;
            var y1 = y;
            if (grid[y][x] > 9 && !alreadyFlashed.Any(a => a.x == x1 && a.y == y1))
            {
                alreadyFlashed.Add((x, y));

                var neighbours = adjacents.Select(adjacent =>
                {
                    int newY = y + adjacent.y;
                    int newX = x + adjacent.x;
                    if (newX >= 0 && newX < grid[0].Length && newY >= 0 && newY < grid.Length) return (newX, newY);
                    return (-1, -1);
                });

                foreach (var neighbour in neighbours.Where(n => n.Item1 != -1))
                {
                    if (!alreadyFlashed.Any(a => a.x == neighbour.Item1 && a.y == neighbour.Item2))
                    {
                        grid[neighbour.Item2][neighbour.Item1]++;
                        Flash(alreadyFlashed);
                    }
                }

                grid[y][x] = 0;
            }
        }
    }
}

void IncrementEnergyLevels()
{
    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            grid[y][x]++;
        }
    }
}