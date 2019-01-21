using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
    public class UserPowers : Table
    {
        [Required,Key]
         public int UserPowersID { get; set; }
         //用户ID
         public int UserID {get;set;}
         //导航到user
         public User user {get;set;}
         //权限组ID
         public int powerTeamID {get;set;}
         //导航到powerTeam
         public PowerTeam powerTeam {get;set;}
    }
}
