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
    public class MasterSeleniumTest
    {
        public string URL;
        public IWebDriver Driver;
        private string LoginEmail = "ebtest@gmail.com";
        private string PW = "ebTest1!";

        public MasterSeleniumTest()
        {
            Driver = new ChromeDriver(".");
        }

        public void Dispose()
        {
            Driver.Dispose();
        }

        public void Login()
        {
            Driver.Navigate().GoToUrl(URL);
            GetElementWait("email-input").SendKeys(LoginEmail);
            GetElementWait("password-input").SendKeys(PW);
            GetElementWait("login-button").Click();
        }

        public void GoHome()
        {
            Driver.Navigate().GoToUrl("http://www.event-bull.com/");
        }

        public IWebElement GetElementWait(string id)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            return element;
        }

        public IWebElement GetElementWaitByXPath(string xpath)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            return element;
        }
    }
}
