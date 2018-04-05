## Run Statistics

Collects various statistics throughout each run and outputs them to a file when the player crashes, advances to the next world, or resets the current map.

## Output

The statistics of each run are added to a file called `rundata.csv` located in `C:\Users\YourUsername\Documents\SFMF`.

The CSV file contains the following data points:

- Seed
- Alphanumeric seed (if one exists)
- Accumulated score
  - Includes the total score achieved and any combo that was broken by crashing.
- Secured score
  - Only includes the total score achieved, broken combos are excluded.
- Ending
  - Possible endings are `Reset`, `NextWorld`, or `Death`.
- Score per second
  - A pipe (`|`) separated list of the scores achieved during each second of a player's run.

### Example `rundata.csv`

```
Seed,Alphanumeric Seed,Accumulated Score,Secured Score,Ending,Score Per Second
-1926281526,rekkerd,8308,0,Death,0|0|0|175|3180|3221|301|1431
-835280784,,229,0,Reset,0|0|34|195
```

## What to do with it

Import the CSV file into Microsoft Excel or Google Sheets and chart your score per second. It could potentially help choose between two obstacles in a run to see which one is actually generating more points.

Here's an graph that I generated from one of my highest scoring runs (210k on seed "rekkerd"):

[[https://github.com/vicjohnson1213/Superflight-Mods/blob/master/RunStatistics/images/rekkerd.png]]
