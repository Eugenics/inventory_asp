using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class Offices
    {
        public Offices()
        {
            Employees = new HashSet<Employees>();
            RelOfficeResponsEmployee = new HashSet<RelOfficeResponsEmployee>();
            WealthHardware = new HashSet<WealthHardware>();
        }

        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeNotes { get; set; }
        public int OfficeHousesId { get; set; }
        public int OfficeIsStore { get; set; }

        public virtual Houses OfficeHouses { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<RelOfficeResponsEmployee> RelOfficeResponsEmployee { get; set; }
        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
    }
}
