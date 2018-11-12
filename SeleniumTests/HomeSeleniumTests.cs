using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class HomeSeleniumTests : MasterTest
    {
        public HomeSeleniumTests()
        {
            URL = "http://www.event-bull.com";
        }

        public void GoToHome()
        {
            _login.Login();
            Driver.Navigate().GoToUrl(URL);
        }

        public bool HomeIsReachAble()
        {
            GoToHome();
            return (URL == Driver.Url);
        }

        
    }
}
