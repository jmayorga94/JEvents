using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JEvents.API.Context;
using JEvents.API.Dtos;
using JEvents.API.Models;
using JEvents.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JEvents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventsController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }


        // <summary>
        /// Returns all the Events.
        /// <summary>
        /// <returns>returns events registered </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/events/
        ///
        /// </remarks>
        [Authorize]
        [HttpGet("", Name = nameof(GetEvents))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public ActionResult<IList<EventReadDto>> GetEvents()
        {
            var eventItems = _eventRepository.GetEvents().ToList();

            return Ok(_mapper.Map<IEnumerable<EventReadDto>>(eventItems));
        }

        [HttpGet("{id}", Name = nameof(GetEventById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public ActionResult<EventReadDto> GetEventById(Guid id)
        {
            var eventItem = _eventRepository.GetEventById(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EventReadDto>(eventItem));

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EventReadDto> CreateEvent(EventCreateDto dto)
        {
            var eventModel = _mapper.Map<Event>(dto);
            _eventRepository.CreateEvent(eventModel);
            _eventRepository.SaveChanges();

            var eventReadDto = _mapper.Map<EventReadDto>(eventModel);

            return CreatedAtRoute(nameof(GetEventById), new { Id = eventModel.Id }, eventReadDto);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateEvent(Guid id, EventUpdateDto dto)
        {
            var eventModelFromRepo = _eventRepository.GetEventById(id);

            if (eventModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, eventModelFromRepo);

            _eventRepository.UpdateEvent(eventModelFromRepo);

            _eventRepository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PartialCommandUpdate(Guid id, JsonPatchDocument<EventUpdateDto> patchDto)
        {
            var eventFromRepo = _eventRepository.GetEventById(id);
            if (eventFromRepo == null)
            {
                return NotFound();
            }

            var eventToPatch = _mapper.Map<EventUpdateDto>(eventFromRepo);
            patchDto.ApplyTo(eventToPatch, ModelState);

            if (!TryValidateModel(eventToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(eventToPatch, eventFromRepo);

            _eventRepository.UpdateEvent(eventFromRepo);

            _eventRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteEvent(Guid id)
        {
            var eventFromRepo = _eventRepository.GetEventById(id);
            if (eventFromRepo == null)
            {
                return NotFound();
            }

            _eventRepository.DeleteEvent(eventFromRepo);
            _eventRepository.SaveChanges();

            return NoContent();

        }
    }
    
}