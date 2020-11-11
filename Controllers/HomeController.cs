using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;
using NPTester.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System.Threading.Tasks;
using System.Configuration;

namespace NPTester.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region DB Connect experiment right here

            //SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryInteractive, new SqlAppAuthenticationProvider());
            //string sqlConnectionString = "Server=cdctests.database.windows.net,1433;Database=cdcKafka;UID=;Authentication=Active Directory Interactive";
            //Environment.SetEnvironmentVariable("AzureServicesAuthConnectionString", $"RunAs=App; AppId={clientId}");

            string databaseName = "cdcKafka";
            string connectionResult = "Unable to Connect to Database.";
            string debugBuffer = "";

          
            string sqlConnectionString = Environment.GetEnvironmentVariable("DbConnection");

            using (var conn = new SqlConnection(sqlConnectionString))
            {
                try
                {
                    conn.Open();
                    connectionResult = "DB Connection Successful.";

                    using (var cmd = new SqlCommand("SELECT 1", conn))
                    {
                        var result = cmd.ExecuteScalar();
                        Console.WriteLine(result.ToString());

                        connectionResult = "Result:" + result;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    debugBuffer = "###StackTrace " + e.StackTrace + "###Message " + e.Message;
                    debugBuffer += "###InnerException " + e.InnerException.StackTrace + "###InnerException Message " + e.InnerException.InnerException.Message;

                    // throw;
                }

            }


           
            #endregion DB Connect experiment

            Record rcrd = new Record
            {
                DBName = databaseName,
                DBConnectionResult = connectionResult,
                DebugBuffer = debugBuffer
            };
            ViewBag.Message = rcrd;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

   
}