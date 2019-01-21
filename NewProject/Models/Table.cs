using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
    public class Table
    {
        //最后一次操作的用户
        public int OperatorID { get; set; }

        //最后一次操作时间
        public string OperatorTime { get; set; }
        
        //版本号
        public int Version { get; set; }


    }
}
