var lines = (await File.ReadAllLinesAsync("input.txt"));

var gammas = lines
    .SelectMany(line => line) // all chars
    .Select((c, i) => new { Char = c, Index = i }) // all chars with their index
    .GroupBy(p => p.Index % lines[0].Length) // grouped by their position
    .Select(grp =>
    new
    {
        Gamma = grp
            .GroupBy(charWithIndex => charWithIndex.Char) // grouped by char
            .OrderByDescending(charGroup => charGroup.Count()).ToList()
    })
    .Select(gammas => new 
    { 
        First = gammas.Gamma[0].Key, 
        Second = gammas.Gamma[1].Key 
    });

var gamma = Convert.ToInt32(new string(gammas.Select(gamma => gamma.First).ToArray()), 2);
var epsilon = Convert.ToInt32(new string(gammas.Select(gamma => gamma.Second).ToArray()), 2);

Console.WriteLine(gamma * epsilon);