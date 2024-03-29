﻿var grid = File.ReadAllLines("input.txt")
    .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

(int x, int y)[] adjacents = { (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1) };

int stepCount = 100;
int flashCount = 0;

for (int i = 0; i < stepCount; i++)
{
    IncrementEnergyLevels();
    Flash();
}

Console.WriteLine(flashCount);

void Flash(List<(int x, int y)>? alreadyFlashed = null)
{
    alreadyFlashed ??= new List<(int x, int y)>();

    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            if (grid[y][x] > 9 && !alreadyFlashed.Any(a => a.x == x && a.y == y))
            {
                flashCount++;
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