using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public enum Category
    {
        CleaningAndDesinfection,
        PetAndPlantCare,
        ChildCare,
        ElderCare
    }
    public class AppTask
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string creatorUsername { get; set; } = string.Empty;
        //public virtual ShablonUser creator { get; set; } = new ShablonUser();
        public string LocId { get; set; } = string.Empty;
        //public virtual Location location { get; set; } = new Location();
        //public virtual Category category { get; set; }

    }
}
