// Find the most ambiguous ("worst") four-letter combos in Wordle. Streak killers


//string wordle_long_list = @"Data\wordle-allowed-guesses.txt";
//string wordle_short_list = @"Data\wordle-answers-alphabetical.txt";
string wordle_long_list = @"Data\wordle-nyt-allowed-guesses.txt";
string wordle_short_list = @"Data\wordle-nyt-answers-alphabetical.txt";

string output_long_list_focus = "output-longlist-focus.txt";
string output_short_list_focus = "output-shortlist-focus.txt";
string output_full_long_list_focus = "output-full-list.txt";

string prefix = ""; //  "- "; // list prefix

Console.WriteLine("Finding the most ambiguous (\"worst\") four-letter combos in Wordle...");
Console.WriteLine();

var longList = File.ReadAllLines(wordle_long_list).OrderBy(w => w); // sorting is redundant but do anyway
var shortList = File.ReadAllLines(wordle_short_list).OrderBy(w => w);

var longCombos = GetCombos(shortList.Concat(longList)); // Note: Order matters here: displaying lists with default sorting shows short list items then long list
var shortCombos = GetCombos(shortList);

Dictionary<string, List<String>> GetCombos(IEnumerable<string> words) {
    // e.g. "wor_y" => ["wordy", "worry"]
    Dictionary<string, List<String>> combos = new();

    foreach (var word in words) {
        if (string.IsNullOrWhiteSpace(word)) continue;
        if (word.StartsWith("//")) continue;
        if (word.Length != 5) continue;

        for (int i = 0; i < word.Length; i++) {
            char[] chars = word.ToCharArray();
            chars[i] = '_';
            string underscored = new string(chars); // e.g. "_IGHT" or "F_GHT"

            if (combos.ContainsKey(underscored)) {
                combos[underscored].Add(word);
            } else {
                var newList = new List<string>();
                newList.Add(word);
                combos[underscored] = newList;
            }
        }
    }
    return combos;
}


void OutputLongFocus() {
    using StreamWriter writer = new(output_long_list_focus);

    writer.WriteLine("Worst combos ordered by most long list solutions: List of the most ambiguous four-letter wordle combos, ordered by number of allowed guesses. Only includes combos with at least one real answer from the short list. Count is given as ('total' = 'short list solutions' + 'additional long list solutions'). A '*' means the guess is allowed but not in the short list of possible answers. Source: https://github.com/pengowray/WorstWordle");
    writer.WriteLine("");

    var worstShortListCombos = shortCombos
        //.OrderByDescending(l => l.Value.Count).ThenByDescending(l => longCombos[l.Key].Count).ThenBy(l => l.Key) // by short list count
        .OrderByDescending(l => longCombos[l.Key].Count).ThenBy(l => l.Key) // by long list count
        //.Where(l => l.Value.Count >= 2 || (l.Value.Count == 1 && longCombos[l.Key].Count >= 2)); // only if 2 or more items and 1 is in the short list
        .Where(l => l.Value.Any()); // at least 1 short list

    foreach (var item in worstShortListCombos) {

        var key = item.Key;
        var shortListGroup = item.Value;
        var longListGroup = longCombos[key];

        var nonAnswersButValid = longListGroup.Count - shortListGroup.Count;

        if (nonAnswersButValid > 0) {
            // put asterix on non-answer words
            var longListGroupStarred = longListGroup
                //.OrderBy(word => word)
                .Select(word => GiveStar(key, word, shortListGroup));

            //string count = $"({shortListGroup.Count} + {nonAnswersButValid})";
            //string count = $"({shortListGroup.Count} / {longListGroup.Count})";
            //string count = $"({longListGroup.Count})";
            string count = $"({longListGroup.Count} = {shortListGroup.Count} + {nonAnswersButValid})";
            writer.WriteLine($"{prefix}{key.ToUpper()} {count}: {string.Join(" ", longListGroupStarred)}");
        } else {
            string count = $"({shortListGroup.Count})";
            //.Select(word => GiveStar(key, word, shortListGroup))
            // note: never has "*"
            writer.WriteLine($"{prefix}{key.ToUpper()} {count}: {string.Join(" ", shortListGroup.Select(word => GiveStar(key, word, shortListGroup)))}");
        }
    }
    writer.Flush();
    writer.Close();
}
OutputLongFocus();

