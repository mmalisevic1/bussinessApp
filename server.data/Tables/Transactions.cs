using server.data.CustomAttributes;
using server.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.data.Tables
{
    public class Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }

        public DateTime Date { get; set; }

        public TRANSACTION_TYPE Type { get; set; }

        [MaxLength(4000, ErrorMessage = "Description can contain 4000 characters maximum.")]
        [NonUnicode]
        [Required]
        public string Description { get; set; }

        public decimal? Charge { get; set; }

        public decimal? Deposit { get; set; }

        [MaxLength(4000, ErrorMessage = "Notes can contain 4000 characters maximum.")]
        [NonUnicode]
        public string Notes { get; set; }

        public DateTime? CreatedOn { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }
    }
}
