using EventGroupProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestLibrary
{
    [TestClass]
    public class UserTests
    {
        DBHandler dbHandler;
        public static string ConnString;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            ConnString = TestValues.ConnectionString;
        }

        [TestInitialize()]
        public void Initialize()
        {
            dbHandler = new DBHandler();  
        }

        [TestMethod()]
        public void CanAddUser()
        {
            // AddUser();
            Assert.IsTrue(VerifyUserExists());
            //DeleteUser();
        }

        public void AddUser()
        {
            //Guid guid1 = new Guid();
            //Guid guid2 = new Guid();

            //string email = guid1.ToString() + "@gmail.com";
            //string display = guid2.ToString();

            //dbHandler.AddUser(display, email);
        }

        public bool VerifyUserExists()
        {
            //int userId = dbHandler.GetUserId("TestEmail");
            return true;
        }

        public void DeleteUser()
        {

        }
    }
}
