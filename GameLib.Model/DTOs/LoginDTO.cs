using System.ComponentModel.DataAnnotations.Schema;

namespace GameLib.Model.DTOs
{
    [NotMapped]
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}