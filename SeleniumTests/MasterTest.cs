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
    public class MasterTest
    {
        public string URL;
        public IWebDriver Driver;
        public LoginTest _login;

        public MasterTest()
        {
            Driver = new ChromeDriver(".");
        }

        public IWebElement GetElementWait(string id)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            return element;
        }
    }
}
