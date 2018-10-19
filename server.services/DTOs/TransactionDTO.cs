using System;
using System.ComponentModel.DataAnnotations;

namespace server.services.DTOs
{
    public class TransactionDTO
    {
        public long UserId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Type { get; set; }

        [MaxLength(4000, ErrorMessage = "Description can contain 4000 characters maximum.")]
        [Required]
        public string Description { get; set; }

        public decimal? Charge { get; set; } = 0;

        public decimal? Deposit { get; set; } = 0;

        [MaxLength(4000, ErrorMessage = "Notes can contain 4000 characters maximum.")]
        public string Notes { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
