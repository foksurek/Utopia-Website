using Newtonsoft.Json;

namespace Utopia.Models.ApiModels;

public class GetPlayerInfo
{
    public string status { get; set; }
    public Player player { get; set; }
}

public class Player
{
    public Info info { get; set; }
}

public class Info
{
    public int id { get; set; }
    public string name { get; set; }
    public string safe_name { get; set; }
    public int priv { get; set; }
    public string country { get; set; }
    public int silence_end { get; set; }
    public int donor_end { get; set; }
    public int creation_time { get; set; }
    public int latest_activity { get; set; }
    public int clan_id { get; set; }
    public int clan_priv { get; set; }
    public int preferred_mode { get; set; }
    public int play_style { get; set; }
    public object custom_badge_name { get; set; }
    public object custom_badge_icon { get; set; }
    public object userpage_content { get; set; }
}
