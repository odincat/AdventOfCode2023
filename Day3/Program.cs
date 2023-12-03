var inputLines = File.ReadAllLines("./input.txt");
var exampleInputLines = File.ReadAllLines("./input-example.txt");
var inputTest = File.ReadAllLines("./input-test.txt");

//Part1.Run(inputLines);
Part2.Run(inputLines);

// List<char> chars = new();
// foreach (var l in inputLines) {
//     foreach(char c in l) {
//         if (!char.IsNumber(c) && !chars.Contains(c)) {
//             chars.Add(c);
//         }
//     }
// }
//
// foreach (var cc in chars) {
//     Console.WriteLine(cc);
// }
