using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.FilterDtos
{
    public class CourseFilter
    {
        public string Name { get; set; }

        public int? Credits { get; set; }

        public bool? IsOptional { get; set; }

        public int? Year { get; set; }

        public void ToLower()
        {
            Name = Name ?? string.Empty;
            Name = Name.ToLower();
        }
    }
}
