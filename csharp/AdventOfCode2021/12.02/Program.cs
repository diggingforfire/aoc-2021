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

string GetNode((string From, string To) node, string source) => node.From == source ? node.To : node.From;
bool IsBigCave(string name) => name[0] < 97;

List<List<string>> GetPaths(string node, List<string> visited = null)
{
    visited ??= new List<string>();
    if (node == "end") return new List<List<string>> { new List<string>() };
    if (!IsBigCave(node)) visited.Add(node);

    var paths = new List<List<string>>();

    var foundNodes = allNodes.Where(n => (n.From == node || n.To == node) && 
                    (GetNode(n, node) == "start" && visited.Count(v => v == GetNode(n, node)) == 0 ||
                     GetNode(n, node) != "start")).ToArray();

    foreach (var foundNode in foundNodes)
    {
        var otherSmallCavesVisitedTwice = visited.GroupBy(v => v).Count(g => g.Key != GetNode(foundNode, node) && g.Count() == 2);
        var thisCaveVisits = visited.Count(v => v == GetNode(foundNode, node));

        if (otherSmallCavesVisitedTwice + thisCaveVisits <= 1)
        {
            var subNode = GetNode(foundNode, node);
            foreach (var moreNodes in GetPaths(subNode, new List<string>(visited)))
            {
                moreNodes.Insert(0, foundNode.From == node ? foundNode.To : foundNode.From);
                paths.Add(moreNodes);
            }
        }
    }

    if (!paths.Any()) paths.Add(new List<string>());

    return paths;
}