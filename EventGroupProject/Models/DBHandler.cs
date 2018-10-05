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

        public List<Tag> GetAllTags()
        {
            List<Tag> tags = new List<Tag>();

            StartConnection();
            SqlCommand cmd = new SqlCommand("getAllTags", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            Con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Tag newTag = new Tag()
                {
                    TagID = int.Parse(reader["TagID"].ToString()),
                    Name = reader["TagName"].ToString()
                };

                tags.Add(newTag);
            }
            Con.Close();

            return tags;
        }

        public bool AddTagToUser(int userId, int tagId)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("AddTagToUser", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@TagId", tagId);

            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();

            return (i == 1 ? true : false);
        }

        public int GetUserId()
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("GetUserID", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmailAddress", UserEmail);

            Con.Open();
            int userId = (int)cmd.ExecuteScalar();
            Con.Close();

            return userId;
        }

        public void DeleteUserTags(int userId)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("DeleteUserTags", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void SetTagSelectedToTrue(int userId)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("TagsSelectedTrue", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@userId", userId);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        /*This is mmy edit*/
        public void AddEvent(string city, string desc, string start_time, int Duration, string location, int price)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("AddEvent", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            cmd.Parameters.AddWithValue("@EventCity", city);
            cmd.Parameters.AddWithValue("@EventDescription", desc);
            cmd.Parameters.AddWithValue("@EventTime", sqlFormattedDate);
            cmd.Parameters.AddWithValue("@EventDuration", Duration);
            cmd.Parameters.AddWithValue("@EventLocation", location);
            cmd.Parameters.AddWithValue("@EventPrice", price);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

    }
}
