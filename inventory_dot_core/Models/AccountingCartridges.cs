using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class AccountingCartridges
    {
        public int AcId { get; set; }
        public int AcCartWhardId { get; set; }
        public int? AcWhardId { get; set; }

        public virtual WealthHardware AcCartWhard { get; set; }
        public virtual WealthHardware AcWhard { get; set; }
    }
}
