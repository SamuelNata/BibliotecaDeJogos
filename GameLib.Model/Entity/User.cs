using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameLib.Model;

namespace GameLib.Model.Entity
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Display(Name="Apelido")]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Login")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Senha")]
        public string Password { get; set; }
    }
}
