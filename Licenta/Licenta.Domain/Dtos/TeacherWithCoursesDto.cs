using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Dtos
{
    public class TeacherWithCoursesDto
    {
        public int TeacherId { get; set; }

        public List<CourseDto> Courses { get; set; }
    }
}
