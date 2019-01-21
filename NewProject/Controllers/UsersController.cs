using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace MyProject.Controllers
{
    [EnableCors("cors")]
    public class UsersController : Controller
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        //删除用户
        public async Task<IActionResult> deleteUser(int id)
        {
            // try
            // {
            //     var user = await _context.users.FirstOrDefaultAsync(m => m.UserID == id);
            //     _context.Remove(user);
            //     await _context.SaveChangesAsync();
            //     return Json(true);
            // }
            // catch
            // {
            //     return Json(false);
            // }
            UserFunc userFunc = new UserFunc(_context);
            int number = await userFunc.deleteDataByIdAsync(id);
            return Json(true);
        }
        //添加用户
        public async Task<IActionResult> addUser()
        {
            //用户
            User user = new User();
            user.isDisable = Request.Form["isDisable"];
            user.user_explain = Request.Form["user_explain"];
            user.zzjg_code = Request.Form["zzjg_code"];
            user.user_name = Request.Form["user_name"];
            user.user_code = Request.Form["user_code"];
            user.user_pwd = "1";

            //权限列表
            int index = Convert.ToInt32(Request.Form["index"]);
            int[] powers = new int[index];
            //权限读取到powers数组里面
            for(int i = 0 ; i < index ; i++ )
            {
                powers[i] = Convert.ToInt32(Request.Form["powerTeam"+i]);
            }

            try
            {
                //先添加user,再根据user的id来添加power
                await _context.AddAsync(user);
                for(int i = 0 ; i < powers.Length ; i++ )
                {
                    UserPowers userPowers = new UserPowers();
                    userPowers.UserID = user.UserID;
                    userPowers.powerTeamID = powers[i];
                    await _context.userPowers.AddAsync(userPowers);
                }
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }

        }
        //更新用户
        public async Task<IActionResult> alterUser()
        {
            int id = Convert.ToInt32(Request.Form["userID"]);
            User user = await _context.users.FirstOrDefaultAsync(m => m.UserID == id);
            user.isDisable = Request.Form["isDisable"];
            user.user_explain = Request.Form["user_explain"];
            user.zzjg_code = Request.Form["zzjg_code"];
            user.user_name = Request.Form["user_name"];
            user.user_code = Request.Form["user_code"];

            //权限列表
            int index = Convert.ToInt32(Request.Form["index"]);
            //新权限列表
            int[] newPower = new int[index];
            for(int i = 0 ; i < index ; i++)
            {
                newPower[i] = Convert.ToInt32(Request.Form["powerTeam"+i]);
            }

            var powers =  _context.userPowers.Where(m=>m.UserID == id);
            
            //遍历每一个该用户的旧权限
            if(powers!=null)
            foreach(var i in powers)
            {
                //0表示新旧权限都有，不变化，1代表不变，2代表删除
                int type = 0;
                //遍历每一个该用户的新权限
                for(int n = 0 ; n < index ; n++ )
                {
                    //如果旧权限和新权限都有，则跳过
                    if(newPower[n]==i.powerTeamID){
                        type = 1;
                        //标记为-1则代表此项不用变，直接跳过
                        //最后有剩的则是需要新增的
                        newPower[n] = -1;
                        break;
                    }
                }
                //  type=0 则代表新权限表里没有这一项，删除
                if(type == 0)
                {
                    _context.Remove(i);
                }
                type = 0;
            }
            for(int i = 0 ; i < index ; i++ )
            {
                if(newPower[i]!=-1)
                {
                    UserPowers userPowers = new UserPowers();
                    userPowers.UserID = id;
                    userPowers.powerTeamID = newPower[i];
                    await _context.userPowers.AddAsync(userPowers);
                }
            }
            try
            {
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        public async Task<IActionResult> fetchUserById(int id)
        {
            User user = await _context.users.FirstOrDefaultAsync(m => m.UserID == id);
            return Json(user);
        }
 
        //查询子权限表的所有数据
        public async Task<IActionResult> fetchSubPowers()
        {
            var power = await _context.subPower.ToListAsync();
            return Json(power);
        }
        
        [EnableCors("cors")]
        //保存权限
        public async Task<IActionResult> saveSubPowers(){
            int num = Convert.ToInt32(Request.Form["num"]);
            //一个数组，当前位置存储oldID 下一个位置存储newID
            int[] idArr = new int[num*2];
            int idArrIndex = 0;
            for(int i = 0 ; i < num ; i++ )
            {
                string type = Request.Form[$"alter{i}"];
                //当添加时
                if(type == "add")
                {
                    subPowers subPower = new subPowers();
                    subPower.subPowerName = Request.Form[$"alter{i}Name"];
                    subPower.supPowerID = Convert.ToInt32(Request.Form[$"alter{i}supPowerID"]);
                    //若超类id和id数组中任何一项相等，则把数组的下一项赋值给超类id
                    for(int m = 0 ; m < idArr.Length ;m++)
                    {
                        if(subPower.supPowerID == idArr[m]){
                            subPower.supPowerID = idArr[m+1];
                            break;
                        }
                    }
                    //未自动生成之前的id
                    int oldID = Convert.ToInt32(Request.Form[$"alter{i}id"]);
                    //存储
                    await _context.subPower.AddAsync(subPower);
                    await _context.SaveChangesAsync();
                    //把旧id和新id存到数组
                    idArr[idArrIndex] = oldID;
                    idArrIndex++;
                    idArr[idArrIndex] = subPower.subPowerID;
                    idArrIndex++;
                }
                //删除
                if(type == "del")
                {
                    int id = Convert.ToInt32(Request.Form[$"alter{i}subPowerID"]);
                    var subpower = await _context.subPower.FirstOrDefaultAsync(m => m.subPowerID == id);
                    if(subpower!=null){
                        _context.subPower.Remove(subpower);
                    }
                }
            }
            try
            {
                
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
        //查询权限组的信息
        public async Task<IActionResult> fetchPowerTeams()
        {
            var powers = await _context.powerTeams.ToListAsync();
            return Json(powers);
        }
        //查询权限组的权限
        public async Task<IActionResult> fetchTeamPowerByPowerTeamId(int id)
        {
            var powers = await _context.TeamPowers
                             .Where(m=>m.powerTeamID==id)
                             .Select(m => new{
                                 m.subPowerID
                             })
                             .ToListAsync();

            return Json(powers);
        }
        //更改权限组的权限，并返回该权限组的权限
        public async Task<IActionResult> savePowerTeamPower()
        {
            int index = Convert.ToInt32(Request.Form["index"]);
            int id = Convert.ToInt32(Request.Form["powerTeamID"]);
            for(int i = 0 ; i < index ; i++)
            {
                //获取改动类型，添加或者删除
                string type = Request.Form[$"alter{i}"];
                int powerID = Convert.ToInt32(Request.Form[$"alter{i}id"]);
                if(type=="add"){
                    TeamPowers teamPowers = new TeamPowers();
                    teamPowers.subPowerID = powerID;
                    teamPowers.powerTeamID = id;
                    await _context.AddAsync(teamPowers);
                }else if(type=="del"){
                    TeamPowers teamPower =  await _context.TeamPowers
                           .FirstOrDefaultAsync(m=>m.powerTeamID==id&&m.subPowerID==powerID);
                    if(teamPower!=null)  _context.Remove(teamPower);
                }
                
            }
            try{
                await _context.SaveChangesAsync();
                var powers = await _context.TeamPowers
                             .Where(m=>m.powerTeamID==id)
                             .Select(m => new{
                                 m.subPowerID
                             })
                             .ToListAsync();
                return Json(powers);
            }
            catch{
                return Json(false);
            }
        }
        //保存权限组的信息--增删改全有
        public async Task<IActionResult> savePowerTeams(){
            int index = Convert.ToInt32(Request.Form["alterIndex"]);
            for(int i = 0 ; i < index ; i++)
            {
                string type = Request.Form["alter"+i];
                if(type == "alter"){
                    int id = Convert.ToInt32(Request.Form[$"alter{i}id"]);
                    var team =  await _context.powerTeams.FirstOrDefaultAsync(m => m.powerTeamID == id);
                    team.powerTeamName = Request.Form[$"alter{i}name"];
                }else if(type=="add"){
                    PowerTeam powerTeam = new PowerTeam();
                    powerTeam.powerTeamName = Request.Form[$"alter{i}name"];
                    await _context.AddAsync(powerTeam);
                }else if(type=="del"){
                    int id = Convert.ToInt32(Request.Form[$"alter{i}id"]);
                    var team = await _context.powerTeams.FirstOrDefaultAsync(m=>m.powerTeamID == id);
                    if(team!=null)
                    _context.powerTeams.Remove(team);
                }
            }
            try{
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
        //获取用户所拥有的权限组
        public async Task<IActionResult> fetchUserPowersByUserId(int id){

            return Json(await _context.userPowers
                    .Where(m=>m.UserID == id)
                    .Select(m=>new{m.powerTeamID})
                    .ToArrayAsync()
            );
        }
        public async Task<IActionResult> fetchCurrentPowers(){
            int userID = Convert.ToInt32(HttpContext.Session.GetString("userID"));
            //传参  userID
            var powers = await _context.subPower
            .FromSql($"select distinct tp.subPowerID,sp.subPowerName,sp.supPowerID,sp.OperatorID,sp.OperatorTime,sp.powerUrl,sp.Version from userPowers up,powerTeams pt,TeamPowers tp,subPower sp where up.UserID = {userID} and pt.powerTeamID = up.powerTeamID and tp.powerTeamID = pt.powerTeamID and sp.subPowerID = tp.subPowerID")
            .ToListAsync();
            
            return Json(powers);
        }
        //获取当前用户
        public async Task<IActionResult> fetchCurrentUser(){
            User user;
            int id = Convert.ToInt32(HttpContext.Session.GetString("userID"));
            user = await _context.users.FirstOrDefaultAsync(m => m.UserID == id);

            return Json(user);
        }
    }
}
