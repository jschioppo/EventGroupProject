using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class EventSearchSeleniumTests : MasterSeleniumTest
    {
        public EventSearchSeleniumTests()
        {
            URL = "http://www.event-bull.com/EventsRecommendedSearching";
        }

        public void GoToSearchPage()
        {
            GoHome();
            Login();
            Driver.Navigate().GoToUrl(URL);
        }

        public bool CanGoToSearchPage()
        {
            GoToSearchPage();
            return (Driver.Url == URL);
        }

        public bool CanSearch()
        {
            GoToSearchPage();
            GetElementWait("searchbox").SendKeys("Tampa");
            GetElementWaitByXPath("//*[@id='tag-search-btn']").Click();
            GetElementWait("search-btn").Click();
            System.Threading.Thread.Sleep(500);
            return (GetElementWait("result") != null || GetElementWait("no-results-window") != null);
        }
    }
}
