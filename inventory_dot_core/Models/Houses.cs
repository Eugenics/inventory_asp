using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    public partial class Houses
    {
        public Houses()
        {
            Offices = new HashSet<Offices>();
        }

        [Display(Name = "Код")]
        public int HousesId { get; set; }

        [Display(Name = "Наименование")]
        public string HousesName { get; set; }

        [Display(Name = "Примечание")]
        public string HousesRem { get; set; }

        [Display(Name = "Код региона")]
        public int HousesRegionId { get; set; }

        [Display(Name = "Регион")]
        public virtual Region HousesRegion { get; set; }
        public virtual ICollection<Offices> Offices { get; set; }
    }
}
