using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using NewProject.Models;
using Microsoft.AspNetCore.Cors;
namespace NewProject.Models
{
    [EnableCors("cors")]
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        //用户表--用户名，密码，id，用户权限组
        public DbSet<User> users { get; set; }
        //商品主数据
        public DbSet<SPZSJ> SPZSJ { get; set; }
        //药品规格
        public DbSet<SPGG> SPGGs { get; set; }
        //医保信息
        public DbSet<SPYBXX> SPYBXXs { get; set; }
        //适应症
        public DbSet<SPSYZ> SPSYZs { get; set; }
        //禁忌症
        public DbSet<SPJJZ> SPJJZs { get; set; }
        //不良反应
        public DbSet<SPBLFY> SPBLFYs { get; set; }
        //药物相互作用
        public DbSet<SPYWXHZY> SPYWXHZYs { get; set; }
        //特殊人群用药
        public DbSet<SPTSRQYY> SPTSRQYYs { get; set; }
        public DbSet<TYMD> TYMD { get; set; }
        public DbSet<TYMZ> TYMZ { get; set; }
        public DbSet<TYMX> TYMX { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<JGLB> JGLBs { get; set; }
        public DbSet<TYMLB> TYMLBs { get; set; }
        public DbSet<subPowers> subPower {get;set;}
        public DbSet<PowerTeam> powerTeams{get;set;}
        public DbSet<TeamPowers> TeamPowers{get;set;}
        public DbSet<UserPowers> userPowers{get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserSeed
            modelBuilder.Entity<User>().HasData(new User 
            { 
                UserID = 1 ,
                user_code = "Administrator",
                user_name = "管理员",
                user_pwd = "123456",
                isDisable = "0",
                zzjg_code = "001",
                user_explain = "系统内置管理员"
            });
            #endregion
            #region PowerSeed
            modelBuilder.Entity<subPowers>().HasData(new subPowers
            {
              subPowerID = 1,
              subPowerName = "模块",
              powerUrl = "",
              supPowerID = 0
            });
            modelBuilder.Entity<subPowers>().HasData(new subPowers
            {
              subPowerID = 2,
              subPowerName = "通用名库",
              powerUrl = "tymk",
              supPowerID = 1
            });
            modelBuilder.Entity<subPowers>().HasData(new subPowers
            {
              subPowerID = 3,
              subPowerName = "系统设置",
              powerUrl = "",
              supPowerID = 1
            });
            modelBuilder.Entity<subPowers>().HasData(new subPowers
            {
              subPowerID = 4,
              subPowerName = "用户管理",
              powerUrl = "yhgl",
              supPowerID = 3
            });
            modelBuilder.Entity<subPowers>().HasData(new subPowers
            {
              subPowerID = 5,
              subPowerName = "系统设置",
              powerUrl = "",
              supPowerID = 3
            });
            modelBuilder.Entity<subPowers>().HasData(new subPowers
            {
              subPowerID = 6,
              subPowerName = "菜单管理",
              powerUrl = "cdgl",
              supPowerID = 5
            });
            modelBuilder.Entity<subPowers>().HasData(new subPowers
            {
              subPowerID = 7,
              subPowerName = "权限组管理",
              powerUrl = "qxzgl",
              supPowerID = 5
            });
            modelBuilder.Entity<subPowers>().HasData(new subPowers
            {
              subPowerID = 8,
              subPowerName = "权限菜单维护",
              powerUrl = "qxcd",
              supPowerID = 5
            });
            
            #endregion
            #region PowerTeamSeed
            modelBuilder.Entity<PowerTeam>().HasData(new PowerTeam
            {
                powerTeamID = 1,
                powerTeamName = "系统内置管理员"
            });
            #endregion
            #region TeamPowerSeed
            modelBuilder.Entity<TeamPowers>().HasData(new TeamPowers
            {
                TeamPowersID = 1,
                powerTeamID = 1,
                subPowerID = 1,
            });
            modelBuilder.Entity<TeamPowers>().HasData(new TeamPowers
            {
                TeamPowersID = 2,
                powerTeamID = 1,
                subPowerID = 2,
            });
            modelBuilder.Entity<TeamPowers>().HasData(new TeamPowers
            {
                TeamPowersID = 3,
                powerTeamID = 1,
                subPowerID = 3,
            });
            modelBuilder.Entity<TeamPowers>().HasData(new TeamPowers
            {
                TeamPowersID = 4,
                powerTeamID = 1,
                subPowerID = 4,
            });
            modelBuilder.Entity<TeamPowers>().HasData(new TeamPowers
            {
                TeamPowersID = 5,
                powerTeamID = 1,
                subPowerID = 5,
            });
            modelBuilder.Entity<TeamPowers>().HasData(new TeamPowers
            {
                TeamPowersID = 6,
                powerTeamID = 1,
                subPowerID = 6,
            });
            modelBuilder.Entity<TeamPowers>().HasData(new TeamPowers
            {
                TeamPowersID = 7,
                powerTeamID = 1,
                subPowerID = 7,
            });
            modelBuilder.Entity<TeamPowers>().HasData(new TeamPowers
            {
                TeamPowersID = 8,
                powerTeamID = 1,
                subPowerID = 8,
            });
            #endregion 
            #region UserPowersSeed
            modelBuilder.Entity<UserPowers>().HasData(new UserPowers
            {
                UserPowersID = 1,
                UserID = 1,
                powerTeamID = 1,
            });
            #endregion
            #region TymdSeed
            modelBuilder.Entity<TYMD>().HasData(new TYMD
            {
                DLID = 1,
                DNAME = "中药",
            });
            modelBuilder.Entity<TYMD>().HasData(new TYMD
            {
                DLID = 2,
                DNAME = "西药",
            });
            modelBuilder.Entity<TYMD>().HasData(new TYMD
            {
                DLID = 3,
                DNAME = "其他"
            });
            #endregion
            #region TymzSeed
            modelBuilder.Entity<TYMZ>().HasData(new TYMZ
            {
                ZLID = 1,
                DLID = 1,
                ZNAME = "中药中类1"
            });
            modelBuilder.Entity<TYMZ>().HasData(new TYMZ
            {
                ZLID = 2,
                DLID = 1,
                ZNAME = "中药中类2"
            });
            modelBuilder.Entity<TYMZ>().HasData(new TYMZ
            {
                ZLID = 3,
                DLID = 2,
                ZNAME = "西药中类1"
            });
            modelBuilder.Entity<TYMZ>().HasData(new TYMZ
            {
                ZLID = 4,
                DLID = 2,
                ZNAME = "西药中类2"
            });
            modelBuilder.Entity<TYMZ>().HasData(new TYMZ
            {
                ZLID = 5,
                DLID = 3,
                ZNAME = "其他1"
            });
            modelBuilder.Entity<TYMZ>().HasData(new TYMZ
            {
                ZLID = 6,
                DLID = 3,
                ZNAME = "其他2"
            });
            #endregion
            #region TymxSeed
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 1,
                ZLID = 1,
                XNAME = "中药小类1",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 2,
                ZLID = 1,
                XNAME = "中药小类2",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 3,
                ZLID = 2,
                XNAME = "中药小类3",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 4,
                ZLID = 2,
                XNAME = "中药小类4",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 5,
                ZLID = 3,
                XNAME = "西药小类1",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 6,
                ZLID = 3,
                XNAME = "西药小类2",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 7,
                ZLID = 4,
                XNAME = "西药小类3",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 8,
                ZLID = 4,
                XNAME = "西药小类4",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 9,
                ZLID = 5,
                XNAME = "其他小类1",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 10,
                ZLID = 5,
                XNAME = "其他小类2",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 11,
                ZLID = 6,
                XNAME = "其他小类3",
            });
            modelBuilder.Entity<TYMX>().HasData(new TYMX
            {
                XLID = 12,
                ZLID = 6,
                XNAME = "其他小类4",
            });
            #endregion
            #region TymLbSeed
            modelBuilder.Entity<TYMLB>().HasData(new TYMLB
            {
                LBID = 1,
                LBNAME = "类别1",
            });
            modelBuilder.Entity<TYMLB>().HasData(new TYMLB
            {
                LBID = 2,
                LBNAME = "类别2",
            });
            modelBuilder.Entity<TYMLB>().HasData(new TYMLB
            {
                LBID = 3,
                LBNAME = "类别3",
            });
            #endregion
            #region JglbSeed
            modelBuilder.Entity<JGLB>().HasData(new JGLB
            {
                jglbID = 1,
                lbName = "处方药",
            });
            modelBuilder.Entity<JGLB>().HasData(new JGLB
            {
                jglbID = 2,
                lbName = "非处方药",
            });
            #endregion
        }
    }
}