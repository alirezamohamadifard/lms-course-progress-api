using Lms.Domain.Entity.Common;
using Lms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Domain.Entity
{
    public class User : BaseEntity
    {
        public string FullName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string HashPassword { get; private set; } = null!;
        public UserRole Role { get; private set; }
        private User()
        {
        }
        public User(string fullName, string email, string hashPassword, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.", nameof(fullName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));
            FullName = fullName;
            Email = email;
            HashPassword = hashPassword;
            Role = role;
            CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
