namespace Utopia.Models;

public enum GamemodeEnum
{
    Std = 0,
    Taiko = 1,
    Fruits = 2,
    Mania = 3,

    StdRx = 4,
    TaikoRx = 5,
    FruitsRx = 6,
    ManiaRx = 7,
    
    StdAp = 8,
    TaikoAp = 9,
    FruitsAp = 10,
    ManiaAp = 11
}
public class GamemodeModel
{
    public int id { get; set; }
    public int tscore { get; set; }
    public int rscore { get; set; }
    public int pp { get; set; }
    public int plays { get; set; }
    public int playtime { get; set; }
    public double acc { get; set; }
    public int max_combo { get; set; }
    public int total_hits { get; set; }
    public int replay_views { get; set; }
    public int xh_count { get; set; }
    public int x_count { get; set; }
    public int sh_count { get; set; }
    public int s_count { get; set; }
    public int a_count { get; set; }
    public int rank { get; set; }
    public int country_rank { get; set; }
}