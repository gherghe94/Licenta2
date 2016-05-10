using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using Licenta.Models.DTO.WebValidators;
using Licenta.Services.Implementation;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Licenta.Controllers
{
    [Authorize]
    public class RoomController : ApiController
    {
        private readonly IRoomService _roomService;

        public RoomController()
        {
            _roomService = new RoomService();
        }

        [HttpGet]
        public IHttpActionResult GetAll([FromUri] string location, string name)
        {
            location = location ?? string.Empty;
            name = name ?? string.Empty;

            RoomFilter filter = new RoomFilter() { Location = location.ToLower(), Name = name.ToLower() };

            var allRooms = _roomService.GetAll();
            var filteredRooms = allRooms.Where(room => room.Name.ToLower().Contains(filter.Name) && room.Location.ToLower().Contains(filter.Location)).ToList();

            return Ok(filteredRooms);
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] Room entity)
        {
            try
            {
                IWebValidator<Room> validator = new RoomValidator(_roomService);
                WebValidatorResult webValidationResult = validator.Validate(entity);
                if (webValidationResult.IsOk)
                    _roomService.Save(entity);

                return Ok(webValidationResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult Edit([FromBody] Room entity)
        {
            return Add(entity);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] Room entity)
        {
            try
            {
                var result = _roomService.Delete(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
