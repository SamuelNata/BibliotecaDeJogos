using System;
using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.ViewModel
{
    public class CreateUserGameVM
    {
        [Required]
        [Display(Name="Jogo")]
        public Guid GameId { get; set; }

        public Guid UserId { get; set; }
    }
}