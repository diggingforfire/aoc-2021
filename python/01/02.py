with open("input.txt", "r", encoding="utf-8-sig") as f:
    lines = f.read().splitlines()
    sums = [sum([int(i) for i in line]) for line in zip(lines, lines[1:], lines[2:])]
    count = sum([1 if summed[1] > summed[0] else 0 for summed in zip(sums, sums[1:])])
    print(count)

