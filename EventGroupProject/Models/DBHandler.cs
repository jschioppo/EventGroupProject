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
    public class DBHandler
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

        //TODO: Am I using this? Check unit tests
        public DBHandler()
        {

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
            return (i >= 1);
        }

        public string GetDisplayName(int userId)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("GetUserDisplayName", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);

            Con.Open();
            string displayName = (string)cmd.ExecuteScalar();
            Con.Close();

            return displayName;
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

            return (i == 1);
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

            return (i == 1);
        }

        public bool AddTagToEvent(int eventId, int tagId)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("AddTagToEvent", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EventId", eventId);
            cmd.Parameters.AddWithValue("@TagId", tagId);

            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();

            return (i == 1);
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

        public int GetUserId(string email)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("GetUserID", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmailAddress", email);

            Con.Open();
            var userId = cmd.ExecuteScalar();
            Con.Close();
            return (userId != null) ? (int)userId : 0;
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

        public void AddCityToUser(string city)
        {
            int userId = GetUserId();

            StartConnection();
            SqlCommand cmd = new SqlCommand("AddCityToUser", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@City", city);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public bool IsUserRegisteredToEvent(int UserID, int EventID)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("CheckIfRegistered", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", UserID);
            cmd.Parameters.AddWithValue("@EventId", EventID);

            Con.Open();
            int i = (int)cmd.ExecuteScalar();
            Con.Close();

            return (i == 1);

        }
      
        public int AddEvent(string name, string city, string desc, DateTime dateTime, int Duration, string location, int price, int creatorId)
        {
            StartConnection();

            SqlCommand cmd = new SqlCommand("AddEvent", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if(desc == null)
            {
                desc = "";
            }

            string sqlFormattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            cmd.Parameters.AddWithValue("@EventName", name);
            cmd.Parameters.AddWithValue("@EventCity", city);
            cmd.Parameters.AddWithValue("@EventTime", sqlFormattedDate);
            cmd.Parameters.AddWithValue("@EventDuration", Duration);
            cmd.Parameters.AddWithValue("@EventLocation", location);
            cmd.Parameters.AddWithValue("@EventPrice", price);
            cmd.Parameters.AddWithValue("@EventCreatorID", creatorId);
            cmd.Parameters.AddWithValue("@EventDescription", desc);

            Con.Open();
            int createdEventId = (int)cmd.ExecuteScalar();
            Con.Close();

            return createdEventId;
        }

        public Events GetEvent(int eventId)
        {
            List<Tag> eventTags = GetEventTags(GetEventTagIds(eventId));
            List<User> signedUpUsers = GetSignedUpUsers(eventId);
            //List<Comments> eventComments = GetComments(eventId);
            Events newEvent = null;

            StartConnection();

            SqlCommand cmd = new SqlCommand("GetEvent", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EventId", eventId);

            Con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                newEvent = new Events()
                {
                    EventId = int.Parse(reader["EventId"].ToString()),
                    EventName = reader["EventName"].ToString(),
                    City = reader["City"].ToString(),
                    Description = reader["Description"].ToString(),
                    Duration = int.Parse(reader["Duration"].ToString()),
                    Location = reader["Location"].ToString(),
                    StartTime = reader.GetDateTime(4),
                    Price = int.Parse(reader["Price"].ToString()),
                    EventCreator = new User()
                    {
                        UserId = int.Parse(reader["EventCreatorID"].ToString()),
                        UserDisplayName = GetDisplayName(int.Parse(reader["EventCreatorID"].ToString()))
                    },
                    SignedUpUsers = signedUpUsers,
                    EventTags = eventTags
                };
            }

            Con.Close();

            return newEvent;
        }

        public bool IsAdmin(int userId)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("checkIfAdmin", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);

            Con.Open();
            int i = (int)cmd.ExecuteScalar();
            Con.Close();

            return (i == 1);
        }

        public bool UserIsBanned()
        {
            int userId = GetUserId();

            StartConnection();
            SqlCommand cmd = new SqlCommand("CheckUserBanned", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);

            Con.Open();
            bool banned = (bool)cmd.ExecuteScalar();
            Con.Close();

            return banned;
        }

        public bool IsEventCreator(int userId, int eventId)
        {
            StartConnection();
            SqlCommand cmd = new SqlCommand("CheckIfEventCreator", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@EventId", eventId);

            Con.Open();
            int i = (int)cmd.ExecuteScalar();
            Con.Close();

            return (i == 1);
        }

        List<int> GetEventTagIds(int eventId)
        {
            StartConnection();

            SqlCommand cmd = new SqlCommand("GetEventTagIds", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EventId", eventId);
            Con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            List<int> tagIds = new List<int>();

            while (reader.Read())
            {
                tagIds.Add(int.Parse(reader["TagID"].ToString()));
            }

            Con.Close();

            return tagIds;
        }

        List<Tag> GetEventTags(List<int> eventTagIds)
        {
            SqlCommand cmd = new SqlCommand("GetTag", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            List<Tag> eventTags = new List<Tag>();

            Con.Open();

            foreach (int tagId in eventTagIds)
            {
                cmd.Parameters.AddWithValue("@TagId", tagId);
                eventTags.Add(new Tag()
                {
                    Name = (string)cmd.ExecuteScalar()
                });
                cmd.Parameters.Clear();
            }

            Con.Close();

            return eventTags;
        }

        List<User> GetSignedUpUsers(int eventId)
        {
            List<int> userIds = new List<int>();
            List<User> users = new List<User>();

            SqlCommand cmd = new SqlCommand("GetEventUsers", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EventId", eventId);
            Con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                userIds.Add(int.Parse(reader["UserId"].ToString()));
            }

            Con.Close();

            foreach(int userID in userIds)
            {
                users.Add(new User()
                {
                    UserId = userID,
                    UserDisplayName = GetDisplayName(userID)
                });
            }

            return users;
        }

        public List<Comments> GetComments(int event_ID)
        {
            StartConnection();

            List<Comments> comment_list= new List<Comments>();


            SqlCommand cmd = new SqlCommand("GetEventComments", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EventId", event_ID);
            Con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                comment_list.Add(new Comments
                {
                    EventId = event_ID,
                    CommentId = int.Parse(reader["CommentID"].ToString()),
                    UserId = int.Parse(reader["UserID"].ToString()),
                    Content = reader["Content"].ToString(),
                    UserDisplayName = reader["UserDisplayName"].ToString()
                });
            }

            Con.Close();

            return comment_list;

        }

        public bool AddComment(string user_comment, int event_ID) {

            StartConnection();

            int user_id = GetUserId();
            string displayName = GetDisplayName(user_id);

            SqlCommand cmd = new SqlCommand("AddComment", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserComment", user_comment);
            cmd.Parameters.AddWithValue("@EventId", event_ID);
            cmd.Parameters.AddWithValue("@UserId", user_id);
            cmd.Parameters.AddWithValue("@UserDisplayName", displayName);

            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();

            /*Returns if Query was successful or not*/
            return (i == 1);
        }

        public List<Events> SearchEvents(List<int> tagIds, string city)
        {
            StartConnection();

            List<Events> events = new List<Events>();
            List<int> eventIds = new List<int>();

            SqlCommand cmd = new SqlCommand("GetEventFromCityAndTag", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            Con.Open();
            foreach(var tagId in tagIds)
            {
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@TagId", tagId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int eventId = int.Parse(reader["EventId"].ToString());

                    if (!eventIds.Contains(eventId))
                    {
                        eventIds.Add(eventId);

                        events.Add(new Events()
                        {
                            EventId = eventId,
                            EventName = reader["EventName"].ToString(),
                            StartTime = reader.GetDateTime(1),
                            Duration = int.Parse(reader["Duration"].ToString()),
                            Location = reader["Location"].ToString(),
                            Price = int.Parse(reader["Price"].ToString())
                        });
                    }
                }

                cmd.Parameters.Clear();
                reader.Close();

            }
            return events;
        }

        //Using above method instead of this one
        public List<int> GetEventIdsFromTag(List<int> tagIds)
        {
            StartConnection();
            List<int> eventIds = new List<int>();

            SqlCommand cmd = new SqlCommand("GetEventsFromTag", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            Con.Open();
            foreach(int tagId in tagIds)
            {
                cmd.Parameters.AddWithValue("@tagId", tagId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int eventId = int.Parse(reader["EventId"].ToString());
                    if (!eventIds.Contains(eventId))
                    {
                        eventIds.Add(eventId);
                    }
                }
                cmd.Parameters.Clear();
                reader.Close();
            }
            Con.Close();

            return eventIds;
        }

        public bool AddUserToEvent(int event_id)
        {
            StartConnection();

            int user_id = GetUserId();

            SqlCommand cmd = new SqlCommand("AddUserToEvent", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@event_ID", event_id);
            cmd.Parameters.AddWithValue("@user_ID", user_id);

            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();

            /*Returns if Query was successful or not*/
            return (i == 1);

        }

        public List<Events> GetUserEvents()
        {
            List<Events> events = new List<Events>();
            int userId = GetUserId();

            StartConnection();

            SqlCommand cmd = new SqlCommand("GetUserEvents", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);

            Con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) {
                events.Add(new Events()
                {
                    EventId = int.Parse(reader["EventId"].ToString()),
                    EventName = reader["EventName"].ToString(),
                    City = reader["City"].ToString(),
                    StartTime = reader.GetDateTime(3),
                    Duration = int.Parse(reader["Duration"].ToString()),
                    Location = reader["Location"].ToString(),
                    Price = int.Parse(reader["Price"].ToString())
                });
            }

            return events;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            StartConnection();

            SqlCommand cmd = new SqlCommand("GetAllUserNames", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            Con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User()
                {
                    UserId = int.Parse(reader["UserId"].ToString()),
                    UserDisplayName = reader["DisplayName"].ToString()
                });
            }

            return users;
        }

        public void BanUser(int userId)
        {
            StartConnection();

            SqlCommand cmd = new SqlCommand("BanUser", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void MakeUserAdmin(int userId)
        {
            StartConnection();

            SqlCommand cmd = new SqlCommand("MakeUserAdmin", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void RemoveUserFromEvent(int userId, int eventId)
        {
            StartConnection();

            SqlCommand cmd = new SqlCommand("LeaveEvent", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@EventId", eventId);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void DeleteComment(int commentId)
        {
            StartConnection();

            SqlCommand cmd = new SqlCommand("DeleteComment", Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CommentId", commentId);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }
    }
}
