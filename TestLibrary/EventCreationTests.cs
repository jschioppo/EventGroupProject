using System;
using System.Collections.Generic;
using System.Text;
using SeleniumTests;
using Xunit;

namespace TestLibrary
{
    public class EventCreationTests : IDisposable
    {
        public EventCreationSeleniumTests _tests;

        public EventCreationTests()
        {
            _tests = new EventCreationSeleniumTests();
        }

        [Fact]
        public void CanGoToEventPage()
        {
            Assert.True(_tests.CanGoToEventPage());
        }

        [Fact]
        public void CanCreateEvent()
        {
            Assert.True(_tests.CanCreateEvent());
        }

        [Fact]
        public void CreateFailsNoName()
        {
            Assert.True(_tests.EventCreationFailsNoName());
        }

        public void Dispose()
        {
            _tests.Dispose();
        }
    }
}
