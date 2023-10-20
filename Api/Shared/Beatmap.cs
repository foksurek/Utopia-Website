namespace Utopia.Models.ApiModels.Shared;

public class Beatmap
{
    public string md5 { get; set; }
    public int id { get; set; }
    public int setId { get; set; }
    public string artist { get; set; }
    public string title { get; set; }
    public string version { get; set; }
    public string creator { get; set; }
    public string last_update { get; set; }
    public int total_length { get; set; }
    public int max_combo { get; set; }
    public int status { get; set; }
    public int plays { get; set; }
    public int passes { get; set; }
    public int mode { get; set; }
    public double bpm { get; set; }
    public double cs { get; set; }
    public double od { get; set; }
    public double ar { get; set; }
    public double hp { get; set; }
    public double diff { get; set; }
}