namespace Utopia.Api;

public class SearchPlayers
{
    public string status { get; set; }
    public int results { get; set; }
    public Result[] result { get; set; }
}
public class Result
{
    public int id { get; set; }
    public string name { get; set; }
}

