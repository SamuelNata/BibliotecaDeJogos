using System;
using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.Entity
{
    public class Game : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Ano de lançamento")]
        public short? Year { get; set; }
    }
}
