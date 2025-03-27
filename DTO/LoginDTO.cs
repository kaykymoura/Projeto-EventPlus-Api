using System.ComponentModel.DataAnnotations;

namespace webapi.event_.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Informe o e-mail do usuario!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuario!")]
        public string? Senha { get; set; }
    }
}
