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
        private SqlConnection Con { get; set; }
        public string UserEmail { get; private set; }
        public AuthenticatedUser _authenticatedUser { get; private set; }
        public AppValues _appValues { get; private set; }

        public DBHandler(AuthenticatedUser authenticatedUser, AppValues appValues)
        {
            _appValues = appValues;
            _authenticatedUser = authenticatedUser;
            UserEmail = _authenticatedUser.Email;
        }

        private void StartConnection()
        {
            string conStr = _appValues.ConnectionString;
            Con = new SqlConnection(conStr);
        }

        public bool UserTagsSelected()
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("GetUserTagsSelected", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmailAddress", UserEmail);

            Con.Open();
            bool tagsSelected = (bool)cmd.ExecuteScalar();
            return tagsSelected;
        }

        public bool AddUser(string displayName, string email)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("CreateNormalUser", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@DisplayName", displayName);
            cmd.Parameters.AddWithValue("@EmailAddress", email);

            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();

            //Return to check if add succeeded
            return (i >= 1 ? true : false);
        }

        //Keep just in case we want display name to be unique.
        public bool CheckDisplayNameExists(string displayName)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("EnsureUniqueDisplayName", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@DisplayName", displayName);

            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();

            return (i == 1 ? true : false);
        }
    }
}
