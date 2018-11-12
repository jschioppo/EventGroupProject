using EventGroupProject.Controllers;
using EventGroupProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace UnitTestLibrary
{
    public class EventCreationTests
    {
        DBHandler _dbHandler;
        EventCreationController _controller;

        public EventCreationTests()
        {
            _controller = new EventCreationController(_dbHandler);
        }

        [Fact]
        public void IndexViewTest()
        {
            
        }
    }
}
