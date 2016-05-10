using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class HourValidator : IWebValidator<Hour>
    {
        #region members
        private readonly IHourService _hourService;

        private readonly List<string> _daysOfTheWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

        private readonly List<string> _intervals = new List<string> { "08:00-9:50", "10:00-11:50", "12:00-13:50", "14:00-15:50", "16:00-17:50", "18:00-19:50", "20:00-21:50" };

        private readonly List<string> _weekTypes = new List<string> { "Even", "Odd", "Both" };

        private readonly List<string> _hourTypes = new List<string> { "Lab", "Workshop", "Class" };
        #endregion members

        public HourValidator(IHourService service)
        {
            _hourService = service;
        }

        private void CompleteMandatoryError(Hour entity, WebValidatorResult webValidatorResult)
        {
            if (string.IsNullOrWhiteSpace(entity.TheDay))
                webValidatorResult.Append("The day is mandatory!");

            if (string.IsNullOrWhiteSpace(entity.TheHour))
                webValidatorResult.Append("The interval is mandatory!");

            if (string.IsNullOrWhiteSpace(entity.WhichWeek))
                webValidatorResult.Append("The week type is mandatory!");

            if (string.IsNullOrWhiteSpace(entity.TypeOfHour))
                webValidatorResult.Append("The hour type is mandatory!");
        }

        private void CompleteNotAllowedValues(Hour entity, WebValidatorResult webValidatorResult)
        {
            if (!_daysOfTheWeek.Contains(entity.TheDay))
                webValidatorResult.Append("Day is not valid!");

            if (!_intervals.Contains(entity.TheHour))
                webValidatorResult.Append("Time interval is not valid!");

            if (!_weekTypes.Contains(entity.WhichWeek))
                webValidatorResult.Append("Week type is not valid!");

            if (!_hourTypes.Contains(entity.TypeOfHour))
                webValidatorResult.Append("Hour type is not valid!");
        }

        public void ValidateSchedule(Hour entity, WebValidatorResult webValidatorResult)
        {
            var allHours = _hourService.GetAll();
            allHours.RemoveAll(q => q.Id == entity.Id);

            var duplicate = allHours.Any(h=> 
                h.TeacherId == entity.TeacherId &&
                h.CourseId == entity.CourseId &&
                h.RoomId == entity.RoomId && 
                h.GroupId == entity.GroupId &&
                h.TheDay == entity.TheDay &&
                h.TheHour == entity.TheHour &&
                h.WhichWeek == entity.WhichWeek &&
                h.TypeOfHour == entity.TypeOfHour);

            if(duplicate)
                webValidatorResult.Append("The hour that you want to introduce it's duplicated!");

            var alreadyHasCourse = allHours.Any(h =>
                h.TeacherId == entity.TeacherId &&
                h.TheDay == entity.TheDay &&
                h.TheHour == entity.TheHour &&
                h.CourseId != entity.CourseId);

            if (alreadyHasCourse)
                webValidatorResult.Append("Teacher already has other course!");

            var result = allHours.Any(h =>
                h.TeacherId == entity.TeacherId &&
                h.TheDay == entity.TheDay &&
                h.TheHour == entity.TheHour &&
                h.WhichWeek == entity.WhichWeek &&
                h.RoomId != entity.RoomId);

            if (result)
                webValidatorResult.Append("The teacher has already another course in other room!");

            var sameRoom = allHours.Any(h =>
                h.RoomId == entity.RoomId &&
                h.TheDay == entity.TheDay &&
                h.TheHour == entity.TheHour &&
                h.CourseId != entity.CourseId);

            if (sameRoom)
                webValidatorResult.Append("The room is already used for another course!");

            var sameTypeOfHour = allHours.Any(h =>
                h.TeacherId == entity.TeacherId &&
                h.TheDay == entity.TheDay &&
                h.TheHour == entity.TheHour &&
                h.WhichWeek == entity.WhichWeek &&
                h.RoomId == entity.RoomId &&
                h.TypeOfHour != entity.TypeOfHour);

            if (sameTypeOfHour)
                webValidatorResult.Append("The type of hour has to be the same with the hour that is connected");
        }

        public void NoMoreThan5(Hour entity, WebValidatorResult webValidatorResult)
        {
            var allHours = _hourService.GetAll();
            allHours = allHours.Where(e => e.GroupId == entity.GroupId && e.TheDay == entity.TheDay).ToList();

            if (allHours.Count > 4)
                webValidatorResult.Append(string.Format("The number of hours that can be in {0} has been reached!", entity.TheDay));
        }

        public WebValidatorResult Validate(Hour entity)
        {
            WebValidatorResult webValidatorResult = new WebValidatorResult();

            if (entity == null)
            {
                webValidatorResult.Append("Entity is null!");
                return webValidatorResult;
            }

            CompleteMandatoryError(entity, webValidatorResult);
            CompleteNotAllowedValues(entity, webValidatorResult);
            ValidateSchedule(entity, webValidatorResult);
            NoMoreThan5(entity, webValidatorResult);

            return webValidatorResult;
        }
    }
}