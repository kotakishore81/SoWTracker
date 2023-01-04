using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.Utility.OtherUtilities.OtherUtilitiesInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DBContext
{
        [ExcludeFromCodeCoverage]
        public partial class SoWDbContext : DbContext
        {
            /// <summary>
            /// Declare Connection String
            /// </summary>
            private ConnectionStrings _connectionString { get; set; }
            private IUtilityFunctions _utilityFunctions = null;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor of IDCContext to inject the Connection String.
        /// </summary>
        /// <param name="connectionString"></param>

        public SoWDbContext(ConnectionStrings connectionString, IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = connectionString;
            _connectionString.SoWConnectionString = connectionString.SoWConnectionString;
            if (_configuration["databaseEnv"] != "local")
            {
                var conn = (Microsoft.Data.SqlClient.SqlConnection)Database.GetDbConnection();
                var opt = new DefaultAzureCredentialOptions() { ExcludeSharedTokenCacheCredential = true };
                var credential = new DefaultAzureCredential(opt);
                var token = credential
                        .GetToken(new Azure.Core.TokenRequestContext(
                            new[] { "https://database.windows.net/.default" }));
                conn.AccessToken = token.Token;
            }
        }

        private string DecryptPassword(string sConstring)
            {
                string[] sFrstSplits = sConstring.Split(';');
                string sModConnStr = string.Empty;

                foreach (string strCon in sFrstSplits)
                {

                    if (strCon.StartsWith("Password"))
                    {
                        string strEncPassword = strCon.Substring(strCon.IndexOf('=') + 1, strCon.Length - strCon.IndexOf('=') - 1);
                        //string strDecPassword = _utilityFunctions.Decrypt(strEncPassword);

                       // sModConnStr = sModConnStr + strCon.Replace(strEncPassword, strDecPassword) + ';';
                    }
                    else
                    {
                        if (strCon.Length > 0)
                            sModConnStr = sModConnStr + strCon + ';';
                    }
                }

                return sModConnStr;

            }

            /// <summary>
            /// Setting Connection String.
            /// </summary>
            /// <param name="optionsBuilder"></param>
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {

                    optionsBuilder.UseSqlServer(_connectionString.SoWConnectionString,
                         opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
                }
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            }
        }
    
}
