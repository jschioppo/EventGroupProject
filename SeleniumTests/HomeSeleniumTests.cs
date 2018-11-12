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
    public class HomeSeleniumTests : MasterSeleniumTest
    {
        public HomeSeleniumTests()
        {
            URL = "http://www.event-bull.com/";
            GoToHome();
        }

        public void GoToHome()
        {
            Login();
            Driver.Navigate().GoToUrl(URL);
        }

        public bool HomeIsReachAble()
        {
            return (URL == Driver.Url);
        }

        
    }
}
