using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
    public class TYMLB : Table
    {
        [Key]
        public int LBID { get; set; }

        [Display(Name = "药物类别")]
        public string LBNAME { get; set; }
        //导航到药物
        public List<Medicine> medicines { get; set; }
    }
}
