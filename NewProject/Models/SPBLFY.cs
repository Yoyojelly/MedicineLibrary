using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
    //不良反应
    public class SPBLFY : Table
    {
        [Key]
        public int GUID { get; set; }
        [Display(Name = "不良反应ID")]
        public string BLFYBH { get; set; }
        [Display(Name = "产品ID")]
        public int SPBH { get; set; }
        [Display(Name = "不良反应名称")]
        public string BLFYMC { get; set; }
    }
}
