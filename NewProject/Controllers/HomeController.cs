using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace NewProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;

        public HomeController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //用户id  在cookies获取
            string id;
            HttpContext.Request.Cookies.TryGetValue("userID", out id);
            HttpContext.Session.SetString("userID", id);

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        //登陆验证
        [HttpPost]
        public async Task<IActionResult> ToLogin()
        {
            //获取post请求的from
            string userName = Request.Form["userName"],
                   userPwd = Request.Form["userPwd"];
            //存储登陆状态 0表示成功，1表示密码错误，2表示账号不存在，3发生未知错误
            int loginStatus = 3;

            User user = await _context.users.FirstOrDefaultAsync(m => m.user_code == userName);

            if (user != null && user.user_pwd == userPwd)
            {
                //如果没被禁用
                if (user.isDisable == "0")
                {
                    loginStatus = 0;

                    //设置cookie
                    HttpContext.Response.Cookies.Append("userID", user.UserID.ToString());

                    return Json(loginStatus);
                }
                else
                {
                    ///4代表禁用了
                    return Json(4);
                }
            }
            else if (user != null && user.user_pwd != userPwd)
            {
                loginStatus = 1;
                return Json(loginStatus);
            }
            else if (user == null)
            {
                loginStatus = 2;
                return Json(loginStatus);
            }
            else
            {
                return Json(loginStatus);
            }
        }
        

        //注销
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("userID");
            return RedirectToAction(nameof(Login));
        }

        //修改密码
        public async Task<IActionResult> toAlterPwd()
        {
            var oldPwd = Request.Form["oldPwd"];
            var newPwd = Request.Form["newPwd"];
            var userID =Convert.ToInt32(HttpContext.Request.Cookies["userID"]);
            var user = await _context.users.FirstOrDefaultAsync(m => m.UserID == userID);
            if (oldPwd == user.user_pwd)
            {
                user.user_pwd = newPwd;
                _context.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        //获取用户列表
        public async Task<IActionResult> fetchUsers()
        {
            var Users = await _context.users.ToListAsync();
            return Json(Users);
        }
        //获取权限列表
        [HttpGet]
        public async Task<IActionResult> fetchPowers(int id)
        {
            var powers = await _context.userPowers
            .Where(m=>m.UserID==id)
            .Include(m=>m.powerTeam)
            .Select(m=>new{
                m.powerTeamID,
                m.powerTeam.powerTeamName
            })
            .ToListAsync();
            
            return Json(powers);
        }
    }
}
