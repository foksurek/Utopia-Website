# Utopia-Website

Utopia osu! server offical website

remeber to add appsettings.json to your project

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "urls" : {
    "Base": "app url e.g. https://doamin.xyz/",
    "Assets": "assets url e.g. https://doamin.xyz/assets or https://assets.doamin.xyz/"
  },
  "//": "remember to adjust your code to be compatible with the api, and to change endpoints to your own",
  "ApiUrls" : {
    "GetLeaderboardTop": "https://YOUR_DOMAIN/v1/get_leaderboard?mode=0&limit=5",
    "GetLeaderboardTopRelax": "https://YOUR_DOMAIN/v1/get_leaderboard?mode=4&limit=5",
    "GetPlayerCount" : "https://YOUR_DOMAIN/v1/get_player_count",
    "GetPlayerInfo": "https://YOUR_DOMAIN/v1/get_player_info?scope=all",
    "GetPlayerStatus": "https://YOUR_DOMAIN/v1/get_player_status",
    "GetPlayerTopScores": "https://YOUR_DOMAIN/v1/get_player_scores?scope=best",
    "GetPlayerTopScoresRelax": "https://YOUR_DOMAIN/v1/get_player_scores?scope=best&mode=4",
    "SearchPlayers": "https://YOUR_DOMAIN/v1/search_players"
  },
  "FilePaths" : {
    "Avatars": "Absolute Path to Avatars folder e.g. on linux /Data/Avatars | on windows C://.....",
    "Replays": "Absolute Path to Replays folder"
  },
  "AllowedHosts": "*"
}
```

use `.env` for MySQL connection

You have to create databse, we're using [THIS](https://github.com/osuAkatsuki/bancho.py/) as a osu! server
Thanks [cmyui](https://github.com/cmyui) and [Bancho.py](https://github.com/osuAkatsuki/bancho.py/) for making the open source osu server
