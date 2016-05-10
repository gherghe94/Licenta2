using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.FilterDtos
{
    public class GroupFilter
    {
        public string Name { get; set; }

        public int? Year { get; set; }

        public string Specialization { get; set; }

        public void ToLower()
        {
            Name = Name ?? string.Empty;
            Specialization = Specialization ?? string.Empty;

            Name = Name.ToLower();
            Specialization = Specialization.ToLower();
        }
    }
}
