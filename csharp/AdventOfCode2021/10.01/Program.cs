var chunks = File.ReadAllLines("input.txt");
var stack = new Stack<char>();
int points = 0;

foreach (var chunk in chunks)
{
    foreach (var c in chunk)
    {
        char popped = ' ';
        switch (c)
        {
            case '(':
            case '[':
            case '{':
            case '<':
                stack.Push(c);
                break;
            case ')':
                popped = stack.Pop();
                if (popped != '(') points += 3;
                break;
            case ']':
                popped = stack.Pop();
                if (popped != '[') points += 57;
                break;
            case '}':
                popped = stack.Pop();
                if (popped != '{') points += 1197;
                break;
            case '>':
                popped = stack.Pop();
                if (popped != '<') points += 25137;
                break;
        }
    }
}

Console.WriteLine(points);