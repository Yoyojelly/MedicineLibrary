using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NewProject.Models
{
    public class TYMX : Table
    {
        [Key]
        public int XLID { get; set; }
        [Display(Name = "商品小类")]
        public string XNAME { get; set; }
        //导航到药物
        [JsonIgnore]
        public List<Medicine> medicines { get; set; }

        //外键--中类
        public int ZLID { get; set; }
    }
}
