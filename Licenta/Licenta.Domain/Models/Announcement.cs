using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    [NPoco.TableName("Announcement")]
    [NPoco.PrimaryKey("Id")]
    public class Announcement : BaseModel
    {
        public int TeacherId { get; set; }
        [NPoco.Ignore]
        public Teacher Teacher { get; set; }

        public int GroupId { get; set; }
        [NPoco.Ignore]
        public Group Group { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
