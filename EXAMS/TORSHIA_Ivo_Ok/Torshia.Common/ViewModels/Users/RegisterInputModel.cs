namespace Torshia.Common.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+\.)+[a-z]{2,5}$")]
        public string Email { get; set; }
    }
}