using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUDMVC.Models
{
    public class EmployeModel
    {
        public int id { get; set; }
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public Nullable<int> salary { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Genders { get; set; }
        [Required]
        public string Date { get; set; }
    }
}

