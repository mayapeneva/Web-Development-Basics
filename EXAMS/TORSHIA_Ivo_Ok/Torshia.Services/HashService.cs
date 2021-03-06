﻿namespace Torshia.Services
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Contracts;

    public class HashService : IHashService

    {
        public string StrongHash(string stringToHash)
        {
            var result = stringToHash;
            for (int i = 0; i < 10; i++)
            {
                result = this.Hash(result);
            }

            return result;
        }

        public string Hash(string stringToHash)
        {
            stringToHash = stringToHash + "myAppSalt1316387123718#";
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                // Get the hashed string.
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}