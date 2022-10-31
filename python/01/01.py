with open("input.txt", "r", encoding="utf-8-sig") as f:
    lines = f.read().splitlines()
    count = sum([1 if line[1] > line[0] else 0 for line in zip(lines, lines[1:])])
    print(count)

