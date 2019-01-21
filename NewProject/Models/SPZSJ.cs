using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NewProject.Models
{
    //商品主数据表
    
    public class SPZSJ : Table
    {
        //产品ID
        [Key,Display(Name ="商品编号")]
        public int SPBH { get; set; }
        
        
        [Display(Name = "批准文号")]
        public string PZWH { get; set; }
        [Display(Name = "原始品名")]
        public string YSPM { get; set; }
        //[Display(Name = "标准通用名称")]
        //public string TYM { get; set; }
        //一个商品对应一个通用名
        public int MedicineID { get; set; }
        //导航到通用名
        public Medicine medicine { get; set; }
        [Display(Name = "剂型")]
        public string JX { get; set; }
        [Display(Name = "商品名")]
        public string SPM { get; set; }
        [Display(Name = "生产商")]
        public string SCDW { get; set; }
        [Display(Name = "监管类别")]
        public string JGLB { get; set; }
        [Display(Name = "药物类别")]
        public string YWLB { get; set; }
        [Display(Name = "说明书适应症")]
        public string SMSSYZ { get; set; }
        [Display(Name = "说明书用法用量")]
        public string SMSYFYL { get; set; }
        [Display(Name = "服药与进食")]
        public string FYYJS { get; set; }
        [Display(Name = "说明书禁忌症")]
        public string SMSJJZ { get; set; }
        [Display(Name = "不良反应")]
        public string BLFY { get; set; }
        [Display(Name = "药物相互作用")]
        public string YWXHZY { get; set; }
        [Display(Name = "信息来源")]
        public string XXLY { get; set; }
        [Display(Name ="成分")]
        public string YWCF { get; set; }
        [Display(Name ="特殊人群用药")]
        public string TSRQYY { get; set; }
    }

    //商品列表专用模型
    public struct SpInfo
    {
        public string SPM;
        public string SCDW;
        public int SPBH;
    }
    public class SpzsjFunc
    {
        private readonly MyDbContext dbContext;

        public SpzsjFunc(MyDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        ///<summary>
        ///获取商品列表 by id
        ///</summary>
        public async Task<List<SpInfo>> fetchSpListAsync(int id)
        {
            return await dbContext.SPZSJ.Where(m=>m.MedicineID == id)
            .Select(m => new SpInfo
            {
                SPM = m.SPM,
                SCDW = m.SCDW,
                SPBH = m.SPBH
            }).ToListAsync();
        }
        ///<summary>
        ///保存商品规格
        ///</summary>
        public async Task<bool> saveSpggAsync(int SPBH,string GGMC,string BZSL,string BZDW,bool need)
        {
            if(!need) return true;

            try
            {
                SPGG spgg = new SPGG();
                spgg.SPBH = SPBH;
                spgg.GGMC = GGMC;
                spgg.BZSL = BZSL;
                spgg.BZDW = BZDW;
                await dbContext.SPGGs.AddAsync(spgg);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        ///<summary>
        ///保存商品适应症
        ///</summary>
        public async Task<bool> saveSpsyzAsync(int SPBH,string SYZMC,bool need)
        {
            if(!need) return true;
            try
            {
                SPSYZ spsyz = new SPSYZ();
                spsyz.SPBH = SPBH;
                spsyz.SYZMC = SYZMC;
                await dbContext.SPSYZs.AddAsync(spsyz);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        ///<summary>
        ///保存商品禁忌症
        ///</summary>
        public async Task<bool> saveSpjjzAsync(int SPBH,string JJZMC,bool need)
        {
            if(!need) return true;

            try
            {
            SPJJZ spjjz = new SPJJZ();
            spjjz.SPBH = SPBH;
            spjjz.JJZMC = JJZMC;
                await dbContext.SPJJZs.AddAsync(spjjz);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        ///<summary>
        ///保存商品不良反应
        ///</summary>
        public async Task<bool> saveSpblfyAsync(int SPBH,string BLFYMC,bool need)
        {
            if(!need) return true;

            try
            {
            SPBLFY spblfy = new SPBLFY();
            spblfy.SPBH = SPBH;
            spblfy.BLFYMC = BLFYMC;
                await dbContext.SPBLFYs.AddAsync(spblfy);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        ///<summary>
        ///保存特殊人群用药
        ///</summary>
        public async Task<bool> saveSptsrqyyAsync(int SPBH,string RQLX,string YYSM,bool need)
        {
            if(!need) return true;

            try
            {
            SPTSRQYY sptsrqyy = new SPTSRQYY();
            sptsrqyy.SPBH = SPBH;
            sptsrqyy.RQLX =RQLX;
            sptsrqyy.YYSM =YYSM;
                await dbContext.SPTSRQYYs.AddAsync(sptsrqyy);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        ///<summary>
        ///保存药物相互作用
        ///</summary>
        public async Task<bool> saveSpywxhzy(int SPBH1,int SPBH2,string ZYXG,bool need)
        {
            if(!need) return true;

            try
            {
            SPYWXHZY spywxhfy = new SPYWXHZY();
            spywxhfy.SPBH1 = SPBH1;
            spywxhfy.SPBH2 = SPBH2;
            spywxhfy.ZYXG = ZYXG;
                await dbContext.SPYWXHZYs.AddAsync(spywxhfy);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        ///<sumary>
        ///保存商品主数据
        ///</sumary>
        public async Task<int> saveSpzsj(int MedicineID,string PZWH,string JX,
        string SPM,string SCDW,string XXLY,string YWCF,string SMSSYZ,string SMSJJZ,
        string SMSYFYL,string BLFY,bool BZCT)
        {
            try
            {
                var medicine = await dbContext.Medicine
                        .Include(m=>m.JGLB)
                        .Include(m=>m.TYMLB)
                        .FirstOrDefaultAsync(m => m.MedicineID == MedicineID);
    
                SPZSJ spzsj = new SPZSJ();
                    spzsj.PZWH =  PZWH ;
                    spzsj.JX =  JX ;
                    spzsj.SPM =  SPM ;
                    spzsj.SCDW =  SCDW ;
                    spzsj.XXLY =  XXLY ;
                    spzsj.YWCF =  YWCF ;
                    spzsj.SMSSYZ =  SMSSYZ ;
                    spzsj.SMSJJZ =  SMSJJZ ;
                    spzsj.SMSYFYL =  SMSYFYL ;
                    spzsj.BLFY =  BLFY ;
                    spzsj.MedicineID = MedicineID;
                    spzsj.medicine = medicine;
                    spzsj.YWLB = medicine.TYMLB.LBNAME;
                    spzsj.JGLB = medicine.JGLB.lbName;


                await dbContext.SPZSJ.AddAsync(spzsj);
                await dbContext.SaveChangesAsync();
                if(BZCT)
                {
                    medicine.bzctID = spzsj.SPBH;
                    await dbContext.SaveChangesAsync();
                }
                
                return spzsj.SPBH;
            }
            catch
            {
                return -1;
            }
        }
    }


}
