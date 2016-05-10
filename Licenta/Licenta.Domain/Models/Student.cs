using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Domain.Models
{
    [NPoco.TableName("Student")]
    [NPoco.PrimaryKey("Id")]
    public class Student : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarPath { get; set; }
        public int GroupId { get; set; }
        public string Username { get; set; }

        [NPoco.Ignore]
        public Group Group { get; set; }

        public static string GeneratePassword()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GenerateUsername(string firstName, string lastName)
        {
            firstName = firstName != null ? firstName : string.Empty;
            lastName = lastName != null ? lastName : string.Empty;
            return string.Format("{0}.{1}", firstName.ToLower(), lastName.ToLower());
        }
    }
}
