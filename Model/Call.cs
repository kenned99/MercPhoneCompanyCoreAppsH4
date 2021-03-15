using System;

namespace Models
{
    public class Call
    {
        public Call()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
