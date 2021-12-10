var incompleteLines =
    File.ReadAllLines("input.txt")
    .Where(LineIsComplete)
    .ToArray();


var completedLines = new List<string>();
foreach (var line in incompleteLines)
{
    var stack = new Stack<char>();
    foreach (var c in line) stack.Push(c);
      
    var remainingStack = new Stack<char>();
    var toAdd = new List<char>();
    while (stack.Count > 0)
    {
        var popped = stack.Pop();

        switch (popped)
        {
            case '(':
                if (remainingStack.Count == 0) toAdd.Add(')');      
                else if (remainingStack.Peek() == ')') remainingStack.Pop();
                break;
            case '[':
                if (remainingStack.Count == 0) toAdd.Add(']');
                else if (remainingStack.Peek() == ']') remainingStack.Pop();
                break;
            case '{':
                if (remainingStack.Count == 0) toAdd.Add('}');
                else if (remainingStack.Peek() == '}') remainingStack.Pop();
                break;
            case '<':
                if (remainingStack.Count == 0) toAdd.Add('>');
                else if (remainingStack.Peek() == '>') remainingStack.Pop();
                break;
            default:
                remainingStack.Push(popped);
                break;
        }
    }

    completedLines.Add(new string(toAdd.ToArray()));
}

var scores = completedLines.Select(line => line.Select(c =>
                                            c == ')' ? 1 :
                                            c == ']' ? 2 :
                                            c == '}' ? 3 :
                                            c == '>' ? 4 : 0).Aggregate((long)0, (a, b) => a * 5 + b));

var middle = scores.OrderBy(score => score).ElementAt(scores.Count() / 2);

Console.WriteLine(middle);

bool LineIsComplete(string line)
{
    var stack = new Stack<char>();
    foreach (var c in line)
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
                if (popped != '(') return false;
                break;
            case ']':
                popped = stack.Pop();
                if (popped != '[') return false;
                break;
            case '}':
                popped = stack.Pop();
                if (popped != '{') return false;
                break;
            case '>':
                popped = stack.Pop();
                if (popped != '<') return false;
                break;
        }
    }

    return true;
}