var chunks = File.ReadAllLines("input.txt");
var stack = new Stack<char>();
int points = 0;

foreach (var chunk in chunks)
{
    foreach (var c in chunk)
    {
        switch (c)
        {
            case '(':
            case '[':
            case '{':
            case '<':
                stack.Push(c);
                break;
            case ')':
                if (stack.Pop() != '(') points += 3;
                break;
            case ']':
                if (stack.Pop() != '[') points += 57;
                break;
            case '}':
                if (stack.Pop() != '{') points += 1197;
                break;
            case '>':
                if (stack.Pop() != '<') points += 25137;
                break;
        }
    }
}

Console.WriteLine(points);