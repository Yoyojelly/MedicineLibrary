using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace NewProject.Models
{
    //禁忌症
    public class SPJJZ : Table
    {
        [Key]
        public int GUID { get; set; }
        [Display(Name = "禁忌症ID")]
        public string JJZBH { get; set; }
        [Display(Name = "产品ID")]
        public int SPBH { get; set; }
        [Display(Name = "禁忌症名称")]
        public string JJZMC { get; set; }
    }
}
