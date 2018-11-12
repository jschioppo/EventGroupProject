using System;
using Xunit;
using SeleniumTests;

namespace TestLibrary
{
    public class HomeTests
    {
        public HomeSeleniumTests _tests;
        public LoginTest _loginTests;

        public HomeTests()
        {
            _tests = new HomeSeleniumTests();
        }

        [Fact]
        public void NavigateToHome()
        {
            Assert.True(_tests.HomeIsReachAble());
        }
    }
}
