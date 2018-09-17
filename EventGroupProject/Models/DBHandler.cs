using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace EventGroupProject.Models
{
    public class DBHandler : Controller
    {
        private SqlConnection con { get; set; }
        public IConfiguration AppSetting { get; }
        public string userEmail;
        public IHttpContextAccessor contextAccessor;

        public DBHandler(IHttpContextAccessor _contextAccessor)
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            contextAccessor = _contextAccessor;
            userEmail = contextAccessor.HttpContext.User.Identity.Name;
        }


        private void StartConnection()
        {
            string conStr = AppSetting["ConnectionStrings:DefaultConnection"];
            con = new SqlConnection(conStr);
        }

        public bool UserTagsSelected()
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("GetUserTagsSelected", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmailAddress", userEmail);

            con.Open();
            bool tagsSelected = (bool)cmd.ExecuteScalar();
            return tagsSelected;
        }

        public bool AddUser(string displayName, string email)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("CreateNormalUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DisplayName", displayName);
            cmd.Parameters.AddWithValue("@EmailAddress", email);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            //Return to check if add succeeded
            return (i >= 1 ? true : false);
        }

        //Keep just in case we want display name to be unique.
        public bool CheckDisplayNameExists(string displayName)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("EnsureUniqueDisplayName", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DisplayName", displayName);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i == 1 ? true : false);
        }
    }
}
