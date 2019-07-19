using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class AccountingBatteries
    {
        public int AbId { get; set; }
        public int AbBatWhardId { get; set; }
        public int? AbWhardId { get; set; }

        public virtual WealthHardware AbBatWhard { get; set; }
        public virtual WealthHardware AbWhard { get; set; }
    }
}
