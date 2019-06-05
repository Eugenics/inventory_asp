using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class Positions
    {
        public Positions()
        {
            Employees = new HashSet<Employees>();
        }

        [Display(Name = "Код")]
        public int PositionId { get; set; }

        [Display(Name = "Наименование")]
        public string PositionName { get; set; }

        [Display(Name = "Примечание")]
        public string PositionNotes { get; set; }

        [Display(Name = "Код подразделения")]
        public int PositionDepartmentId { get; set; }

        [Display(Name = "Подразделение")]
        public virtual Departments PositionDepartment { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
