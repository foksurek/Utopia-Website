namespace Utopia.Models;

public class DbConnectionModel
{
    public string Host { get; set; } = null!;
    public string Port { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}