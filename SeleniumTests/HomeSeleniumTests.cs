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

        public HomeSeleniumTests()
        {
            Driver = new ChromeDriver();
            URL = "https://localhost:44359/Home/Index";
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
