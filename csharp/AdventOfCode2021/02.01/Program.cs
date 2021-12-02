var input = (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => line.Split(" "))
    .Select(parts => new
    {
        Command = parts[0],
        X = int.Parse(parts[1])
    });

int depth = 0;
int pos = 0;

foreach (var cmd in input)
{
    switch (cmd.Command)
    {
        case "down": depth += cmd.X; break;
        case "up": depth -= cmd.X; break;
        case "forward": pos += cmd.X; break;
    }
}

var result = depth * pos;
Console.WriteLine(result);