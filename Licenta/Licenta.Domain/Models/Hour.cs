using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    [NPoco.TableName("Hour")]
    [NPoco.PrimaryKey("Id")]
    public class Hour : BaseModel
    {
        public int GroupId { get; set; }
        [NPoco.Ignore]
        public Group Group { get; set; }

        public int CourseId { get; set; }
        [NPoco.Ignore]
        public Course Course { get; set; }

        public int RoomId { get; set; }
        [NPoco.Ignore]
        public Room Room { get; set; }

        public int TeacherId { get; set; }
        [NPoco.Ignore]
        public Teacher Teacher { get; set; }

        public string TheDay { get; set; }

        public string TheHour { get; set; }

        public string WhichWeek { get; set; }

        public string TypeOfHour { get; set; }
    }
}
