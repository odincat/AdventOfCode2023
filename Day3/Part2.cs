using System.Text.Json;

public static class Part2 {
    const char IGNORE = '.';

    const char GEAR_IDENTIFIER = '*';

    private static int totalSum = 0;

    private static List<GearValue> gearValues = new();
    
    public static void Run(string[] input) {
        int lineIndex = -1;

        foreach (string line in input) {
            lineIndex++;

            int characterIndex = -1;

            List<int> singleNumber = new();

            bool valid = false;

            Console.WriteLine(line);

            var gearLineIndex = 0;
            var gearCharIndex = 0;

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
                            
                            if (aboveLeftChar == GEAR_IDENTIFIER) {
                                valid = true;
                                gearLineIndex = lineIndex - 1;
                                gearCharIndex = characterIndex - 1;
                            }
                        }

                        var aboveChar = aboveLine[characterIndex];
                        if (aboveChar == GEAR_IDENTIFIER) {
                            valid = true;
                            gearLineIndex = lineIndex - 1;
                            gearCharIndex = characterIndex;
                        }

                        if (characterIndex + 1 < aboveLine.Count()) {
                            var aboveRightChar = aboveLine[characterIndex + 1];

                            if (aboveRightChar == GEAR_IDENTIFIER) {
                                valid = true;
                                gearLineIndex = lineIndex - 1;
                                gearCharIndex = characterIndex + 1;
                            }
                        }
                    }
                    // END LINE ABOVE

                    // CURRENT LINE
                    if (characterIndex - 1 >= 0) {
                        var beforeChar = line[characterIndex - 1];

                        if (beforeChar == GEAR_IDENTIFIER) {
                            valid = true;
                            gearLineIndex = lineIndex;
                            gearCharIndex = characterIndex - 1;
                        }
                    }

                    if (characterIndex + 1 < line.Count()) {
                        var nextChar = line[characterIndex + 1];

                        if (nextChar == GEAR_IDENTIFIER) {
                            valid = true;
                            gearLineIndex = lineIndex;
                            gearCharIndex = characterIndex + 1;
                        }
                    }
                    // END CURRENT LINE


                    // BELOW LINE
                    if (lineIndex + 1 < input.Count()) {
                        var belowLine = input[lineIndex + 1];

                        if (characterIndex - 1 >= 0) {
                            var belowLeftChar = belowLine[characterIndex - 1];

                            if (belowLeftChar == GEAR_IDENTIFIER) {
                                valid = true;
                                gearLineIndex = lineIndex + 1;
                                gearCharIndex = characterIndex - 1;
                            }
                        }

                        var belowChar = belowLine[characterIndex];
                        if (belowChar == GEAR_IDENTIFIER) {
                            valid = true;
                            gearLineIndex = lineIndex + 1;
                            gearCharIndex = characterIndex;
                        }

                        if (characterIndex + 1 < belowLine.Count()) {
                            var belowRightChar = belowLine[characterIndex + 1];

                            if (belowRightChar == GEAR_IDENTIFIER) {
                                valid = true;
                                gearLineIndex = lineIndex + 1;
                                gearCharIndex = characterIndex + 1;
                            }
                        }
                    }

                } else {
                    if (singleNumber.Count > 0) {
                        var joined = string.Join("", singleNumber);

                        //Console.WriteLine(joined + $" - char index: {characterIndex} - line: {lineIndex + 1}");

                        int parsed = int.Parse(joined);

                        if (valid) {
                            GearValue gv = new() {
                                Value = parsed,
                                GearLineIndex = gearLineIndex,
                                GearCharIndex = gearCharIndex 
                            };

                            gearValues.Add(gv);
                        } else {
                            //Console.WriteLine($"INVALID on line {lineIndex + 1} - {joined}");
                        }
                    }

                    gearLineIndex = 0;
                    gearCharIndex = 0;

                    singleNumber.Clear();
                    valid = false;
                }
            }


            if (singleNumber.Count > 0) {
                var joined = string.Join("", singleNumber);

                //Console.WriteLine(joined + $" - char index: {characterIndex} - line: {lineIndex + 1}");

                int parsed = int.Parse(joined);

                if (valid) {
                    GearValue gv = new() {
                        Value = parsed,
                        GearLineIndex = gearLineIndex,
                        GearCharIndex = gearCharIndex 
                    };

                    gearValues.Add(gv);
                } else {
                    //Console.WriteLine($"INVALID on line {lineIndex + 1} - {joined}");
                }
            }

            singleNumber.Clear();
            valid = false;
        }


        foreach(var gv in gearValues) {
            var t = gearValues.Where(g => g.GearCharIndex == gv.GearCharIndex && g.GearLineIndex == gv.GearLineIndex).ToList();

            if (t.Count != 2) {
                gearValues = gearValues.Except(t)
                    .OrderBy(g => g.GearLineIndex)
                    .ThenBy(g => g.GearCharIndex)
                    .ToList();
            }

        }

        int index = -1;
        foreach(var gv in gearValues) {
            index++;
            if (index % 2 == 0) continue;

            var ls = gearValues.Where(g => g.GearCharIndex == gv.GearCharIndex && g.GearLineIndex == gv.GearLineIndex && g.Value != gv.Value).FirstOrDefault();


            if (ls == null) throw new Exception("not found");

            var gearRatio = gv.Value * ls.Value;

            Console.WriteLine($"{gv.Value} * {ls.Value} = {gearRatio}");

            totalSum += gearRatio;
        }

        Console.WriteLine(totalSum);
    }
}
