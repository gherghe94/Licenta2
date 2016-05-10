using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    [NPoco.TableName("Group")]
    [NPoco.PrimaryKey("Id")]
    public class Group : BaseModel
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Specialization { get; set; }

        [NPoco.Ignore]
        public List<Student> Students { get; set; }
    }
}
