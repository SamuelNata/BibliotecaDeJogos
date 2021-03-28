using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameLib.Model.DTOs
{
    [NotMapped]
    public class GameInfoDTO
    {
        public Guid OwnedGameRelationId { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public Guid GameOwnerId { get; set; }
        public string GameOwnerName { get; set; }
        public Guid? CurrentBorrowingId { get; set ;}
        public DateTime? BorrowDate { get; set; }
        public DateTime? ExpectedDevolutionDate { get; set; }
        public DateTime? RealDevolutionDate { get; set; }
        public Guid? BorrowerId { get; set; }
        public string BorrowerName { get; set; }
    }
}