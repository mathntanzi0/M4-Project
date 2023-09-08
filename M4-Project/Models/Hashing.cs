using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;


namespace M4_Project.Models 
{
    public class Hashing 
    {
        String HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);

            var hashedPassword = hash.ComputeHash(passwordBytes);

            return BitConverter.ToString(hashedPassword).Replace("-", "").ToLower();
        }
    }
}