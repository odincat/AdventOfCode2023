using System.Diagnostics;
using Spectre.Console;

Dictionary<string, int> wordNumbers = new() {
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};

var fileLines = File.ReadAllLines("./input.txt");

var stopwatch = new Stopwatch();
stopwatch.Start();

int totalSum = 0;

foreach(var lineString in fileLines) {
    List<int> numbers = new();
    List<string> characterBuffer = new();

    foreach(char character in lineString) {
        if (Char.IsNumber(character)) {
            numbers.Add(int.Parse(character.ToString()));

            characterBuffer.Clear();
        } else {
            characterBuffer.Add(character.ToString());
        }

        var joinedChars = string.Join("", characterBuffer);
        var matchingNumber = wordNumbers.Where(w => joinedChars.Contains(w.Key)).FirstOrDefault();

        if (matchingNumber.Key != null) {
            numbers.Add(matchingNumber.Value);

            characterBuffer.Clear();

            // hack to still detect strings like 'sevenine' properly as 7 and 9
            characterBuffer.Add(character.ToString());
        }
    }


    int firstNum = numbers.FirstOrDefault();
    int lastNum = numbers.LastOrDefault();

    var joined = int.Parse($"{firstNum}{lastNum}");

    totalSum += joined;
}

stopwatch.Stop();

AnsiConsole.MarkupLine($"[blue]Result of all joined numbers is[/] [gray]'[/][lime]{totalSum}[/][gray]'[/] [gray]-[/] [blue]took[/] [lime]{stopwatch.ElapsedMilliseconds}ms[/]");
