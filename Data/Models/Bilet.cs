using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Bilet
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string UserId { get; set; }
    }
}
