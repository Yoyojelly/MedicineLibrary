using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NewProject.Models
{
    public class User : Table
    {
        #region 字段
        [Required]
        public int UserID { get; set; }
        [Required,Display(Name ="用户代码")]
        public string user_code { get; set; }
        [Required, Display(Name = "用户名")]
        public string user_name { get; set; }
        [Required, Display(Name = "密码")]
        public string user_pwd { get; set; }
        [Display(Name = "是否禁用")]
        public string isDisable { get; set; }
        //组织机构代码
        [Display(Name ="组织机构代码")]
        public string zzjg_code { get; set; }
        [Display(Name ="说明")]
        public string user_explain { get; set; }
        public List<UserPowers> userPowers {get;set;}
        #endregion
    }

    public class UserFunc : IMyDbFunc<User>
    {
        public User user;
        private readonly MyDbContext myDbContext;
        public UserFunc(MyDbContext _myDbContext){
            user = new User();
            myDbContext = _myDbContext;
        }
        /// <summary>
        ///  Find user by id , use userFunc.user to obtain it
        ///  </summary>
        ///  <param name="id">The user's ID</param>
        
        public async void FindUserAsync(int id)
        {
            this.user = await myDbContext.users.FirstOrDefaultAsync(m=>m.UserID == id);
        }
        /// <summary>
        /// add and save this.user
        /// </summary>
        public async Task<int> addDataAsync()
        {
            await myDbContext.users.AddAsync(this.user);
            return await myDbContext.SaveChangesAsync();
        }
        /// <summary>
        /// delete user by userID
        /// </summary>
        /// <returns>Return number of alter rols </returns>
        public async Task<int> deleteDataByIdAsync(int id)
        {
            User user1 = await myDbContext
                               .users
                               .Include(m=>m.userPowers)
                               .FirstOrDefaultAsync(m=>m.UserID==id);
            myDbContext.users.Remove(user1);
            return await myDbContext.SaveChangesAsync();
        }


        /// <summary>
        /// No code
        /// </summary>
        public async Task<int> saveChangesAsync()
        {
           return await this.myDbContext.SaveChangesAsync();
        }
    }
}
