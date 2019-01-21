using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
    //医保信息
    public class SPYBXX : Table
    {
        [Key]
        public int GUID { get; set; }
        [Display(Name = "医保ID")]
        public string YBBH { get; set; }

        [Display(Name = "省")]
        public string SHENG { get; set; }
        [Display(Name = "城市")]
        public string SHI { get; set; }
        [Display(Name = "产品ID")]
        public int SPBH { get; set; }
        [Display(Name = "医保状态")]
        public string YBZT { get; set; }
        [Display(Name = "医保类别")]
        public string YBLB { get; set; }
        [Display(Name = "信息来源")]
        public string XXLY { get; set; }
    }
}
