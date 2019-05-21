using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class Region
    {
        public Region()
        {
            Departments = new HashSet<Departments>();
            Employees = new HashSet<Employees>();
            Houses = new HashSet<Houses>();
            WealthHardware = new HashSet<WealthHardware>();
            WealthSoftware = new HashSet<WealthSoftware>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Departments> Departments { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<Houses> Houses { get; set; }
        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
        public virtual ICollection<WealthSoftware> WealthSoftware { get; set; }
    }
}
