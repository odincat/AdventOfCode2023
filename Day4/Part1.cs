public static class Part1 {
    const char SEPARATE_CARD_ID = ':';
    const char SET_SPLIT = '|';

    static int totalSum = 0;

    public static void Run(string[] input) {
        foreach (var line in input) {
            var cardIdSplit = line.Split(SEPARATE_CARD_ID);

            var numberSets = cardIdSplit[1];

            var numberSetSplit = numberSets.Split(SET_SPLIT);


            List<int> winningNumbers = new();
            var winningNumbersRaw = numberSetSplit.First();
            foreach(var winningNumberRaw in winningNumbersRaw.Split(' ')) {
                if (!string.IsNullOrEmpty(winningNumberRaw)) {
                    winningNumbers.Add(int.Parse(winningNumberRaw));
                }
            }

            List<int> givenNumbers = new();
            var givenNumbersRaw = numberSetSplit.Last();
            foreach(var givenNumberRaw in givenNumbersRaw.Split(' ')) {
                if (!string.IsNullOrEmpty(givenNumberRaw)) {
                    givenNumbers.Add(int.Parse(givenNumberRaw));
                }
            }

            int cardWorth = 0;

            foreach (var num in givenNumbers) {
                if (winningNumbers.Contains(num)) {
                    if (cardWorth == 0) {
                        cardWorth = 1;
                    } else {
                        cardWorth = cardWorth * 2;
                    }
                }
            }

            totalSum += cardWorth;
        }

        Console.WriteLine(totalSum);
    }
}
