var lines = 
        (await File.ReadAllTextAsync("input.txt"))
        .Split($"{Environment.NewLine}{Environment.NewLine}")
        .ToArray();

var numbers = lines[0].Split(',').Select(int.Parse).ToArray();
var boards = lines
    .Skip(1)
    .Select(line => new Board 
    { 
        Numbers = line
        .Split(Environment.NewLine)
        .Select(innerLine => innerLine
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(c => new Ticket { Number = int.Parse(c) }).ToArray())
        .ToArray()
    }
    ).ToArray();

foreach (var num in numbers)
{
    foreach (var board in boards)
    {
        board.Mark(num);
        if (board.IsComplete)
        {
            board.WinningNumber = num;
            Console.WriteLine(board.Score);
            return;
        }   
    }
}

class Board
{
    public Ticket[][] Numbers { get; set; }
    public void Mark(int number)
    {
        var num = Numbers.SelectMany(n => n).SingleOrDefault(n => n.Number == number);
        if (num != null) num.Marked = true;
    }

    public int UnmarkedCount => Numbers.SelectMany(n => n.Select(t => t)).Where(ticket => !ticket.Marked).Sum(t => t.Number);
    public bool IsComplete => AnyRowComplete || AnyColumnComplete;
    public int WinningNumber { get; set; }
    public int Score => WinningNumber * UnmarkedCount;
    bool AnyRowComplete => Numbers.Any(row => row.All(n => n.Marked));
    bool AnyColumnComplete => Numbers
            .SelectMany(n => n)
            .Select((n, i) => new { n, i })
            .GroupBy(n => n.i % 5)
            .Any(g => g.All(n => n.n.Marked));
}

class Ticket
{
    public int Number { get; set; }
    public bool Marked { get; set; }
    public override string ToString() => $"{Number}: {Marked}";
}