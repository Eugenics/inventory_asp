using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class RelSoftwareHardware
    {
        public RelSoftwareHardware()
        {
        }

        [Display(Name = "Код")]
        public int RelshId { get; set; }
        [Display(Name = "Код ПО")]
        public int RelshWsoftId { get; set; }
        [Display(Name = "Код МЦ")]
        public int RelshWhardId { get; set; }
        [Display(Name = "Оборудование")]
        public virtual WealthHardware RelshWhard { get; set; }
        [Display(Name = "Програмное обеспечение")]
        public virtual WealthSoftware RelshWsoft { get; set; }
    }
}
