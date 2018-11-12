using System;
using Xunit;
using SeleniumTests;

namespace TestLibrary
{
    public class HomeTests
    {
        public HomeSeleniumTests Tests;

        public HomeTests()
        {
            Tests = new HomeSeleniumTests();
        }

        [Fact]
        public void NavigateToHome()
        {
            Assert.True(Tests.HomeIsReachAble());
        }
    }
}
