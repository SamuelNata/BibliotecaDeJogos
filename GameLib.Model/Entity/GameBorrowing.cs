using System;
using System.ComponentModel.DataAnnotations;

namespace GameLib.Model.Entity
{
    public class GameBorrowing : BaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime PredictedEndDate { get; set; }
        
        public DateTime? RealEndDate { get; set; }

        [Required]
        public Guid GameOwnershipId { get; set; }

        [Required]
        public Guid GameBorrowerId { get; set; }


        public virtual UserGame GameOwnership { get; set; }
        
        public virtual User GameBorrower { get; set; }
    }
}
