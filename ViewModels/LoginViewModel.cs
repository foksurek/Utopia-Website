using System.ComponentModel.DataAnnotations;

namespace Utopia.ViewModels;

public class LoginViewModel
{
    
    [Required(ErrorMessage = "This field is required.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}