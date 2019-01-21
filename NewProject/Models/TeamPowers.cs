using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;

namespace NewProject.Models
{
    [EnableCors("cors")]
    public class TeamPowers : Table
    {
        [Key]
        //id
        public int TeamPowersID{get;set;}
        //权限组id
        public int powerTeamID{get;set;}
        //导航到权限组
        public PowerTeam powerTeam{get;set;}
        //权限id
        public int subPowerID{get;set;}
        //导航到权限
        public subPowers subPowers{get;set;}

    }
}
