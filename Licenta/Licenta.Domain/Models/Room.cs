using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    [NPoco.TableName("Room")]
    [NPoco.PrimaryKey("Id")]
    public class Room : BaseModel
    {
        public string Name { get; set; }

        public string Location { get; set; }
    }
}
