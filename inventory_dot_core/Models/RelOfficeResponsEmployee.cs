using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    public partial class RelOfficeResponsEmployee
    {
        [Display(Name = "Код")]
        public int RoeId { get; set; }

        [Display(Name = "Офис")]
        public int RoeOfficeId { get; set; }

        [Display(Name = "Сотрудник")]
        public int RoeEmployeeId { get; set; }

        [Display(Name = "Сотрудники")]
        public virtual Employees RoeEmployee { get; set; }

        [Display(Name = "Офисы")]
        public virtual Offices RoeOffice { get; set; }
    }
}
