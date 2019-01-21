using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace NewProject.Models
{
    //适应症
    public class SPSYZ : Table
    { 
        [Key]
        public int GUID { get; set; }
        [Display(Name = "适应症ID")]
        public string SYZBH { get; set; }
        [Display(Name = "产品ID")]
        public int SPBH { get; set; }
        [Display(Name = "标准适应症名称")]
        public string SYZMC { get; set; }
    }
}
