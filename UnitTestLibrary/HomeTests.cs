using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SeleniumTests;


namespace UnitTestLibrary
{
    public class HomeTests
    {
        public HomeSeleniumTests Tests;

        public HomeTests()
        {
            Tests = new HomeSeleniumTests();
        }
    }
}
