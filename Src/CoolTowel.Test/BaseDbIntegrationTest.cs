using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoolTowel.Data;
using CoolTowel.Model;
using System.IO;
using System.Linq;
using System.Configuration;
using CoolTowel.Data.Core;
using System.Diagnostics;


namespace CoolTowel.Test
{
    public class BaseDbIntegrationTest
    {
        
        protected static void DeleteDbFile()
        {
            //set Data Directory for the connection string to use
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Empty));

            //lets delete any existing database files
            var databaseFile = Path.Combine((string)AppDomain.CurrentDomain.GetData("DataDirectory"), "CoolTowelDatabase.mdf");
            var databaseLogFile = Path.Combine((string)AppDomain.CurrentDomain.GetData("DataDirectory"), "CoolTowelDatabase_log.ldf");
            File.Delete(databaseFile);
            File.Delete(databaseLogFile);
        }

        
    }
}
