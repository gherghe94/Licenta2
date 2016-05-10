using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    [NPoco.PrimaryKey("Id")]
    [NPoco.TableName("TeacherCourse")]
    public class TeacherCourse : BaseModel
    {
        public int TeacherId { get; set; }

        public int CourseId { get; set; }
    }
}
