using Licenta.Domain.FilterDtos;
using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces
{
    public interface ICourseService : IService<Course>
    {
        List<Course> GetFilteredCourses(CourseFilter filter);
    }
}
