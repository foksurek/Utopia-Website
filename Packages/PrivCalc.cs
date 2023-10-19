using Utopia.Models;

namespace Utopia.Packages;

public class PrivCalc
{ 
    public static string? GetHighestRank(int privileges)
    {
        return privileges switch
        {
            >= (int)Privileges.DEVELOPER => "DEVELOPER",
            >= (int)Privileges.ADMINISTRATOR => "ADMINISTRATOR",
            >= (int)Privileges.MODERATOR => "MODERATOR",
            >= (int)Privileges.NOMINATOR => "NOMINATOR",
            >= (int)Privileges.TOURNEY_MANAGER => "TOURNEY_MANAGER",
            >= (int)Privileges.ALUMNI => "ALUMNI",
            _ => ""
        };
    }
}