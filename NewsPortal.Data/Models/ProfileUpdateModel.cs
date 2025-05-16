using System.ComponentModel.DataAnnotations;

public class ProfileUpdateModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
