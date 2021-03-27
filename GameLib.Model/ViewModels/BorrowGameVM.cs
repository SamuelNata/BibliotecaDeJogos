using System;
using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.ViewModel
{
    public class BorrowGameVM
    {
        [Required(ErrorMessage="Qual jogo será emprestado?")]
        [Display(Name="Jogo")]
        public Guid? GameOwnershipId { get; set; }

        [Required(ErrorMessage="Informe a data de devolução")]
        [Display(Name = "Devolução esperada em")]
        public DateTime PredictedDevolution { get; set; }

        [Required(ErrorMessage="Desculpe, quem vai pegar o jogo emprestado?")]
        [Display(Name = "Emprestado a")]
        public Guid UserGetingBorrowedId { get; set; }
    }
}