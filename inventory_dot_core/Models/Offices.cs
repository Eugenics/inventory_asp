using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    [Authorize]
    public partial class Offices
    {
        public Offices()
        {
            Employees = new HashSet<Employees>();
            RelOfficeResponsEmployee = new HashSet<RelOfficeResponsEmployee>();
            WealthHardware = new HashSet<WealthHardware>();
        }

        [Display(Name = "Код")]
        public int OfficeId { get; set; }

        [Display(Name = "Наименование")]
        public string OfficeName { get; set; }

        [Display(Name = "Примечание")]
        public string OfficeNotes { get; set; }

        [Display(Name = "Код строения")]
        public int OfficeHousesId { get; set; }

        [Display(Name = "Склад")]
        public int OfficeIsStore { get; set; }

        [Display(Name = "Строение")]
        public virtual Houses OfficeHouses { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<RelOfficeResponsEmployee> RelOfficeResponsEmployee { get; set; }
        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
    }
}
