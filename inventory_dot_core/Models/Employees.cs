using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class Employees
    {
        public Employees()
        {
            AccountingPhones = new HashSet<AccountingPhones>();
            RelHardwareEmployee = new HashSet<RelHardwareEmployee>();
            RelOfficeResponsEmployee = new HashSet<RelOfficeResponsEmployee>();
            WealthHardware = new HashSet<WealthHardware>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeFirstname { get; set; }
        public string EmployeeLastname { get; set; }
        public string EmployeeMiddlename { get; set; }
        public string EmployeePhoneHome { get; set; }
        public string EmployeeEmail { get; set; }
        public int EmployeePositionId { get; set; }
        public int EmployeeOfficeId { get; set; }
        public string EmployeePhoneWork { get; set; }
        public string EmployeeNote { get; set; }
        public string EmployeeFullFio { get; set; }
        public int? EmployeeIsChief { get; set; }
        public int? EmployeeIsRespons { get; set; }
        public int? UserId { get; set; }
        public int? EmployeeIsMol { get; set; }
        public int EmployeeRegionId { get; set; }

        public virtual Offices EmployeeOffice { get; set; }
        public virtual Positions EmployeePosition { get; set; }
        public virtual Region EmployeeRegion { get; set; }
        public virtual ICollection<AccountingPhones> AccountingPhones { get; set; }
        public virtual ICollection<RelHardwareEmployee> RelHardwareEmployee { get; set; }
        public virtual ICollection<RelOfficeResponsEmployee> RelOfficeResponsEmployee { get; set; }
        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
    }
}
