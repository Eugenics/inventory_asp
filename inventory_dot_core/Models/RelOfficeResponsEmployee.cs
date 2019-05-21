using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class RelOfficeResponsEmployee
    {
        public int RoeId { get; set; }
        public int RoeOfficeId { get; set; }
        public int RoeEmployeeId { get; set; }

        public virtual Employees RoeEmployee { get; set; }
        public virtual Offices RoeOffice { get; set; }
    }
}
