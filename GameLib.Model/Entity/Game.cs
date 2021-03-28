using System;
using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.Entity
{
    public class Game : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public short? Year { get; set; }
    }
}
