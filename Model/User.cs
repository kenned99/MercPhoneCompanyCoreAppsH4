using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string Adress { get; set; }
        public string Password { get; set; }

/*UserId (PN) | GUID
Full Name | varchar
Role | varchar
Adress | Varchar
Adress 2 | varchar
Email | varchar
Phone No. | varchar
City | varchar
Post Code | varchar
Birthday | Date
Password | varchar
Picture | BLOB*/

    }
}
