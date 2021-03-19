using System;
using System.Text;

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"    Start tid: {StartTime.ToString("dd/MM/yyyy H:mm:ss")}\n");
            sb.Append($"    Slut tid: {EndTime.ToString("dd/MM/yyyy H:mm:ss")}\n");
            sb.Append($"    Kunde Id: {CustId}\n");
            sb.Append($"    Telefon Nr.: {PhoneNo}\n");
            return sb.ToString();
        }
    }
}
