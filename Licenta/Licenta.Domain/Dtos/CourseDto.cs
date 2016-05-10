using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Licenta.Domain.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Credits { get; set; }

        public bool IsOptional { get; set; }

        public int Year { get; set; }

        public bool IsChecked { get; set; }
    }
}
