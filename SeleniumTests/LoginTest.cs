using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class LoginTest : MasterTest
    {
        private string LoginEmail = "ebtest@gmail.com";
        private string PW = "ebTest1!";

        public LoginTest()
        {
            URL = "http://www.event-bull.com";
        }


        public bool Login()
        {
            Driver.Navigate().GoToUrl(URL);
            GetElementWait("email-input").SendKeys(LoginEmail);
            GetElementWait("password-input").SendKeys(PW);
            GetElementWait("login-button").Click();
            
            return true;
        }
    }
}
