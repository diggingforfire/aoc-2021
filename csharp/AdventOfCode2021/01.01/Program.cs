var input = (await File.ReadAllLinesAsync("input.txt")).Select(int.Parse).ToArray();
var result = input.Skip(1).Select((cur, i) => new { cur, prev = input[i] }).Count(x => x.cur > x.prev);
Console.WriteLine(result);