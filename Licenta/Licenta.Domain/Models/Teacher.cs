using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    [NPoco.TableName("Teacher")]
    [NPoco.PrimaryKey("Id")]
    public class Teacher : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        [NPoco.Ignore]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", Title, FirstName, LastName);
            }
        }

        [NPoco.Ignore]
        public List<Course> Courses { get; set; }
    }
}
