using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameLib.Model;

namespace GameLib.Model.DTOs
{
    [NotMapped]
    public class SignInUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
