using System;

namespace Models
{
    public class Call
    {
        public Call()
        {
            //Id = Guid.NewGuid();
        }
        public int Id { get; set; }
        public int CustId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PhoneNo { get; set; }
    }
}
