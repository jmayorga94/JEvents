using AutoMapper;
using JEvents.API.Dtos;
using JEvents.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JEvents.API.Profiles
{
    public class EventsProfile : Profile
    {
        public EventsProfile()
        {
            CreateMap<Event, EventReadDto>();
            CreateMap<EventCreateDto, Event>();
            CreateMap<EventUpdateDto, Event>();
            CreateMap<Event, EventUpdateDto>();

        }
    }
}
