using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lms.Application.Contracts.Auth
{
    public sealed class RegisterRequest
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; init; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; init; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; init; } = string.Empty;
    }
}
