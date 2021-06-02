using JEvents.API.Models;
using System;
using Xunit;

namespace JEventsAPI.Tests
{
    public class EventTests : IDisposable
    {
        Event evnt;

        public EventTests()
        {
            evnt = new Event()
            {
                Description = "something",
                Id = Guid.NewGuid(),
                Duration = 1,
                StartDate = DateTime.Now

            };
        }
        [Fact]
        public void CanChangeDescription()
        {

            //Arrange
           

            //Act

            evnt.Description = "Testing change";

            // assert

            Assert.Equal("Testing change", evnt.Description);

        

        }

        public void Dispose()
        {
            evnt = null;
        }
    }
}
