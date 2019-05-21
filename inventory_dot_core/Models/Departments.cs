using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class Departments
    {
        public Departments()
        {
            InverseDepartmentParent = new HashSet<Departments>();
            Positions = new HashSet<Positions>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNotes { get; set; }
        public int DepartmentRegionId { get; set; }
        public int? DepartmentParentId { get; set; }

        public virtual Departments DepartmentParent { get; set; }
        public virtual Region DepartmentRegion { get; set; }
        public virtual ICollection<Departments> InverseDepartmentParent { get; set; }
        public virtual ICollection<Positions> Positions { get; set; }
    }
}
