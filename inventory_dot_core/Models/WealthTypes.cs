using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class WealthTypes
    {
        public WealthTypes()
        {
            WealthHardware = new HashSet<WealthHardware>();
            WealthSoftware = new HashSet<WealthSoftware>();
        }

        public int WtypeId { get; set; }
        public string WtypeName { get; set; }
        public string WtypeNotes { get; set; }

        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
        public virtual ICollection<WealthSoftware> WealthSoftware { get; set; }
    }
}
