using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw5.Models.DTO
{
    public class ClientToTrip
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Telephone { get; set; }
        public String Pesel { get; set; }
        public int IdTrip { get; set; }
        public String Name { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
