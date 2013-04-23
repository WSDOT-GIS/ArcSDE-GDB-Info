using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Wsdot.Geodatabase.Properties;

namespace Wsdot.Geodatabase
{
	/// <summary>
	/// Provides data about file geodatabases.
	/// </summary>
	public class DataAccess
	{
		/// <summary>
		/// Returns a list of database names.
		/// </summary>
		/// <param name="connection">A database connection to a server containing geodatabases.</param>
		/// <returns></returns>
		public static List<string> GetDatabaseNames(IDbConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			// Store the initial connection state.  If the connection is already open we will leave it open instead of closing it.
			var initConnectionState = connection.State;
			// Store the initial database in case we need to change it.
			string originalDatabase = connection.Database;

			List<string> output = null;

			try
			{
				// Open the connection if it is not already open.
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				// We need to use the "master" database.
				if (string.Compare(connection.Database, "master", true) != 0)
				{
					Trace.WriteLine(string.Format("Changing database from {0} to master...", connection.Database));
					connection.ChangeDatabase("master");
				}

				// Create the command to select the list of databases.
				var cmd = connection.CreateCommand();
				cmd.CommandText = Resources.ListDatabases;

				// Initialize the list of database names.
				output = new List<string>();

				// Execute the command and store the database names that are returned.
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
				// Close the connection ONLY if it was not already opened when we started.
				if (connection != null && initConnectionState != ConnectionState.Open)
				{
					connection.Close();
				}
			}

			return output;
		}

		public static List<GeodatabaseItemInfo> GetGeodatabaseInfo(IDbConnection connection)
		{
			if (connection == null) throw new ArgumentNullException("connection");

			var initState = connection.State;
			string initialDb = connection.Database;

			List<GeodatabaseItemInfo> output = null;

			try
			{
				if (initState != ConnectionState.Open)
				{
					connection.Open();
				}
				var command = connection.CreateCommand();
				command.CommandText = Resources.ListFeatureClassesAndFeatureDatasets;
				using (var reader = command.ExecuteReader())
				{
					output = new List<GeodatabaseItemInfo>();
					while (reader.Read())
					{
						var gdbItem = new GeodatabaseItemInfo
						{
							Name = reader.GetString(0),
							PhysicalName = reader.GetString(1),
							Path = reader.GetString(2),
							Type = new GeodatabaseType(reader.GetString(3), reader.GetGuid(4), reader.GetGuid(5))
						};
						output.Add(gdbItem);
					}
				}
			}
			finally
			{
				if (connection != null && initState != ConnectionState.Open)
				{
					connection.Close();
				}
			}

			return output;
		}
	}
}