using System;
using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.DTOs
{
    public class GameBorrowingDTO
    {
        [Display(Name="Id")]
        public Guid BorrowingId { get; set ;}
        
        [Display(Name="Jogo")]
        public string Game { get; set; }

        [Display(Name="Emprestado a")]
        public string Borrower { get; set; }

        [Display(Name="Emprestado em")]
        public DateTime BorrowDate { get; set; }

        [Display(Name="Devolução esperada")]
        public DateTime ExpectedDevolutionDate { get; set; }

        [Display(Name="Devolvido em")]
        public DateTime? RealDevolutionDate { get; set; }
    }
}