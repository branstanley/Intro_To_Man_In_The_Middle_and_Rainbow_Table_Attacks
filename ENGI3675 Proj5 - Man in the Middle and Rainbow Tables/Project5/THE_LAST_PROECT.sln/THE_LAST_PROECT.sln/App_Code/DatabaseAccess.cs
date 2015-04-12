// <copyright file="DatabaseAccess.cs" company="engi">
// The Database Accesssor class
// </copyright>
namespace THE_LAST_PROECT.DataAccess
{
	using System;
	using System.Collections.Generic;
    using System.Data;
	using System.Linq;
	using System.Web;
	using Npgsql;
	using NpgsqlTypes;

	/// <summary>
	/// This class is used to access the database
	/// </summary>
	public static class DatabaseAccess
	{
        /// <summary>
        /// This method is used to dump the contents of the database.  This is used in our RainbowTable class to simulate the database has been hacked, and we have all the resources.
        /// </summary>
        /// <returns>All entries in the database</returns>
        public static List<KeyValuePair<string, string>> dumpAllUsersAndPasswords()
        {
            using (NpgsqlConnection connection = DatabaseConnection())
            using (NpgsqlCommand command = new NpgsqlCommand("select username, pass from authen;", connection))
            {
                connection.Open();

                using (NpgsqlDataReader results = command.ExecuteReader())
                {
                    List<KeyValuePair<string, string>> output = new List<KeyValuePair<string, string>>();
                    foreach (IDataRecord t in results)
                    {
                        output.Add(new KeyValuePair<string, string>((string)t["username"],(string)t["pass"]));
                    }
                    return output;
                }
            }
        }

		/// <summary>
		/// This method checks if a user has provided a valid username and password
		/// </summary>
		/// <param name="userName">The user's name</param>
		/// <param name="password">The user's hashed password</param>
		/// <returns>True if valid information was provided, false otherwise</returns>
		public static Boolean authenticate_user(string userName, string password)
		{
			using (NpgsqlConnection connection = DatabaseConnection())
			using (NpgsqlCommand command = new NpgsqlCommand("select username, pass from authen where username = :pusername AND pass = :ppassword;", connection))
			{
				connection.Open();

				command.Parameters.Add(new NpgsqlParameter("pusername", NpgsqlDbType.Text) { Value = userName });
				command.Parameters.Add(new NpgsqlParameter("ppassword", NpgsqlDbType.Text) { Value = password });

                using (NpgsqlDataReader results = command.ExecuteReader())
				{
					return results.HasRows;
				}
			}
		}

		/// <summary>
		/// This is the method that provides a connection to Postgres Database.
		/// </summary>
		/// <returns>A connection to the database.</returns>
		private static NpgsqlConnection DatabaseConnection()
		{
			NpgsqlConnectionStringBuilder myBuilder = new NpgsqlConnectionStringBuilder();
			myBuilder.Host = "127.0.0.1";
			myBuilder.Port = 5432;
			myBuilder.Database = "Project5";
			myBuilder.IntegratedSecurity = true;
			return new NpgsqlConnection(myBuilder);
		}
	}
}