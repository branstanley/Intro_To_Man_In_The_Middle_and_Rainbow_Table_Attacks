// <copyright file="RainbowTable.cs" company="engi">
// The RainbowTable password lookup class
// </copyright>
namespace THE_LAST_PROECT.RainbowTable
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;
	using System.Web;
    using THE_LAST_PROECT.DataAccess;

	/// <summary>
    /// This program has two modes of operation:
	/// 1) Allows you to look up a hash string and find out the password stored.
    /// 2) Runs as if a Database was hacked, reading all entries in the database and spitting out those that a password was found for.
    /// 
    /// Note: 
    /// To be able to copy and paste, program must NOT be run in debugging mode.
	/// </summary>
	public static class RainbowTable
	{
		/// <summary>
		/// Stores the RainbowTable dictionary
		/// </summary>
		private static List<KeyValuePair<string, string>> rainbowTable;

		/// <summary>
		/// Initializes static members of the <see cref="RainbowTable"/> class.
		/// </summary>
		static RainbowTable()
		{
			rainbowTable = new List<KeyValuePair<string, string>>();
			string[] lines = new string[0];

			try
			{
				lines = System.IO.File.ReadAllLines(@"./RainbowFile/words.txt");
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to load file");
			}

			foreach (string t in lines)
			{
				using (MD5 md5 = MD5.Create())
				{
					string hash = GetMd5Hash(md5, t);
					rainbowTable.Add(new KeyValuePair<string, string>(hash, t));
				}
			}
		}

		/// <summary>
		/// This method is used to look up what a password is based on its MD5 Hash
		/// </summary>
		/// <param name="hashIn">A MD5 hash string</param>
		/// <returns>A password if found, or else null if the password is not found.</returns>
		public static string GetPassword(string user, string hashIn)
		{
			foreach (KeyValuePair<string, string> t in rainbowTable)
			{
				if (t.Key.Equals(hashIn))
				{
					return "Username: " + user + "\nPassword: " + t.Value + "\n";
				}
			}

			return "";
		}

        /// <summary>
        /// Pretending we've hacked the database, and have access to all users and their passwords.  Let us get their actual passwords.
        /// </summary>
        /// <returns>A long string containing all user names and their passwords, provided their Hash was convertable to its original form based on the dictionary.</returns>
        public static string getPasswordsFromDatabase()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> t in DatabaseAccess.dumpAllUsersAndPasswords())
            {
                string temp = GetPassword(t.Key, t.Value);
                if (temp != "")
                {
                    sb.Append(temp + "\n");
                }

            }
            return sb.ToString();
        }

		/// <summary>
		/// The main method.  Runs the hash lookup program.
		/// </summary>
        public static void Main()
        {
            Console.WriteLine("Select a mode of operation:");
            Console.WriteLine("\t1) Man in the Middle password parsing.");
            Console.WriteLine("\t2) Database dump password parsing.");
            int select = int.Parse(Console.ReadLine());

            switch (select)
            {
                case 1: // Man in the middle mode
                    while (true) // Sorry but ctrl + c, pressing the big X, or something similar is your only hope of escaping those mode
                    {
                        string user, inHash;
                        Console.WriteLine("What is the user name?");
                        user = Console.ReadLine();
                        Console.WriteLine("What hash do you want to check?");
                        inHash = Console.ReadLine();

                        string result = GetPassword(user, inHash);
                        if (result != "")
                        {
                            Console.WriteLine("\n" + result);
                        }
                        else
                        {
                            Console.WriteLine("\nPassword not found.\n");
                        }
                    }
                case 2: // Database dump mode
                    Console.WriteLine(getPasswordsFromDatabase()); // In real life I'd print this data to a file
                    break;
                default: // Invalid selection
                    Console.WriteLine("Invalid selection.  Program is terminating.");
                    break;
            }
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
		} // End Method

		/// <summary>
		/// This method performs the hashing for the constructor
		/// </summary>
		/// <param name="md5">Our hashing method</param>
		/// <param name="source">The string to hash</param>
		/// <returns>A Hashed string</returns>
		private static string GetMd5Hash(MD5 md5, string source)
		{
			byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < data.Length; ++i)
			{
				sb.Append(data[i].ToString("x2"));
			}

			return sb.ToString();
		}
	}
}