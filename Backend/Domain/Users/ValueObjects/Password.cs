using Domain.Base;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Domain.Users.ValueObjects
{
    public class Password : ValueObject
    {
        private static readonly int SaltLength = 8;
        private static readonly int Pbkdf2Iterations = 10000;
        private static readonly int KeyLength = 8;

        private Password(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        public byte[] PasswordHash { get; }
        public byte[] PasswordSalt { get; }

        public static Result<Password> Create(string password, byte[] salt = null)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Failure<Password>("Password was not provided.");
            }
            password = password.Trim();

            if(password.Length < 8)
            {
                return Result.Failure<Password>("Password is too short (at least 8 characters).");
            }
            
            if(password.Length > 128)
            {
                return Result.Failure<Password>("Password is too long (maximum l28 characters).");
            }

            byte[] passwordSalt;
            if (salt == null)
            {
                passwordSalt = CreatePasswordSalt();
            }
            else
            {
                passwordSalt = salt;
            }
            byte[] passwordHash = CreatePasswordHash(password, passwordSalt);

            return Result.Success(new Password(passwordHash, passwordSalt));
        }

        private static byte[] CreatePasswordHash(string password, byte[] passwordSalt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, passwordSalt,
                    Pbkdf2Iterations, HashAlgorithmName.SHA512))
            {
                return pbkdf2.GetBytes(KeyLength);
            }
        }

        private static byte[] CreatePasswordSalt()
        {
            byte[] salt = new byte[SaltLength];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PasswordHash;
        }
    }
}
