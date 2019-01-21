using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NewProject.Models
{
    //通用名大类
    public class TYMD : Table
    {
        [Key]
        public int DLID { get; set; }
        [Display(Name ="商品大类")]
        public string DNAME { get; set; }
        //导航到药物
        [JsonIgnore]
        public List<Medicine> medicines { get; set; }
    }
}
