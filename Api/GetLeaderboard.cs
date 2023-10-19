namespace Utopia.Api;

public class GetLeaderboard
{
    public string status { get; set; }
    public Leaderboard[] leaderboard { get; set; }
}
public class Leaderboard
{
    public int player_id { get; set; }
    public string name { get; set; }
    public string country { get; set; }
    public int tscore { get; set; }
    public int rscore { get; set; }
    public int pp { get; set; }
    public int plays { get; set; }
    public int playtime { get; set; }
    public double acc { get; set; }
    public int max_combo { get; set; }
    public int xh_count { get; set; }
    public int x_count { get; set; }
    public int sh_count { get; set; }
    public int s_count { get; set; }
    public int a_count { get; set; }
    public object clan_id { get; set; }
    public object clan_name { get; set; }
    public object clan_tag { get; set; }
}

