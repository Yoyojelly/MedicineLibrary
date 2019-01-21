using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace NewProject.Models
{
    //通用名中类
    public class TYMZ : Table
    {
        [Key]
        public int ZLID { get; set; }
        [Display(Name = "商品中类")]
        public string ZNAME { get; set; }
        //导航到药物
        [JsonIgnore]
        public List<Medicine> medicines { get; set; }

        //外键--大类
        public int DLID { get; set; }
    }
}
