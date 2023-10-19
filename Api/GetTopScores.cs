namespace Utopia.Api;

public class GetTopScores
{
    public string status { get; set; }
    public Scores[] scores { get; set; }
    public Player player { get; set; }
}

public class Scores
{
    public int id { get; set; }
    public int score { get; set; }
    public double pp { get; set; }
    public double acc { get; set; }
    public int max_combo { get; set; }
    public int mods { get; set; }
    public int n300 { get; set; }
    public int n100 { get; set; }
    public int n50 { get; set; }
    public int nmiss { get; set; }
    public int ngeki { get; set; }
    public int nkatu { get; set; }
    public string grade { get; set; }
    public int status { get; set; }
    public int mode { get; set; }
    public string play_time { get; set; }
    public int time_elapsed { get; set; }
    public int perfect { get; set; }
    public Beatmap beatmap { get; set; }
}



