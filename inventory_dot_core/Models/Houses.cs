using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class Houses
    {
        public Houses()
        {
            Offices = new HashSet<Offices>();
        }

        public int HousesId { get; set; }
        public string HousesName { get; set; }
        public string HousesRem { get; set; }
        public int HousesRegionId { get; set; }

        public virtual Region HousesRegion { get; set; }
        public virtual ICollection<Offices> Offices { get; set; }
    }
}
