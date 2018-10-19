using server.data.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.data.Tables
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(200, ErrorMessage = "Email can contain 200 characters maximum.")]
        [NonUnicode]
        [Required]
        public string Email { get; set; }

        [MaxLength(100, ErrorMessage = "First name can contain 200 characters maximum.")]
        [NonUnicode]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(150, ErrorMessage = "Last name can contain 150 characters maximum.")]
        [NonUnicode]
        [Required]
        public string LastName { get; set; }

        [NonUnicode]
        [Required]
        public string Password { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
