using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class RoomValidator : IWebValidator<Room>
    {
        private readonly IRoomService _roomService;

        public RoomValidator(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public WebValidatorResult Validate(Room entity)
        {
            WebValidatorResult result = new WebValidatorResult();
            if (entity == null)
            {
                result.Append("Entity is null!");
                return result;
            }

            if (string.IsNullOrWhiteSpace(entity.Name))
                result.Append("Name is not completed!");

            if (string.IsNullOrWhiteSpace(entity.Location))
                result.Append("Locations is not completed!");

            var unique = _roomService.IsUnique(entity);
            if (!unique)
                result.Append("Room name has to be unique!");

            return result;
        }
    }
}