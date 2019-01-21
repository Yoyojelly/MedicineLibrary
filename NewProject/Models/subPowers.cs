using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
namespace NewProject.Models
{
    [EnableCors("cors")]
    public class subPowers : Table
    {
        [Key]
        public int subPowerID { get; set; }
        //子权限名称
        public string subPowerName { get; set; }
        //权限指向的地址
        public string powerUrl{get;set;}
        //子权限所属超权限 ---一般只指定上一级权限的id
        public int supPowerID{get;set;}
    }
}
