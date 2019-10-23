using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    public partial class RelHardwareEmployee
    {
        public RelHardwareEmployee()
        {           
        }

        [Display(Name ="Код")]
        public int RelheId { get; set; }

        [Display(Name = "Сотрудник")]
        public int RelheEmployeeId { get; set; }

        [Display(Name = "Оборудование")]
        [ForeignKey("WealthHardware")]
        public int RelheWhardId { get; set; }

        [Display(Name = "Сотрудник")]
        public virtual Employees RelheEmployee { get; set; }

        [Display(Name = "Оборудование")]
        public virtual WealthHardware RelheWhard { get; set; }
        
    }
}
