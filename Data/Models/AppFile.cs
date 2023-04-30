using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AppFile
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public byte[] Content { get; set; }
    }
}
