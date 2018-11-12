using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class HomeSeleniumTests
    {
        private IWebDriver Driver;
        private string URL;
        private string LoginEmail = "ebtest@gmail.com";
        private string PW = "ebTest1!";

        public HomeSeleniumTests()
        {
            Driver = new ChromeDriver(".");
            URL = "http://www.event-bull.com";
        }

        public bool Login()
        {
            Driver.FindElement(By.Id("email-input")).SendKeys(LoginEmail);
            Driver.FindElement(By.Id("password-input")).SendKeys(PW);
            return true;
        }

        public void GoToHome()
        {
            Driver.Navigate().GoToUrl(URL);
        }

        public bool HomeIsReachAble()
        {
            GoToHome();
            return (URL == Driver.Url);
        }

        
    }
}
