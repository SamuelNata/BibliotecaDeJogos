using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameLib.Model.DTOs
{
    [NotMapped]
    public class GameBorrowingDTO
    {
        public Guid BorrowingId { get; set ;}
        public string Game { get; set; }
        public string Borrower { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ExpectedDevolutionDate { get; set; }
        public DateTime? RealDevolutionDate { get; set; }
    }
}