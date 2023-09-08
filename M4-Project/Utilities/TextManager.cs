using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace M4_Project.Utilities
{
    public static class TextManager
    {
        ///
        /// <summary>
        ///     Returns a string with the first letter of the passed string capitalized.
        /// </summary>
        public static string CapitalizeString(string text)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(text);
        }
        ///
        /// <summary>
        ///      Returns a string with it lenth capped at a specified size.
        /// </summary>
        public static string ShortString(string text, int shortSize)
        {
            if (shortSize > (text.Length + 3))
                return text;

            return text.Substring(0, shortSize - 3) + "...";
        }
    }

    ///
    /// <summary>
    ///     Contains functions responsible for handling password text.
    /// </summary>
    public static class PasswordManager
    {
        ///
        /// <summary>
        ///     Check if password entered by the user matches with the one on the database.
        /// </summary>
        /// <param name="password">Password to be tested</param>
        /// <param name="passwordHash">Hashed password value from a database to test the entered password</param>
        /// <returns></returns>
        public static bool PasswordMatch(string password, string passwordHash)
        {
            //
            // Ms. Moloko Masasanya Password Hashing Function needed here.
            //
            //
            return password == passwordHash;
        }
    }
}