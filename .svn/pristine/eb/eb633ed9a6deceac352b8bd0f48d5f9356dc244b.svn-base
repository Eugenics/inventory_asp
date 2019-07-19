using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    public partial class Departments
    {
        public Departments()
        {
            InverseDepartmentParent = new HashSet<Departments>();
            Positions = new HashSet<Positions>();
        }

        [Display(Name = "Код")]
        public int DepartmentId { get; set; }

        [Display(Name = "Наименование")]
        public string DepartmentName { get; set; }

        [Display(Name = "Примечание")]
        public string DepartmentNotes { get; set; }

        [Display(Name = "Регион")]
        public int DepartmentRegionId { get; set; }

        [Display(Name = "Головное подразделение")]
        public int? DepartmentParentId { get; set; }

        [Display(Name = "Головное подразделение")]
        public virtual Departments DepartmentParent { get; set; }

        [Display(Name = "Регион")]
        public virtual Region DepartmentRegion { get; set; }
        public virtual ICollection<Departments> InverseDepartmentParent { get; set; }
        public virtual ICollection<Positions> Positions { get; set; }
    }
}
