using Licenta.DataLayer.SqlDb.Implementation;
using Licenta.DataLayer.SqlDb.Interfaces;
using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Implementation
{
    public class CourseService : Service<Course, ICourseRepository>, ICourseService
    {
        public CourseService()
        {
            base.repository = new CourseRepository();
        }

        public List<Course> GetFilteredCourses(CourseFilter filter)
        {
            List<Course> result = new List<Course>();

            result = GetAll().Where(c => c.Name.ToLower().Contains(filter.Name)).ToList();

            if (filter.Credits.HasValue)
                result = result.Where(c => c.Credits == filter.Credits.Value).ToList();

            if (filter.IsOptional.HasValue)
                result = result.Where(c => c.IsOptional == filter.IsOptional.Value).ToList();

            if (filter.Year.HasValue)
                result = result.Where(c => c.Year == filter.Year.Value).ToList();

            return result;
        }
    }
}
