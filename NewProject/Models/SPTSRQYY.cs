using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace NewProject.Models
{
    public class SPTSRQYY : Table
    {
        [Key]
        public int GUID { get; set; }
        [Display(Name = "产品ID")]
        public int SPBH { get; set; }
        [Display(Name = "人群类型")]
        public string RQLX { get; set; }
        [Display(Name = "用药说明")]
        public string YYSM { get; set; }
    }
}
