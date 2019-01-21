using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace NewProject.Models
{
    //药物相互作用
    public class SPYWXHZY : Table
    {
        [Key]
        public int GUID { get; set; }
        [Display(Name = "相互作用ID")]
        public string XHZYBH { get; set; }
        [Display(Name = "产品ID1")]
        public int SPBH1 { get; set; }
        [Display(Name = "产品ID2")]
        public int SPBH2 { get; set; }
        [Display(Name = "作用效果")]
        public string ZYXG { get; set; }
    }
}
