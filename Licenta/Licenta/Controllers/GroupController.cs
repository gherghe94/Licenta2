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
    public class GroupController : ApiController
    {
        private readonly IGroupService groupService;

        public GroupController()
        {
            groupService = new GroupService();
        }

        [HttpGet]
        public IHttpActionResult GetAllGroups([FromUri] GroupFilter filter)
        {
            List<Group> allGroups = groupService.GetAllFiltered(filter);
            return Ok(allGroups);
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] Group group)
        {
            try
            {
                IWebValidator<Group> validator = new GroupValidator(groupService);
                WebValidatorResult result = validator.Validate(group);
                if (result.IsOk)
                    groupService.Save(group);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] Group entity)
        {
            try
            {
                var result = groupService.Delete(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
