with open("input.txt", "r", encoding="utf-8-sig") as f:
    commands = [line.split() for line in f.read().splitlines()]
    commands = list(map(lambda c: (c[0], int(c[1])), commands))
    aim, horizontal_position, depth = 0, 0, 0

    for command in commands:
        match command[0]:
            case "down": aim += command[1]
            case "up": aim -= command[1]
            case "forward":
                horizontal_position += command[1]
                depth += aim * command[1]

    print(horizontal_position * depth)