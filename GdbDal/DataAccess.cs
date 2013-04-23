using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Wsdot.Geodatabase
{
	public class DataAccess
	{
		public static List<string> GetDatabaseNames(DbConnection connection)
		{
			var initConnectionState = connection.State;
			string originalDatabase = connection.Database;

			List<string> output = null;

			try
			{
				// Open the connection.
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				if (string.Compare(connection.Database, "master", true) != 0)
				{
					Trace.WriteLine(string.Format("Changing database from {0} to master...", connection.Database));
					connection.ChangeDatabase("master");
				}

				var cmd = connection.CreateCommand();
				cmd.CommandText = @"SELECT [name]
  FROM [master].[sys].[databases]
  WHERE [owner_sid] <> 0x01
  ORDER BY [name]";

				output = new List<string>();

				using (var reader = cmd.ExecuteReader(CommandBehavior.Default))
				{
					while (reader.Read())
					{
						string s = reader.GetString(0);
						output.Add(s);
					}
				}

			}
			finally
			{
				if (connection != null && initConnectionState != ConnectionState.Open)
				{
					connection.Close();
				}
			}

			return output;
		}
	}
}