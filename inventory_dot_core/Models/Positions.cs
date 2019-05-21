using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class Positions
    {
        public Positions()
        {
            Employees = new HashSet<Employees>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string PositionNotes { get; set; }
        public int PositionDepartmentId { get; set; }

        public virtual Departments PositionDepartment { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
