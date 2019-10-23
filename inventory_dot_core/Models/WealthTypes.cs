using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{ 
    public partial class WealthTypes
    {
        public WealthTypes()
        {
            WealthHardware = new HashSet<WealthHardware>();
            WealthSoftware = new HashSet<WealthSoftware>();
        }

        [Display(Name ="Код")]
        public int WtypeId { get; set; }

        [Display(Name = "Наименование")]
        public string WtypeName { get; set; }

        [Display(Name = "Примечание")]
        public string WtypeNotes { get; set; }

        [Display(Name ="IT оборудование")]
        [UIHint("_YesNoTemplate")]
        public int wtype_is_it { get; set; }

        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
        public virtual ICollection<WealthSoftware> WealthSoftware { get; set; }
    }
}
