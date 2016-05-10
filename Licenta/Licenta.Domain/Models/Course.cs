using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    [NPoco.TableName("Course")]
    [NPoco.PrimaryKey("Id")]
    public class Course : BaseModel
    {
        public string Name { get; set; }

        public int Credits { get; set; }

        public bool IsOptional { get; set; }

        public int Year { get; set; }
    }
}
