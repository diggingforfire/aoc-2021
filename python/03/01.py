import collections

with open("input.txt", "r", encoding="utf-8-sig") as f:
    lines = f.read().splitlines()
    indexedLines = [list(x) for x in [enumerate(line) for line in lines]]

    groups = {}

    for indexedLine in indexedLines:
        for i, char in indexedLine:
            groups.setdefault(i, "")
            groups[i] += char

    gammaLine = "".join([c[0] for c in [collections.Counter(x).most_common(1)[0] for x in groups.values()]])
    gamma = int(gammaLine, 2)
    epsilon = ~gamma & 0xFFF

    print(gamma*epsilon)



