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

Worst combos ordered by most short list solutions:

-  `_ight` (9 + 6): eight fight light might night right sight tight wight *aight *bight *dight *hight *kight *pight
-  `_ound` (8 + 1): bound found hound mound pound round sound wound *lound
-  `_atch` (7 + 3): batch catch hatch latch match patch watch *gatch *natch *ratch
-  `_ower` (7 + 3): cower lower mower power rower sower tower *bower *dower *vower
-  `sha_e` (7): shade shake shale shame shape share shave
-  [**more** (shortlist-focus.txt)](https://gist.github.com/pengowray/5fc804cd2130e11ab7708263c71fcc79)

Worst combos ordered by most most allowed guesses from long list:

-  _ight (15): eight fight light might night right sight tight wight *aight *bight *dight *hight *kight *pight
-  _aker (12): baker maker taker *daker *faker *jaker *laker *naker *oaker *raker *saker *waker
-  _ater (12): cater eater hater later water *dater *gater *mater *oater *pater *rater *tater
-  _erry (11): berry ferry merry *derry *herry *jerry *kerry *perry *serry *terry *verry
-  _iver (11): diver giver liver river *aiver *fiver *hiver *jiver *siver *viver *wiver
-  _olly (11): dolly folly golly holly jolly *colly *lolly *molly *polly *tolly *wolly
-  cha_s (11): chaos *chads *chais *chals *chams *chaps *chars *chats *chavs *chaws *chays
-  la_er (11): lager later layer *lacer *lader *laker *lamer *laser *laver *lawer *laxer
- [**more** (longlist-focus.txt)](https://gist.github.com/pengowray/f0ca3dfd8762418774f2b7df2a8955ff)
- even more for completeness sake: [also includes combos without a short list answer (full-list.txt)](https://gist.github.com/pengowray/440abfb12b8059c750ef0105e8c10bdf)
