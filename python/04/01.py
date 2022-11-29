from dataclasses import dataclass


@dataclass
class Board:
    numbers: list[list[int]] = None

    @property
    def rows(self):
        return [row for row in self.numbers]

    @property
    def columns(self):
        return [[row[i] for row in self.numbers] for i in range(len(self.numbers))]

    def any_row_complete(self, drawn: list[int]):
        return any(all(d in drawn for d in row) for row in self.rows)

    def any_column_complete(self, drawn: list[int]):
        return any(all(d in drawn for d in col) for col in self.columns)

    def complete(self, drawn: list[int]):
        return self.any_row_complete(drawn) or self.any_column_complete(drawn)

    def unmarked_count(self, drawn: list[int]):
        return sum([cell if cell not in drawn else 0 for row in self.rows for cell in row])

    def score(self, drawn: list[int], winning_number):
        return self.unmarked_count(drawn) * winning_number


with open("input.txt", "r", encoding="utf-8-sig") as f:
    lines = f.read().split('\n\n')
    numbers = [int(n) for n in lines[0].split(',')]
    boards = [list(map(
        lambda n: list(map(
            lambda x: int(x),
            n.split())),
        line.split('\n'))) for line in lines[1:]]

    boards = [Board(b) for b in boards]
    stop = False
    for i in range(1, len(numbers) + 1):
        if stop:
            break
        nums = numbers[:i]
        for b in boards:
            if b.complete(nums):
                print(b.score(nums, nums[-1]))
                stop = True
                break









