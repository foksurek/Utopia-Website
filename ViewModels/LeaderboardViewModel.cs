namespace Utopia.ViewModels;

public class LeaderboardViewModel
{
    public string Name { get; set; }
    public int Id { get; set; }
    public int Pp { get; set; }
    public int PlayCount { get; set; }
    public double Acc { get; set; }
    
    public string Country { get; set; }
    public long DonorEnd { get; set; }
    public int Priv { get; set; }
    public long TScore { get; set; }
}