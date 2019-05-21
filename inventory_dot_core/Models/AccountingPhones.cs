using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class AccountingPhones
    {
        public int ApId { get; set; }
        public int ApPhoneHwId { get; set; }
        public string ApInumber { get; set; }
        public string ApTnumber { get; set; }
        public string ApImei { get; set; }
        public int ApEmployeeId { get; set; }

        public virtual Employees ApEmployee { get; set; }
        public virtual WealthHardware ApPhoneHw { get; set; }
    }
}
