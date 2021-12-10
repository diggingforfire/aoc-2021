var result = File.ReadAllLines("input.txt")
    .Select(line => line.Split(" | "))
    .Select(parts => new
    {
        SignalPatterns = parts[0].Split(" "),
        OutputValue = parts[1].Split(" ")
    })
    .Select(patterns => new
    {
        patterns.SignalPatterns,
        OutputValue = patterns.OutputValue,
    })
    .ToArray();

int total = 0;

foreach (var pattern in result)
{
    var segmentOne = GetSegmentsByPatternLength(pattern.SignalPatterns, 2).Single();
    var segmentFour = GetSegmentsByPatternLength(pattern.SignalPatterns, 4).Single();
    var segmentSeven = GetSegmentsByPatternLength(pattern.SignalPatterns, 3).Single();
    var segmentEight = GetSegmentsByPatternLength(pattern.SignalPatterns, 7).Single();

    string[] fiveSegments = GetSegmentsByPatternLength(pattern.SignalPatterns, 5).ToArray();
    string[] sixSegments = GetSegmentsByPatternLength(pattern.SignalPatterns, 6).ToArray();

    char[] display = new char[7];

    display[0] = segmentSeven.Except(segmentOne).Single();

    var fourOneDiff = segmentFour.Except(segmentOne).ToArray();
    string segmentFive = fiveSegments.Single(segment => fourOneDiff.All(segment.Contains));

    display[1] = segmentOne.Except(segmentFive).Single();
    display[2] = segmentOne.Single(@char => @char != display[1]);

    string[] twoOrThreeSegments = fiveSegments.Except(new[] { segmentFive }).ToArray();
    string segmentThree = twoOrThreeSegments.Single(s => s.Contains(display[2]));
    string segmentTwo = twoOrThreeSegments.Single(s => s != segmentThree);

    display[4] = segmentTwo.Except(segmentThree).Single();

    string segmentNine = sixSegments.Single(s => !s.Contains(display[4]));
    string[] zeroOrSixFragments = sixSegments.Except(new[] { segmentNine }).ToArray();

    string segmentSix = zeroOrSixFragments.Single(s => !s.Contains(display[1]));
    var segmentZero = zeroOrSixFragments.Single(s => s != segmentSix);
 
    var output = "";
    foreach (var outputValue in pattern.OutputValue)
    {
        if (ContentsEquals(outputValue, segmentZero))       output += "0";
        else if (ContentsEquals(outputValue, segmentOne))   output += "1";
        else if (ContentsEquals(outputValue, segmentTwo))   output += "2";
        else if (ContentsEquals(outputValue, segmentThree)) output += "3";
        else if (ContentsEquals(outputValue, segmentFour))  output += "4";
        else if (ContentsEquals(outputValue, segmentFive))  output += "5";
        else if (ContentsEquals(outputValue, segmentSix))   output += "6";
        else if (ContentsEquals(outputValue, segmentSeven)) output += "7";
        else if (ContentsEquals(outputValue, segmentEight)) output += "8";
        else if (ContentsEquals(outputValue, segmentNine))  output += "9";
    }

    total += int.Parse(output);
}

bool ContentsEquals(string one, string two)
{
    var charsOne = one.ToCharArray();
    Array.Sort(charsOne);
    var charsTwo = two.ToCharArray();
    Array.Sort(charsTwo);

    return new string(charsOne) == new string(charsTwo);
}

Console.WriteLine(total);

string[] GetSegmentsByPatternLength(string[] patterns, int length) =>
    patterns.Where(pattern => pattern.Length == length).ToArray();