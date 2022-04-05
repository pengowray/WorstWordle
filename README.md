# WorstWordle

Finds the most ambiguous ("worst") four-letter combos in Wordle

```
Wordle 282 6/6

⬜🟩🟩🟩🟩
⬜🟩🟩🟩🟩
⬜🟩🟩🟩🟩
⬜🟩🟩🟩🟩
⬜🟩🟩🟩🟩
🟩🟩🟩🟩🟩
```

# Output

Worst combos, ordered by most short list solutions:

-  **_IGHT** (9 + 6 = 15): eight fight light might night right sight tight wight *aight *bight *dight *hight *kight *pight
-  **_OUND** (8 + 1 = 9): bound found hound mound pound round sound wound *lound
-  **_ATCH** (7 + 3 = 10): batch catch hatch latch match patch watch *gatch *natch *ratch
-  **_OWER** (7 + 3 = 10): cower lower mower power rower sower tower *bower *dower *vower
-  **SHA_E** (7): shade shake shale shame shape share shave
-  **_AUNT** (6 + 2 = 8): daunt gaunt haunt jaunt taunt vaunt *naunt *saunt
-  **_ILLY** (6 + 2 = 8): billy dilly filly hilly silly willy *gilly *tilly
-  **STA_E** (6 + 2 = 8): stage stake stale stare state stave *stade *stane
-  **_ATTY** (6 + 1 = 7): batty catty fatty patty ratty tatty *natty
-  **GRA_E** (6 + 1 = 7): grace grade grape grate grave graze *grame
-  **S_ORE** (6 + 1 = 7): score shore snore spore store swore *smore
-  **_ASTE** (6): baste caste haste paste taste waste
-  [more...](https://gist.github.com/pengowray/5fc804cd2130e11ab7708263c71fcc79)

More lists:
- [shortlist-focus.txt](https://gist.github.com/pengowray/5fc804cd2130e11ab7708263c71fcc79) (beginning shown above) — Worst combos ordered by most short list solutions
- [longlist-focus.txt](https://gist.github.com/pengowray/f0ca3dfd8762418774f2b7df2a8955ff) — Worst combos ordered by most most allowed guesses from long list (excludes words without a shortlist solution)
- [full-list.txt](https://gist.github.com/pengowray/440abfb12b8059c750ef0105e8c10bdf) — For completeness sake. Includes combos with only long-list answers but no valid short list answer.
