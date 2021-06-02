using AutoMapper;
using JEvents.API.Controllers;
using JEvents.API.Models;
using JEvents.API.Profiles;
using JEvents.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JEventsAPI.Tests
{
    
    public class EventControllerTests
    {
        Mock<IEventRepository> mockRepo;
        EventsProfile profile;
        MapperConfiguration config;
        IMapper _mapper;
        public EventControllerTests()
        {
            mockRepo = new Mock<IEventRepository>();
           


            profile = new EventsProfile();
            config  = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            _mapper = new Mapper(config);

        }

        [Fact]
        public void GetEventItems_ReturnsZeroItems_WhenDbIsEmpty()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetEvents()).Returns(GetEvents(null));
           
            var controller = new EventsController(mockRepo.Object,_mapper);

            //Act

            var result = controller.GetEvents();

            //Assert

            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<Event> GetEvents(Guid? guid)
        {
            var events = new List<Event>();
            if(guid != null)
            {
                events.Add(new Event()
                {
                    Description = "hello test",
                    Id = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    Duration = 1

                });
            }

            return events;
        }

       [Fact]
       public void GetEvents_ReturnsOneItem_WhenRequestedById()
        {
            mockRepo.Setup(repo => repo.GetEventById(Guid.Parse("e12143b3-5e41-4688-a1a3-0bdb107cbeb7"))).Returns(GetEventById(Guid.Parse("e12143b3-5e41-4688-a1a3-0bdb107cbeb7")));

            var controller = new EventsController(mockRepo.Object, _mapper);

            var result = controller.GetEventById(Guid.Parse("e12143b3-5e41-4688-a1a3-0bdb107cbeb7"));


            Assert.IsType<OkResult>(result.Result);
        }

        private Event GetEventById(Guid id)
        {
            var evnt = new Event()
            {
                Id = Guid.Parse("e12143b3-5e41-4688-a1a3-0bdb107cbeb7"),
                Description = "Test event",
                StartDate = DateTime.Now,
                Duration = 1
            };

            return evnt;
        }
    }
}
