using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
    //药品规格表
    public class SPGG : Table
    {
        [Key]
        public int GUID { get; set; }
        [Display(Name = "规格编号")]
        public string GGBH { get; set; }
        [Display(Name = "商品编号")]
        public int SPBH { get; set; }
        [Display(Name = "药品规格")]
        public string GGMC { get; set; }
        [Display(Name = "包装数量")]
        public string BZSL { get; set; }
        [Display(Name = "包装单位")]
        public string BZDW { get; set; }
    }
}
