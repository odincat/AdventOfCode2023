public static class Part1 {
    const char IGNORE = '.';

    static List<char> SYMBOLS = new() {
        '*',
        '&',
        '/',
        '@',
        '=',
        '+',
        '#',
        '$',
        '%',
        '-'
    };

    private static int totalSum = 0;
    
    public static void Run(string[] input) {
        int lineIndex = -1;

        foreach (string line in input) {
            lineIndex++;

            int characterIndex = -1;

            List<int> singleNumber = new();

            bool valid = false;

            Console.WriteLine(line);

            foreach (char c in line) {
                characterIndex ++;

                if (char.IsNumber(c)) {
                    Console.WriteLine(c);
                    // check if it's valid
                    //
                    int parsed = int.Parse(c.ToString());
                    singleNumber.Add(parsed);


                    // LINE ABOVE
                    if (lineIndex - 1 >= 0) {
                        var aboveLine = input[lineIndex - 1];

                        if (characterIndex - 1 >= 0) {
                            var aboveLeftChar = aboveLine[characterIndex - 1];
                            
                            if (SYMBOLS.Contains(aboveLeftChar)) {
                                valid = true;
                            }
                        }

                        var aboveChar = aboveLine[characterIndex];
                        if (SYMBOLS.Contains(aboveChar)) {
                            valid = true;
                        }

                        if (characterIndex + 1 < aboveLine.Count()) {
                            var aboveRightChar = aboveLine[characterIndex + 1];

                            if (SYMBOLS.Contains(aboveRightChar)) {
                                valid = true;
                            }
                        }
                    }
                    // END LINE ABOVE

                    // CURRENT LINE
                    if (characterIndex - 1 >= 0) {
                        var beforeChar = line[characterIndex - 1];

                        if (SYMBOLS.Contains(beforeChar)) {
                            valid = true;
                        }
                    }

                    if (characterIndex + 1 < line.Count()) {
                        var nextChar = line[characterIndex + 1];

                        if (SYMBOLS.Contains(nextChar)) {
                            valid = true;
                        }
                    }
                    // END CURRENT LINE


                    // BELOW LINE
                    if (lineIndex + 1 < input.Count()) {
                        var belowLine = input[lineIndex + 1];

                        if (characterIndex - 1 >= 0) {
                            var belowLeftChar = belowLine[characterIndex - 1];

                            if (SYMBOLS.Contains(belowLeftChar)) {
                                valid = true;
                            }
                        }

                        var belowChar = belowLine[characterIndex];
                        if (SYMBOLS.Contains(belowChar)) {
                            valid = true;
                        }

                        if (characterIndex + 1 < belowLine.Count()) {
                            var belowRightChar = belowLine[characterIndex + 1];

                            if (SYMBOLS.Contains(belowRightChar)) {
                                valid = true;
                            }
                        }
                    }

                } else {
                    if (singleNumber.Count > 0) {
                        var joined = string.Join("", singleNumber);

                        Console.WriteLine(joined + $" - char index: {characterIndex} - line: {lineIndex + 1}");

                        int parsed = int.Parse(joined);

                        if (valid) {
                            totalSum += parsed;
                        } else {
                            Console.WriteLine($"INVALID on line {lineIndex + 1} - {joined}");
                        }
                    }

                    singleNumber.Clear();
                    valid = false;
                }
            }


            if (singleNumber.Count > 0) {
                var joined = string.Join("", singleNumber);

                Console.WriteLine(joined + $" - char index: {characterIndex} - line: {lineIndex + 1}");

                int parsed = int.Parse(joined);

                if (valid) {
                    totalSum += parsed;
                } else {
                    Console.WriteLine($"INVALID on line {lineIndex + 1} - {joined}");
                }
            }

            singleNumber.Clear();
            valid = false;
        }

        Console.WriteLine(totalSum);
    }
}
