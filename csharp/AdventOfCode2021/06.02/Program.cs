var fishies =
    (await File.ReadAllTextAsync("input.txt"))
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse);

long total = 0;
int totalDaysRemaining = 256;
var cache = new Dictionary<int, long>();

foreach (var fish in fishies)
{
    if (!cache.ContainsKey(fish))
    {
        var count = Get(fish + 1, totalDaysRemaining) + 1;
        cache.Add(fish, count);
    }

    total += cache[fish];
}

Console.WriteLine(total);

long Get(int fish, int daysRemaining)
{
    long total = 0;
    daysRemaining -= fish;

    if (daysRemaining < 0) return 0;

    var childFishies = (int)Math.Floor((double)daysRemaining / 7) + 1;
    total += childFishies;

    daysRemaining -= 2;

    for (int i = 0; i < childFishies; i++)
    {
        total += Get(7, daysRemaining - (i * 7));
    }

    return total;
}