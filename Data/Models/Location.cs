using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Location
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        //public virtual ShablonUser creator { get; set; } = new ShablonUser();
        public string creatorUsername { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
