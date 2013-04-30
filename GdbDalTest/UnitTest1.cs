using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wsdot.Geodatabase;
using System.Data.SqlClient;
using System.Collections.Generic;
using GdbDalTest.Properties;
using System.Data;
using System.Linq;

namespace GdbDalTest
{
	[TestClass]
	public class UnitTest1
	{
		public IDbConnection Connection { get; private set; }

		private TestContext testContext;

		public TestContext TestContext
		{
			get { return testContext; }
			set { testContext = value; }
		}


		[TestInitialize]
		public void TestInit()
		{
			string connStr = Settings.Default.GdbConnectionString;
			Assert.IsFalse(string.IsNullOrWhiteSpace(connStr), "Connection string should not be null, empty or whitespace.");
			this.Connection = new SqlConnection(connStr); 
		}

		[TestCleanup]
		public void TestCleanup()
		{
			if (this.Connection != null)
			{
				this.Connection.Close();
			}
		}

		[TestMethod]
		public void TestDatabaseList()
		{
			var names = DataAccess.GetDatabaseNames(this.Connection);
			Assert.IsNotNull(names);
			Assert.IsTrue(names.Count > 0);
		}

		[TestMethod]
		public void TestGdbItemList()
		{
			List<GeodatabaseItemInfo> gdbItems;
			List<string> names;

			try
			{
				this.Connection.Open();
				names = DataAccess.GetDatabaseNames(this.Connection);
				Assert.IsNotNull(names, "List of database names is null.");
				Assert.IsTrue(names.Count > 0, "List of database names is empty.");
				string dbName = names.First();
				this.Connection.ChangeDatabase(dbName);
				gdbItems = DataAccess.GetGeodatabaseInfo(this.Connection);
			}
			finally
			{
				if (this.Connection != null)
				{
					this.Connection.Close();
				}
			}

		}

		[TestMethod]
		public void TestGetXml()
		{
			List<GeodatabaseItemInfo> gdbItems;
			List<string> names;

			try
			{
				this.Connection.Open();
				names = DataAccess.GetDatabaseNames(this.Connection);
				Assert.IsNotNull(names, "List of database names is null.");
				Assert.IsTrue(names.Count > 0, "List of database names is empty.");
				string dbName = names.First();
				this.Connection.ChangeDatabase(dbName);
				gdbItems = DataAccess.GetGeodatabaseInfo(this.Connection);
				var fc = gdbItems.First(i => i.Type.Name == "Feature Class");
				// Get the XML metadata.
				object xml = DataAccess.GetMetadataXmlByName(Connection, fc.Name);
				////object xml = DataAccess.GetMetadataXmlByName(this.Connection, "LRS.DBO.GPSLRSStatewide");
				Assert.IsNotNull(xml, "XML should not be null.");
				Assert.IsInstanceOfType(xml, typeof(string), "XML should be a string, not {0}", xml.GetType());
			}
			finally
			{
				if (this.Connection != null)
				{
					this.Connection.Close();
				}
			}
		}
	}
}
