using System.Diagnostics;
using Spectre.Console;

public static class Part1 {
    public static void Run() {
        var fileLines = File.ReadAllLines("./input.txt");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        const string GAME_ID_SEPARATOR = ": ";
        const string MOVE_SEPARATOR = "; ";
        const string MOVE_SET_SEPARATOR = ", ";
        const char SET_COLOR_SEPARATOR = ' ';

        const string RED = "red";
        const string BLUE = "blue";
        const string GREEN = "green";

        Dictionary<string, int> cubeSet = new() {
            { RED, 12 },
            { GREEN, 13 },
            { BLUE, 14 }
        };

        int possibleGamesIdSum = 0;

        // A little easier than parsing out the number from the line
        int gameIndex = 0;

        foreach(var game in fileLines) {
            gameIndex++;

            bool possible = true;

            var gameIdSplit = game.Split(GAME_ID_SEPARATOR);

            var movesRaw = gameIdSplit.Last();
            var moves = movesRaw.Split(MOVE_SEPARATOR);

            foreach(var move in moves) {
                var sets = move.Split(MOVE_SET_SEPARATOR);

                foreach (var set in sets)
                {
                    var setSplit = set.Split(SET_COLOR_SEPARATOR);
                    
                    var amountRaw = setSplit.First();
                    var amount = int.Parse(amountRaw);

                    var color = setSplit.Last();

                    var cs = cubeSet.Where(s => s.Key == color).FirstOrDefault();

                    if (cs.Key != null) {
                        if (cs.Value < amount) {
                            possible = false;
                        }
                    } else {
                        throw new Exception($"Unexpected color '{color}'");
                    }
                }
            }

            if (possible) {
                possibleGamesIdSum += gameIndex;
            }
        }

        stopwatch.Stop();

        AnsiConsole.MarkupLine($"[blue]Sum of the ids of all the possible games is[/] [gray]'[/][lime]{possibleGamesIdSum}[/][gray]'[/] [gray]-[/] [blue]took[/] [lime]{stopwatch.ElapsedMilliseconds}ms[/]");
    }
}
