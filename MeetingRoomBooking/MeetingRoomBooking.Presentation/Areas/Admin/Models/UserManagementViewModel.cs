using System.ComponentModel.DataAnnotations;

public class UserManagementViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "User Name is required.")]
    public string UserName { get; set; }

    public string Pin { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }

    public string Phone { get; set; }

    public string Department { get; set; }

    public string Designation { get; set; }

    public bool Status { get; set; }

    public IEnumerable<string> Roles { get; set; }
}
