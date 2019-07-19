using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class RelSoftwareHardware
    {
        public int RelshId { get; set; }
        public int RelshWsoftId { get; set; }
        public int RelshWhardId { get; set; }

        public virtual WealthHardware RelshWhard { get; set; }
        public virtual WealthSoftware RelshWsoft { get; set; }
    }
}
