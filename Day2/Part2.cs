using System.Diagnostics;
using Spectre.Console;

public static class Part2 {
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

        int totalPower = 0;

        // A little easier than parsing out the number from the line
        foreach(var game in fileLines) {
            var gameIdSplit = game.Split(GAME_ID_SEPARATOR);

            var movesRaw = gameIdSplit.Last();
            var moves = movesRaw.Split(MOVE_SEPARATOR);

            int minRed = 0;
            int minBlue = 0;
            int minGreen = 0;

            foreach(var move in moves) {
                var sets = move.Split(MOVE_SET_SEPARATOR);

                foreach (var set in sets)
                {
                    var setSplit = set.Split(SET_COLOR_SEPARATOR);
                    
                    var amountRaw = setSplit.First();
                    var amount = int.Parse(amountRaw);

                    var color = setSplit.Last();

                    switch(color) {
                        case RED: {
                            if (amount > minRed) {
                                minRed = amount;
                            }
                        }
                        break;

                        case BLUE: {
                            if (amount > minBlue) {
                                minBlue = amount;
                            }
                        }
                        break;

                        case GREEN: {
                            if (amount > minGreen) {
                                minGreen = amount;
                            }
                        }
                        break;

                        default: {
                            throw new Exception($"Unknown color '{color}'");
                        }
                    }
                }
            }

            var minGamePower = minRed * minBlue * minGreen;

            totalPower += minGamePower;
        }

        stopwatch.Stop();

        AnsiConsole.MarkupLine($"[blue]Total power of minimum required cubes of all the games is[/] [gray]'[/][lime]{totalPower}[/][gray]'[/] [gray]-[/] [blue]took[/] [lime]{stopwatch.ElapsedMilliseconds}ms[/]");
    }
}
