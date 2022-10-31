with open("input.txt", "r", encoding="utf-8-sig") as f:
    commands = [line.split() for line in f.read().splitlines()]
    commands = list(map(lambda c: (c[0], int(c[1])), commands))
    horizontal_position = sum([x[1] if x[0] == "forward" else 0 for x in commands])
    depth = sum([x[1] if x[0] == "down" else -x[1] if x[0] == "up" else 0 for x in commands])
    print(horizontal_position * depth)
