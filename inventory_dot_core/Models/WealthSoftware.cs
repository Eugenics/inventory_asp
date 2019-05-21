using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_dot_core.Models
{
    [NotMapped]
    public partial class WealthSoftware
    {
        public WealthSoftware()
        {
            RelSoftwareHardware = new HashSet<RelSoftwareHardware>();
        }

        public int WsoftId { get; set; }
        public string WsoftInumber { get; set; }
        public string WsoftFnumber { get; set; }
        public int? WsoftWcatId { get; set; }
        public int? WsoftWtypeId { get; set; }
        public string WsoftName { get; set; }
        public DateTime? WsoftDateOfAdoption { get; set; }
        public decimal? WsoftInitialCost { get; set; }
        public decimal? WsoftResidualValue { get; set; }
        public int WsoftRegionId { get; set; }
        public string WsoftNote { get; set; }
        public int? WsoftArchiv { get; set; }
        public DateTime? WsoftCreateDate { get; set; }
        public int WsoftCnt { get; set; }

        public virtual Region WsoftRegion { get; set; }
        public virtual WealthCategories WsoftWcat { get; set; }
        public virtual WealthTypes WsoftWtype { get; set; }
        public virtual ICollection<RelSoftwareHardware> RelSoftwareHardware { get; set; }
    }
}
