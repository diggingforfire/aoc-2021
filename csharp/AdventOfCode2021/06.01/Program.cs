var fishies =
    (await File.ReadAllTextAsync("input.txt"))
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToList();

Console.WriteLine(fishies);
int newf = 0;
for (int i = 0; i < 256; i++)
{
    for (int f = 0; f < fishies.Count; f++)
    {
        fishies[f]--;
        if (fishies[f] == -1)
        {
            fishies[f] = 6;
            newf++;
        }
    }

    for (int nf = 0; nf < newf; nf++)
    {
        fishies.Add(8);
    }

    newf = 0;


    Console.WriteLine(i);

}

Console.WriteLine(fishies.Count());
Console.ReadKey();