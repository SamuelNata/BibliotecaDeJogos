using System;
using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.ViewModel
{
    public class BorrowGameVM
    {
        [Required(ErrorMessage="Qual jogo será emprestado?")]
        public Guid? GameOwnershipId { get; set; }

        [Required(ErrorMessage="Informe a data de devolução.")]
        public DateTime PredictedDevolution { get; set; }

        [Required(ErrorMessage="Desculpe, quem vai pegar o jogo emprestado?")]
        public Guid UserGetingBorrowedId { get; set; }
    }
}