var allNodes = File
    .ReadAllLines("input.txt")
    .Select(line => line.Split("-"))
    .Select(parts => 
    (
        From: parts[0], 
        To: parts[1]
    ))
    .ToArray();

var paths = GetPaths("start").Where(p => p[^1] == "end").ToArray();
Console.WriteLine(paths.Length);

List<List<string>> GetPaths(string node, HashSet<string> visited = null)
{
    visited ??= new HashSet<string>();
    if (node == "end") return new List<List<string>> { new List<string>() };
    if (!IsBigCave(node[0])) visited.Add(node);

    var paths = new List<List<string>>();

    var foundNodes = allNodes.Where(n => (n.From == node || n.To == node) && !visited.Contains(GetNode(n, node))).ToArray();

    foreach (var foundNode in foundNodes)
    {
        var subNode = GetNode(foundNode, node);
        foreach (var moreNodes in GetPaths(subNode, new HashSet<string>(visited)))
        {
            moreNodes.Insert(0, foundNode.From == node ? foundNode.To : foundNode.From);
            paths.Add(moreNodes);
        }
    }

    if (!paths.Any()) paths.Add(new List<string>());

    return paths;
}

string GetNode((string From, string To) node, string source) => node.From == source ? node.To : node.From;
bool IsBigCave(char name) => name < 97;