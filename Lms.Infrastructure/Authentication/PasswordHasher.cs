using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace Lms.Infrastructure.Authentication
{
    public sealed class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Itrations = 600_000;

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Itrations,
                HashAlgorithmName.SHA256,
                HashSize);

            return string.Join(
                ".",
                Itrations,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        public bool Verify(string password, string passwordHash)
        {
            var parts = passwordHash.Split('.');

            if (parts.Length != 3 || !int.TryParse(parts[0], out var iterations))
                return false;

            var salt = Convert.FromBase64String(parts[1]);
            var expectedhash = Convert.FromBase64String(parts[2]);

            var actualhash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Itrations,
                HashAlgorithmName.SHA256,
                HashSize);

            return CryptographicOperations.FixedTimeEquals(actualhash, expectedhash);
        }
    }
}
