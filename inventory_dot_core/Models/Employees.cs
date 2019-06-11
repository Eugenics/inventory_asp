using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace inventory_dot_core.Models
{
    public partial class Employees
    {
        public Employees()
        {
            AccountingPhones = new HashSet<AccountingPhones>();
            RelHardwareEmployee = new HashSet<RelHardwareEmployee>();
            RelOfficeResponsEmployee = new HashSet<RelOfficeResponsEmployee>();
            WealthHardware = new HashSet<WealthHardware>();
        }

        [Display(Name = "Код")]
        public int EmployeeId { get; set; }

        [Display(Name = "Имя"),Required(AllowEmptyStrings =false,ErrorMessage ="Не указано имя!")]
        public string EmployeeFirstname { get; set; }

        [Display(Name = "Фамилия"), Required(AllowEmptyStrings = false, ErrorMessage = "Не указано фамилия!")]
        public string EmployeeLastname { get; set; }

        [Display(Name = "Отчество"), Required(AllowEmptyStrings = false, ErrorMessage = "Не указано отчество!")]
        public string EmployeeMiddlename { get; set; }

        [Display(Name = "Домашний телефон")]
        public string EmployeePhoneHome { get; set; }

        [Display(Name = "email")]
        public string EmployeeEmail { get; set; }

        [Display(Name = "Должность"), Required(AllowEmptyStrings = false, ErrorMessage = "Не указана должность!")]
        public int EmployeePositionId { get; set; }

        [Display(Name = "Офис"), Required(AllowEmptyStrings = false, ErrorMessage = "Не указан офис!")]
        public int EmployeeOfficeId { get; set; }

        [Display(Name = "Телефон")]
        public string EmployeePhoneWork { get; set; }

        [Display(Name = "Примечание")]
        public string EmployeeNote { get; set; }

        [Display(Name = "ФИО полностью")]
        public string EmployeeFullFio { get; set; }

        [Display(Name = "Руководитель")]
        public int? EmployeeIsChief {get; set;}

        [Display(Name = "Ответственный")]
        public int? EmployeeIsRespons { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "МОЛ")]
        public int? EmployeeIsMol { get; set; }

        [Display(Name = "Регион")]
        public int EmployeeRegionId { get; set; }

        [Display(Name = "Офис")]
        public virtual Offices EmployeeOffice { get; set; }

        [Display(Name = "Должность")]
        public virtual Positions EmployeePosition { get; set; }

        [Display(Name = "Регион")]
        public virtual Region EmployeeRegion { get; set; }

        public virtual ICollection<AccountingPhones> AccountingPhones { get; set; }
        public virtual ICollection<RelHardwareEmployee> RelHardwareEmployee { get; set; }
        public virtual ICollection<RelOfficeResponsEmployee> RelOfficeResponsEmployee { get; set; }
        public virtual ICollection<WealthHardware> WealthHardware { get; set; }
    }
}
