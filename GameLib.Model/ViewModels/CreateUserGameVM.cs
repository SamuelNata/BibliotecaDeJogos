using System;
using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.ViewModel
{
    public class CreateUserGameVM
    {
        [Required]
        public Guid GameId { get; set; }
    }
}