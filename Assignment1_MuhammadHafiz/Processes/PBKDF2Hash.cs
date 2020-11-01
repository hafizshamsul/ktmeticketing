using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace Assignment1_MuhammadHafiz.Processes
{
    public class PBKDF2Hash
    {
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20;
        public const int Pbkdf2Iterations = 10000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;
        private string password, hashedpassword, storedpasswordhash;
        private bool passwordcheck;

        // Register user
        public PBKDF2Hash(string password)
        {
            Password = password;
            StoredPasswordHash = storedpasswordhash;
            HashPassword();
        }

        // Login user
        public PBKDF2Hash(string password, string storedpasswordhash)
        {
            Password = password;
            StoredPasswordHash = storedpasswordhash;
            HashPassword();
            ValidatePassword();
        }

        // Properties - input field
        private string Password
        {
            get { return password; }
            set { password = value; }
        }

        // Properties - input field
        private string StoredPasswordHash
        {
            get { return storedpasswordhash; }
            set { storedpasswordhash = value; }
        }

        // Properties - output field
        public string HashedPassword
        {
            get { return hashedpassword; }
            set { hashedpassword = value; }
        }

        // Properties - output field
        public bool PasswordCheck
        {
            get { return passwordcheck; }
            set { passwordcheck = value; }
        }

        protected void HashPassword()
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);
            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            hashedpassword = Pbkdf2Iterations + ":" +
            Convert.ToBase64String(salt) + ":" +
            Convert.ToBase64String(hash);
        }

        protected void ValidatePassword()
        {
            char[] delimiter = { ':' };
            var split = storedpasswordhash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);
            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            passwordcheck = SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}