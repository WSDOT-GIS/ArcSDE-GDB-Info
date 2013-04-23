using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wsdot.Geodatabase;
using System.Data.SqlClient;
using System.Collections.Generic;
using GdbDalTest.Properties;

namespace GdbDalTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestDatabaseList()
		{
			List<string> names;
			string connStr = Settings.Default.GdbConnectionString;
			Assert.IsFalse(string.IsNullOrWhiteSpace(connStr), "Connection string should not be null, empty or whitespace.");
			using (var conn = new SqlConnection(connStr)) 
			{
				names = DataAccess.GetDatabaseNames(conn);
			}

			Assert.IsNotNull(names);
			Assert.IsTrue(names.Count > 0);
		}
	}
}
