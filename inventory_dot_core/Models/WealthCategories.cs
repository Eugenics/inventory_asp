using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    public partial class WealthCategories
    {
        public WealthCategories()
        {
            WealthHardware = new HashSet<WealthHardware>();
            WealthSoftware = new HashSet<WealthSoftware>();
        }

        [Display(Name = "Код")]
        public int WcatId { get; set; }

        [Display(Name = "Наименование")]
        public string Wcatname { get; set; }

        [Display(Name = "Примечание")]
        public string Wcatnotes { get; set; }

        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
        public virtual ICollection<WealthSoftware> WealthSoftware { get; set; }
    }
}
