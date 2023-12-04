public static class Part2 {
    const char SEPARATE_CARD_ID = ':';
    const char SET_SPLIT = '|';

    class Card {
        public int Id { get; set; }
        public int Worth { get; set; }
        public int Amount { get; set; }
    }

    static List<Card> scratchcards = new();

    static int totalSum = 0;

    public static void Run(string[] input) {
        int lineIndex = -1;

        foreach (var line in input) {
            lineIndex++;

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
                    cardWorth += 1;
                }
            }

            Card c = new() {
                Id = lineIndex,
                Worth = cardWorth,
                Amount = 1
            };

            scratchcards.Add(c);
        }

        foreach (var card in scratchcards) {
            if (card.Worth > 0) {
                for (int i = 1; i <= card.Worth; i++) {
                    if (card.Id + i < scratchcards.Count) {
                        scratchcards[card.Id + i].Amount += card.Amount;
                    }
                }
            }
        }

        foreach(var card in scratchcards) {
            totalSum += card.Amount;
        }

        Console.WriteLine(totalSum);
    }
}
