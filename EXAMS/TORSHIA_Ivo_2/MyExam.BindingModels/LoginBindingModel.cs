namespace MyExam.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginBindingModel
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}