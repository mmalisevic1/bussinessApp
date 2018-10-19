using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace server.services.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }

        [MaxLength(200, ErrorMessage = "Email can contain 200 characters maximum.")]
        [Required]
        public string Email { get; set; }

        [MaxLength(100, ErrorMessage = "First name can contain 200 characters maximum.")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(150, ErrorMessage = "Last name can contain 150 characters maximum.")]
        [Required]
        public string LastName { get; set; }

        [StringLength(200, MinimumLength = 5, ErrorMessage = "Password must contain 5 characters at least and can contain up to 200 characters max.")]
        [Required]
        public string Password { get; set; }

        [DefaultValue(true)]
        public bool? IsActive { get; set; } = true;

        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