void OutputShortFocus() {
    using StreamWriter writer = new(output_short_list_focus);

    writer.WriteLine("Worst combos ordered by most short list solutions: List of the most ambiguous four-letter wordle combos, ordered by most number of possible short list solutions. Count is given as ('short list solutions' + 'additional long list solutions' = 'total'). A '*' means the guess is allowed but not in the short list of possible answers. Source: https://github.com/pengowray/WorstWordle");
    writer.WriteLine("");

    var worstShortListCombos = shortCombos
        .OrderByDescending(l => l.Value.Count).ThenByDescending(l => longCombos[l.Key].Count).ThenBy(l => l.Key) // by short list count
        //.Where(l => l.Value.Count >= 2 || (l.Value.Count == 1 && longCombos[l.Key].Count >= 2)); // only if 2 or more items and 1 is in the short list
        .Where(l => l.Value.Any()); // at least 1 short list

    foreach (var item in worstShortListCombos) {
        var key = item.Key;
        var shortListGroup = item.Value;
        var longListGroup = longCombos[key];
        var nonAnswersButValid = longListGroup.Count - shortListGroup.Count;

        if (nonAnswersButValid > 0) {
            // put asterix on non-answer words
            var longListGroupStarred = longListGroup
                //.OrderBy(word => word) // note: default order puts short list first
                .Select(word => GiveStar(key, word, shortListGroup));

            //string count = $"({shortListGroup.Count} / {longListGroup.Count})";
            //string count = $"({longListGroup.Count})";
            //string count = $"({shortListGroup.Count} + {nonAnswersButValid})";
            //string count = $"({longListGroup.Count} = {shortListGroup.Count} + {nonAnswersButValid})";
            string count = $"({shortListGroup.Count} + {nonAnswersButValid} = {longListGroup.Count})";

            writer.WriteLine($"{prefix}{key.ToUpper()} {count}: {string.Join(" ", longListGroupStarred)}");
        } else {
            string count = $"({shortListGroup.Count})";
            writer.WriteLine($"{prefix}{key.ToUpper()} {count}: {string.Join(" ", shortListGroup)}");
        }
    }
    writer.Flush();
    writer.Close();
}
OutputShortFocus();


void OutputLongList() {
    using StreamWriter writer = new(output_full_long_list_focus);

    writer.WriteLine("Full list of worst combos ordered by most long list solutions: List of the most ambiguous four-letter wordle combos, ordered by number of possible long list solutions. Includes combos even without a real answer from the short list. Count is given as ('total' = 'short list solutions' + 'additional long list solutions'). A '*' means the guess is allowed but not in the short list of possible answers. Source: https://github.com/pengowray/WorstWordle");
    writer.WriteLine("");

    var worstLongListCombos = longCombos
        .OrderByDescending(l => l.Value.Count) // by long count
        .ThenByDescending(l => shortCombos.ContainsKey(l.Key) ? shortCombos[l.Key].Count : 0)
        .ThenBy(l => l.Key);

    foreach (var longItem in worstLongListCombos) {

        var key = longItem.Key;
        var longListGroup = longItem.Value;
        var shortListGroup = shortCombos.ContainsKey(key) ? shortCombos[key] : new List<string>();
        var nonAnswersButValid = longListGroup.Count - shortListGroup.Count;

        // put asterix on non-answer words
        var longListGroupStarred = longListGroup
            //.OrderBy(word => word)
            .Select(word => GiveStar(key, word, shortListGroup));

        string count = $"({longListGroup.Count} = {shortListGroup.Count} + {nonAnswersButValid})";
        writer.WriteLine($"{prefix}{key.ToUpper()} {count}: {string.Join(" ", longListGroupStarred)}");
    }
    writer.Flush();
    writer.Close();
}
OutputLongList();

string GiveStar(string underscored, string solution, IEnumerable<string> shortGroup) {
    string longlistStar = shortGroup.Contains(solution) ? "" : "*";
    //string nowayStar = DoubleLetter(underscored, solution) ? "" : "‡";
    string nowayStar = ""; // actually nah don't mark just because same letter twice.

    return $"{nowayStar}{longlistStar}{solution}";
}
bool DoubleLetter(string underscored, string solution) {
    int underscore = underscored.IndexOf('_');
    if (underscore == -1) {
        return true; // error / shouldn't happen
    }
    var ch = solution.Substring(underscore, 1);
    if (underscored.Contains(ch)) {
        // solution has same letter twice. That can actually happen.
        return false;
    }
    return true;
}

Console.WriteLine("Done.");
