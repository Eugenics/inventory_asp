using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
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

        [Display(Name = "Код")]
        public int WhardId { get; set; }

        [Display(Name = "Инвентарный номер")]
        public string WhardInumber { get; set; }

        [Display(Name = "Заводской номер")]
        public string WhardFnumber { get; set; }

        [Display(Name = "Код категории ТМЦ")]
        public int WhardWcatId { get; set; }

        [Display(Name = "Код типа ТМЦ")]
        public int WhardWtypeId { get; set; }

        [Display(Name = "Наименование")]
        public string WhardName { get; set; }

        [Display(Name = "Дата постановки на учет")]
        public DateTime? WhardDateOfAdoption { get; set; }

        [Display(Name = "Начальная стоимость")]
        public decimal? WhardInitialCost { get; set; }

        [Display(Name = "Остаточная стоимость")]
        public decimal? WhardResidualValue { get; set; }

        [Display(Name = "Код офиса")]
        public int WhardOfficeId { get; set; }

        [Display(Name = "Примечание")]
        public string WhardNote { get; set; }

        [Display(Name = "В архиве")]
        public int? WhardArchiv { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime? WhardCreateDate { get; set; }

        [Display(Name = "Код сотрудника МОЛ")]
        public int? WhardMolEmployeeId { get; set; }

        [Display(Name = "Код региона")]
        public int? WhardRegionId { get; set; }

        [Display(Name = "Возможна установка Software")]
        public int IsSoftDeployed { get; set; }

        [Display(Name = "МОЛ сотрудник")]
        public virtual Employees WhardMolEmployee { get; set; }

        [Display(Name = "Офис")]
        public virtual Offices WhardOffice { get; set; }

        [Display(Name = "Регион")]
        public virtual Region WhardRegion { get; set; }

        [Display(Name = "Категория ТМЦ")]
        public virtual WealthCategories WhardWcat { get; set; }

        [Display(Name = "Тип ТМЦ")]
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
