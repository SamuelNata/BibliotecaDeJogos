using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.ViewModel
{
    public class CreateUserVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage="Qual seu nome?")]
        [Display(Name = "Nome ou apelido")]
        public string Name { get; set; }

        [Required(ErrorMessage="Precisamos de uma senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}