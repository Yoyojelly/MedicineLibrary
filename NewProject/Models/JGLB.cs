using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
    public class JGLB : Table
    {
        [Key]
        public int jglbID { get; set; }
        [Display(Name ="监管类别")]
        public string lbName { get; set; }
        public List<Medicine> medicines { get; set; }
    }
}
