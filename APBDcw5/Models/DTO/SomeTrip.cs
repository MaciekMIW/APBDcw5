using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw5.Models.DTO
{
    public class SomeTrip
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public IEnumerable<SomeCountry> Countries { get; set; }
        public IEnumerable<SomeClient> Clients { get; set; }

    }
}
