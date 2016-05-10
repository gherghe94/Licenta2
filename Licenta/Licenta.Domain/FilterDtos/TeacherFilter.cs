using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.FilterDtos
{
    public class TeacherFilter
    {
        public string FullName { get; set; }
        public string Title { get; set; }

        public void ToLower()
        {
            FullName = FullName ?? string.Empty;
            Title = Title ?? string.Empty;

            Title = Title.ToLower();
            FullName = FullName.ToLower();
        }
    }
}
