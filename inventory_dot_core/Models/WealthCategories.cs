using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class WealthCategories
    {
        public WealthCategories()
        {
            WealthHardware = new HashSet<WealthHardware>();
            WealthSoftware = new HashSet<WealthSoftware>();
        }

        public int WcatId { get; set; }
        public string Wcatname { get; set; }
        public string Wcatnotes { get; set; }

        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
        public virtual ICollection<WealthSoftware> WealthSoftware { get; set; }
    }
}
