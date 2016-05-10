using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Licenta.Domain.FilterDtos
{
    public class StudentsFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupName { get; set; }
        public bool RetrieveAll { get; set; }
        public int Page { get; set; }
    }
}
