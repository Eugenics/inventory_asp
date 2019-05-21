using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class RelHardwareEmployee
    {
        public int RelheId { get; set; }
        public int RelheEmployeeId { get; set; }
        public int RelheWhardId { get; set; }

        public virtual Employees RelheEmployee { get; set; }
        public virtual WealthHardware RelheWhard { get; set; }
    }
}
