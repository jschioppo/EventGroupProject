using System;
using Xunit;
using SeleniumTests;

namespace TestLibrary
{
    public class HomeTests : IDisposable
    {
        public HomeSeleniumTests _tests;

        public HomeTests()
        {
            _tests = new HomeSeleniumTests();
        }

        [Fact]
        public void NavigateToHome()
        {
            Assert.True(_tests.HomeIsReachAble());
        }

        public void Dispose()
        {
            _tests.Dispose();
        }
    }
}
