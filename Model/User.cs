using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class User
    {
        /*public User()
        {
            Id = Guid.NewGuid();
        }*/
        public int Id { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string Adress { get; set; }
        public string Adress2 { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
    }
}
