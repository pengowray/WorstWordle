// Find the most ambiguous ("worst") four-letter combos in Wordle

// e.g. "BU_ON" => ["BUXON"]
Dictionary<string, List<String>> list = new();

string wordle_allowed_guesses = @"Data\wordle-allowed-guesses.txt";
string wordle_answers_alphabetical = @"Data\wordle-answers-alphabetical.txt";

//pick word list:
//string wordlist_file = wordle_allowed_guesses;
string wordlist_file = wordle_answers_alphabetical;

Console.WriteLine("Finding the most ambiguous (\"worst\") four-letter combos in Wordle.");
Console.WriteLine(wordlist_file);
Console.WriteLine();

var wordlist = File.ReadAllLines(wordlist_file);


foreach (var word in wordlist) {
    if (string.IsNullOrWhiteSpace(word)) continue;
    if (word.StartsWith("//")) continue;
    if (word.Length != 5) continue;

    for (int i = 0; i < word.Length; i++) {
        char[] chars = word.ToCharArray();
        chars[i] = '_'; 
        string underscored = new string(chars); // e.g. "_IGHT" or "F_GHT"

        if (list.ContainsKey(underscored)) {
            list[underscored].Add(word);
        } else {
            var newList = new List<string>();
            newList.Add(word);
            list[underscored] = newList;
        }
    }
}

//sort list
var worstCombos = list.OrderByDescending(l => l.Value.Count).ThenBy(l => l.Key)
    .Where(l => l.Value.Count >= 2); // only when combo has 2+ words
    //.Take(100); // only first 100

//print list
foreach (var item in worstCombos) {
    Console.WriteLine($"{item.Value.Count}. {item.Key}: {string.Join(" ", item.Value)}");
}

//results:
// - https://gist.github.com/pengowray/0b09d20f0819f338ec0d3caded0474f1#file-worst-answers-txt
// - https://gist.github.com/pengowray/0b09d20f0819f338ec0d3caded0474f1#file-worst-allowed-guesses-txt