namespace Utopia.Interfaces;

public interface IRequests
{
    Task<string> GetAsync(string url);
}