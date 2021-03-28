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
        public string Nickname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
