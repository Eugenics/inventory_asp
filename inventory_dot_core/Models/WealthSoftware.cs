using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    public partial class WealthSoftware
    {
        public WealthSoftware()
        {
            RelSoftwareHardware = new HashSet<RelSoftwareHardware>();
        }

        [Display(Name ="Код")]
        public int WsoftId { get; set; }

        [Display(Name = "Инвентарный номер")]
        public string WsoftInumber { get; set; }

        [Display(Name = "Серийный номер")]
        public string WsoftFnumber { get; set; }

        [Display(Name = "Категория")]
        public int? WsoftWcatId { get; set; }

        [Display(Name = "Тип")]
        public int? WsoftWtypeId { get; set; }

        [Display(Name = "Наименование")]
        public string WsoftName { get; set; }

        [Display(Name = "Дата постановки на учет")]
        public DateTime? WsoftDateOfAdoption { get; set; }

        [Display(Name = "Начальная стоимость")]
        public decimal? WsoftInitialCost { get; set; }

        [Display(Name = "Остаточная стоимость")]
        public decimal? WsoftResidualValue { get; set; }

        [Display(Name = "Регион")]
        public int WsoftRegionId { get; set; }

        [Display(Name = "Примечание")]
        public string WsoftNote { get; set; }

        [Display(Name = "Архив")]
        [UIHint("_YesNoTemplate")]
        public int? WsoftArchiv { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime? WsoftCreateDate { get; set; }

        [Display(Name = "Количество лицензий")]
        public int WsoftCnt { get; set; }

        [Display(Name = "Регион")]
        public virtual Region WsoftRegion { get; set; }

        [Display(Name = "Категория")]
        public virtual WealthCategories WsoftWcat { get; set; }

        [Display(Name = "Тип")]
        public virtual WealthTypes WsoftWtype { get; set; }

        [Display(Name = "Оборудование")]
        public virtual ICollection<RelSoftwareHardware> RelSoftwareHardware { get; set; }
    }
}
