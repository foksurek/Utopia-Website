namespace Utopia.Dto;

public class Map
{
    public int SetID { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Creator { get; set; }
    public string Source { get; set; }
    public string Tags { get; set; }
    public int RankedStatus { get; set; }
    public int Genre { get; set; }
    public int Language { get; set; }
    public int Favourites { get; set; }
    public bool HasVideo { get; set; }
    public string SubmittedDate { get; set; }
    public string ApprovedDate { get; set; }
    public string LastUpdate { get; set; }
    public List<Beatmap> ChildrenBeatmaps { get; set; }
}

public class Beatmap
{
    public int ParentSetID { get; set; }
    public int BeatmapID { get; set; }
    public int TotalLength { get; set; }
    public int HitLength { get; set; }
    public string DiffName { get; set; }
    public string FileMD5 { get; set; }
    public double CS { get; set; }
    public double AR { get; set; }
    public double HP { get; set; }
    public double OD { get; set; }
    public int Mode { get; set; }
    public string BPM { get; set; }
    public int Playcount { get; set; }
    public int Passcount { get; set; }
    public int MaxCombo { get; set; }
    public double DifficultyRating { get; set; }
}
