var input = (await File.ReadAllLinesAsync("input.txt")).Select(int.Parse).ToArray();
var sums = input.Skip(2).Select((cur, i) => cur + input[i] + input[i + 1]).ToArray();
var result = sums.Skip(1).Select((cur, i) => new { cur, prev = sums[i] }).Count(x => x.cur > x.prev);
Console.WriteLine(result);