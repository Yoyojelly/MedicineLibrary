using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;

namespace NewProject.Models
{
    [EnableCors("cors")]
    public class PowerTeam : Table
    {
        [Key]
        //权限组id
        public int powerTeamID{get;set;}
        public List<TeamPowers> teamPowers{get;set;}
        //权限组名称
        public string powerTeamName{get;set;}

    }
}
