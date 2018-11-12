using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class EventCreationSeleniumTests : MasterSeleniumTest
    {
        public EventCreationSeleniumTests()
        {
            URL = "http://www.event-bull.com/EventCreation/";
        }

        public void GoToEventPage()
        {
            GoHome();
            Login();
            Driver.Navigate().GoToUrl(URL);
        }

        public bool CanGoToEventPage()
        {
            GoToEventPage();
            return (Driver.Url == URL);
        }

        public bool CanCreateEvent()
        {
            GoToEventPage();
            

            GetElementWait("event-name-input").SendKeys("TestEventName");
            GetElementWait("city-input").SendKeys("TestEventCity");
            GetElementWait("next-btn").Click();

            GetElementWaitByXPath("//*[@id='tag-btn-container']/button[1]").Click();
            GetElementWait("next-btn").Click();

            GetElementWait("date-input").SendKeys("12/31/2018");
            GetElementWait("time-input").SendKeys("12:00AM");
            GetElementWait("duration-hour-input").SendKeys("4");
            GetElementWait("location-input").SendKeys("Location");
            GetElementWait("next-btn").Click();

            GetElementWait("save-btn").Click();

            System.Threading.Thread.Sleep(500);
            return (Driver.Url == "http://www.event-bull.com/Home/Index");
        }

        public bool EventCreationFailsNoName()
        {
            GoToEventPage();
            GetElementWait("next-btn").Click();

            return (GetElementWait("error-list") != null);
        }
    }
}
