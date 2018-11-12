using System;
using System.Collections.Generic;
using System.Text;
using SeleniumTests;
using Xunit;

namespace TestLibrary
{
    public class EventSearchTests : IDisposable
    {
        EventSearchSeleniumTests _tests;

        public EventSearchTests()
        {
            _tests = new EventSearchSeleniumTests();
        }

        [Fact]
        public void CanGoToSearchPage()
        {
            Assert.True(_tests.CanGoToSearchPage());
        }

        [Fact]
        public void CanSearchEvents()
        {
            Assert.True(_tests.CanSearch());
        }

        public void Dispose()
        {
            _tests.Dispose();
        }
    }
}
