using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class WealthHardware
    {
        public WealthHardware()
        {
            AccountingBatteriesAbBatWhard = new HashSet<AccountingBatteries>();
            AccountingBatteriesAbWhard = new HashSet<AccountingBatteries>();
            AccountingCartridgesAcCartWhard = new HashSet<AccountingCartridges>();
            AccountingCartridgesAcWhard = new HashSet<AccountingCartridges>();
            AccountingPhones = new HashSet<AccountingPhones>();
            RelHardwareEmployee = new HashSet<RelHardwareEmployee>();
            RelSoftwareHardware = new HashSet<RelSoftwareHardware>();
        }

        public int WhardId { get; set; }
        public string WhardInumber { get; set; }
        public string WhardFnumber { get; set; }
        public int WhardWcatId { get; set; }
        public int WhardWtypeId { get; set; }
        public string WhardName { get; set; }
        public DateTime? WhardDateOfAdoption { get; set; }
        public decimal? WhardInitialCost { get; set; }
        public decimal? WhardResidualValue { get; set; }
        public int WhardOfficeId { get; set; }
        public string WhardNote { get; set; }
        public int? WhardArchiv { get; set; }
        public DateTime? WhardCreateDate { get; set; }
        public int? WhardMolEmployeeId { get; set; }
        public int? WhardRegionId { get; set; }
        public int IsSoftDeployed { get; set; }

        public virtual Employees WhardMolEmployee { get; set; }
        public virtual Offices WhardOffice { get; set; }
        public virtual Region WhardRegion { get; set; }
        public virtual WealthCategories WhardWcat { get; set; }
        public virtual WealthTypes WhardWtype { get; set; }
        public virtual ICollection<AccountingBatteries> AccountingBatteriesAbBatWhard { get; set; }
        public virtual ICollection<AccountingBatteries> AccountingBatteriesAbWhard { get; set; }
        public virtual ICollection<AccountingCartridges> AccountingCartridgesAcCartWhard { get; set; }
        public virtual ICollection<AccountingCartridges> AccountingCartridgesAcWhard { get; set; }
        public virtual ICollection<AccountingPhones> AccountingPhones { get; set; }
        public virtual ICollection<RelHardwareEmployee> RelHardwareEmployee { get; set; }
        public virtual ICollection<RelSoftwareHardware> RelSoftwareHardware { get; set; }
    }
}
