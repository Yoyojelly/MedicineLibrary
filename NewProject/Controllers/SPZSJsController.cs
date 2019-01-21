using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using Microsoft.AspNetCore.Cors;

namespace MyProject.Controllers
{
    [EnableCors("cors")]
    public class SPZSJsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly SpzsjFunc spzsjFunc;

        public SPZSJsController(MyDbContext context)
        {
            _context = context;
            spzsjFunc = new SpzsjFunc(context);
        }

        //保存商品主数据--启用事务
        [HttpPost]
        public async Task<IActionResult> saveSpzsj()
        {
            //启用事务
            using (var transaction = _context.Database.BeginTransaction())
            {
                var a1 =  Convert.ToInt32(Request.Form["medicineID"]);
                var a2 =  Request.Form["PZWH"];
                var a3 = Request.Form["JX"];
                var a4 =  Request.Form["SPM"];
                var a5 =  Request.Form["SCDW"];
                var a6 =  Request.Form["XXLY"];
                var a7 =  Request.Form["YWCF"];
                var a8 =  Request.Form["SMSSYZ"];
                var a9 = Request.Form["SMSJJZ"];
                var a10 = Request.Form["BLFY"];
                var a11 =  Request.Form["SMSYFYL"];
                var a12 = Convert.ToBoolean(Request.Form["bzct"]);
                //保存主数据并返回主键
                int spbh = await spzsjFunc.saveSpzsj(
                    MedicineID: Convert.ToInt32(Request.Form["medicineID"]),
                    PZWH: Request.Form["PZWH"],
                    JX: Request.Form["JX"],
                    SPM: Request.Form["SPM"],
                    SCDW: Request.Form["SCDW"],
                    XXLY: Request.Form["XXLY"],
                    YWCF: Request.Form["YWCF"],
                    SMSSYZ: Request.Form["SMSSYZ"],
                    SMSJJZ: Request.Form["SMSJJZ"],
                    BLFY: Request.Form["BLFY"],
                    SMSYFYL: Request.Form["SMSYFYL"],
                    BZCT:Convert.ToBoolean(Request.Form["bzct"])
                );
                //如果保存失败了就返回
                if (spbh == -1) return Json(false);

                //子表线程
                Task<bool> taskSpgg;
                Task<bool> taskSpsyz;
                Task<bool> taskSpjjz;
                Task<bool> taskSpblfy;
                Task<bool> taskSptsrqyy;
                Task<bool> taskSpywxhzy;

                var asd = Request.Form["alterSPGG"];

                #region 异步保存子表
                //商品规格
                taskSpgg = spzsjFunc.saveSpggAsync(
                SPBH: spbh,
                GGMC: Request.Form["GGMC"],
                BZSL: Request.Form["BZSL"],
                BZDW: Request.Form["BZDW"],
                need: Convert.ToBoolean(Request.Form["alterSPGG"]));
                //保存商品适应症
                taskSpsyz = spzsjFunc.saveSpsyzAsync(
                   SPBH: spbh,
                   SYZMC: Request.Form["SYZMC"],
                   need: Convert.ToBoolean(Request.Form["alterSPSYZ"]));
                //商品禁忌症
                taskSpjjz = spzsjFunc.saveSpjjzAsync(
                    SPBH: spbh,
                    JJZMC: Request.Form["JJZMC"],
                    need: Convert.ToBoolean(Request.Form["alterSPJJZ"])
                );
                //不良反应
                taskSpblfy = spzsjFunc.saveSpblfyAsync(
                    SPBH: spbh,
                    BLFYMC: Request.Form["BLFYMC"],
                    need: Convert.ToBoolean(Request.Form["alterSPBLFY"])
                );
                //特殊人群用药
                taskSptsrqyy = spzsjFunc.saveSptsrqyyAsync(
                    SPBH: spbh,
                    RQLX: Request.Form["RQLX"],
                    YYSM: Request.Form["YYSM"],
                    need: Convert.ToBoolean(Request.Form["alterSPTSRQYY"])
                );
                //相互作用
                // taskSpywxhzy = spzsjFunc.saveSpywxhzy(
                //     SPBH1: Convert.ToInt32(Request.Form["SPBH1"]),
                //     SPBH2: Convert.ToInt32(Request.Form["SPBH2"]),
                //     ZYXG: Request.Form["ZYXG"],
                //     need: Convert.ToBoolean(Request.Form["alterSPYWXHZY"])
                // );
                #endregion
                //当所有线程全部执行完毕时
                await Task.WhenAll(taskSpgg, taskSpsyz, taskSpjjz, taskSpblfy, taskSptsrqyy/* , taskSpywxhzy*/);
                //如果任一线程出错，则回滚
                if(taskSpgg.Result||taskSpsyz.Result||taskSpjjz.Result||taskSpblfy.Result||taskSptsrqyy.Result/*||taskSpywxhzy.Result */)
                {
                    transaction.Rollback();
                    return Json(false);
                }
                else
                {
                    //提交事务
                    transaction.Commit();
                    return Json(true);
                }
            }
        }
        //获取商品列表
        [HttpGet]
        public async Task<IActionResult> fetchSPZSJ(int id) => Json(await spzsjFunc.fetchSpListAsync(id));
        //获取商品标准词条
        public async Task<IActionResult> fetchSPBZCT(int id)
        {
            var medicine = await _context.Medicine.FirstOrDefaultAsync(m => m.MedicineID == id);
            return Json(medicine.bzctID);
        }
        //获取商品子表数据
        public async Task<IActionResult> fetchSPdata(int id)
        {
            var spzsj = await _context.SPZSJ.FirstOrDefaultAsync(m => m.SPBH == id);
            // var spsyz = await _context.SPSYZs.Where(m => m.SPBH == id).ToListAsync();
            // var spjjz = await _context.SPJJZs.Where(m => m.SPBH == id).ToListAsync();
            // var sptsrqyy = await _context.SPTSRQYYs.Where(m => m.SPBH == id).ToListAsync();
            // var spywxhzy = await _context.SPYWXHZYs.Where(m => m.SPBH2 == id || m.SPBH1 == id).ToListAsync();
            // var spblfy = await _context.SPBLFYs.Where(m => m.SPBH == id).ToListAsync();
            // var spgg = await _context.SPGGs.Where(m => m.SPBH == id).ToListAsync();
            var spsyz = await _context.SPSYZs.FirstOrDefaultAsync(m => m.SPBH == id);
            var spjjz = await _context.SPJJZs.FirstOrDefaultAsync(m => m.SPBH == id);
            var sptsrqyy = await _context.SPTSRQYYs.FirstOrDefaultAsync(m => m.SPBH == id);
            var spywxhzy = await _context.SPYWXHZYs.FirstOrDefaultAsync(m => m.SPBH2 == id || m.SPBH1 == id);
            var spblfy = await _context.SPBLFYs.FirstOrDefaultAsync(m => m.SPBH == id);
            var spgg = await _context.SPGGs.FirstOrDefaultAsync(m => m.SPBH == id);
            var data = new
            {
                spzsj,
                spsyz,
                spjjz,
                sptsrqyy,
                spywxhzy,
                spblfy,
                spgg
            };
            // ------------------------------------------------
            return Json(data);
        }
        [HttpPost]
        //修改商品主数据&子表数据
        public async Task<IActionResult> alterSpzsj()
        {
            int medicineID = Convert.ToInt32(Request.Form["medicineID"]);
            var medicine = await _context.Medicine.FirstOrDefaultAsync(m => m.MedicineID == medicineID);
            var spbh = Convert.ToInt32(Request.Form["spbh"]);
            SPZSJ mySp = await _context.SPZSJ.FirstOrDefaultAsync(m => m.SPBH == spbh);
            if (mySp != null)
            {
                mySp.PZWH = Request.Form["PZWH"];
                mySp.JX = Request.Form["JX"];
                mySp.SPM = Request.Form["SPM"];
                mySp.SCDW = Request.Form["SCDW"];
                mySp.XXLY = Request.Form["XXLY"];
                //mySp.isStandard = Request.Form["isStandard"];
                mySp.YWCF = Request.Form["YWCF"];
                mySp.SMSSYZ = Request.Form["SMSSYZ"];
                mySp.SMSJJZ = Request.Form["SMSJJZ"];
                mySp.SMSYFYL = Request.Form["SMSYFYL"];
                mySp.BLFY = Request.Form["BLFY"];
            }
            //如果是标准词条
            if (Request.Form["bzct"] == "true")
            {
                medicine.bzctID = mySp.SPBH;
            }
            else if (Request.Form["bzct"] == "false")
            {
                //先判断原标准词条是否是此词条，
                //如果是，则清空，如果不是，则跳过
                if (medicine.bzctID == spbh)
                {
                    medicine.bzctID = -1;
                }
            }
            //商品规格
            if (Convert.ToBoolean(Request.Form["alterSPGG"]))
            {
                SPGG spgg = await _context.SPGGs.FirstOrDefaultAsync(m => m.SPBH == spbh);
                //如果该商品规格存在，则修改 ，若不存在，则创建
                if (spgg != null)
                {
                    spgg.GGMC = Request.Form["GGMC"];
                    spgg.BZSL = Request.Form["BZSL"];
                    spgg.BZDW = Request.Form["BZDW"];
                }
                else
                {
                    SPGG newSpgg = new SPGG();
                    newSpgg.GGMC = Request.Form["GGMC"];
                    newSpgg.BZSL = Request.Form["BZSL"];
                    newSpgg.BZDW = Request.Form["BZDW"];
                    newSpgg.SPBH = spbh;
                    await _context.SPGGs.AddAsync(newSpgg);
                }
            }
            //保存商品适应症
            if (Convert.ToBoolean(Request.Form["alterSPSYZ"]))
            {
                SPSYZ spsyz = await _context.SPSYZs.FirstOrDefaultAsync(m => m.SPBH == spbh);
                if (spsyz != null)
                {
                    spsyz.SYZMC = Request.Form["SYZMC"];
                }
                else
                {
                    SPSYZ newSpsyz = new SPSYZ();
                    newSpsyz.SYZMC = Request.Form["SYZMC"];
                    newSpsyz.SPBH = spbh;
                    await _context.SPSYZs.AddAsync(newSpsyz);
                }
            }
            //商品禁忌症
            if (Convert.ToBoolean(Request.Form["alterSPJJZ"]))
            {
                SPJJZ spjjz = await _context.SPJJZs.FirstOrDefaultAsync(m => m.SPBH == spbh);
                if (spjjz != null)
                {
                    spjjz.JJZMC = Request.Form["JJZMC"];
                }
                else
                {
                    SPJJZ newSpjjz = new SPJJZ();
                    newSpjjz.JJZMC = Request.Form["JJZMC"];
                    newSpjjz.SPBH = spbh;
                    await _context.SPJJZs.AddAsync(newSpjjz);
                }
            }
            //不良反应
            if (Convert.ToBoolean(Request.Form["alterSPBLFY"]))
            {
                SPBLFY spblfy = await _context.SPBLFYs.FirstOrDefaultAsync(m => m.SPBH == spbh);
                if (spblfy != null)
                {
                    spblfy.BLFYMC = Request.Form["BLFYMC"];
                }
                else
                {
                    SPBLFY newSpblfy = new SPBLFY();
                    newSpblfy.BLFYMC = Request.Form["BLFYMC"];
                    newSpblfy.SPBH = spbh;
                    await _context.SPBLFYs.AddAsync(newSpblfy);
                }
            }
            //特殊人群用药
            if (Convert.ToBoolean(Request.Form["alterSPTSRQYY"]))
            {
                SPTSRQYY sptsrqyy = await _context.SPTSRQYYs.FirstOrDefaultAsync(m => m.SPBH == spbh);
                if (sptsrqyy != null)
                {
                    sptsrqyy.RQLX = Request.Form["RQLX"];
                    sptsrqyy.YYSM = Request.Form["YYSM"];
                }
                else
                {
                    SPTSRQYY newSptsrqyy = new SPTSRQYY();
                    newSptsrqyy.RQLX = Request.Form["RQLX"];
                    newSptsrqyy.YYSM = Request.Form["YYSM"];
                    newSptsrqyy.SPBH = spbh;
                }
            }
            //相互作用
            if (Convert.ToBoolean(Request.Form["alterSPYWXHZY"]))
            {
                SPYWXHZY spywxhfy = new SPYWXHZY();
                spywxhfy.SPBH1 = Convert.ToInt32(Request.Form["SPBH1"]);
                spywxhfy.SPBH2 = Convert.ToInt32(Request.Form["SPBH2"]);
                spywxhfy.ZYXG = Request.Form["ZYXG"];
                await _context.SPYWXHZYs.AddAsync(spywxhfy);
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
        //删除商品--------逻辑删除，删除一个商品要删除其下的子表信息
        public async Task<IActionResult> deleteSp(int id)
        {
            var spzsj = await _context.SPZSJ.FirstOrDefaultAsync(m => m.SPBH == id);
            var spsyz = await _context.SPSYZs.FirstOrDefaultAsync(m => m.SPBH == id);
            var spjjz = await _context.SPJJZs.FirstOrDefaultAsync(m => m.SPBH == id);
            var sptsrqyy = await _context.SPTSRQYYs.FirstOrDefaultAsync(m => m.SPBH == id);
            //var spywxhzy = await _context.SPYWXHZYs.FirstOrDefaultAsync(m => m.SPBH2 == id);
            var spblfy = await _context.SPBLFYs.FirstOrDefaultAsync(m => m.SPBH == id);
            var spgg = await _context.SPGGs.FirstOrDefaultAsync(m => m.SPBH == id);

            if (spzsj != null) _context.SPZSJ.Remove(spzsj);
            if (spsyz != null) _context.SPSYZs.Remove(spsyz);
            if (spjjz != null) _context.SPJJZs.Remove(spjjz);
            if (sptsrqyy != null) _context.SPTSRQYYs.Remove(sptsrqyy);
            if (spblfy != null) _context.SPBLFYs.Remove(spblfy);
            if (spgg != null) _context.SPGGs.Remove(spgg);
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
        //根据条件查询商品
        public async Task<IActionResult> fetchMedicineByNameAndFirm()
        {
            int medicineID = Convert.ToInt32(Request.Form["medicineID"]);
            var medicineName = Request.Form["medicineName"];
            var medicineFirm = Request.Form["medicineFirm"];

            var medicine = await _context.SPZSJ
                .Where(m => m.MedicineID == medicineID)
                .Where(m => m.SPM.Contains(medicineName))
                .Where(m => m.SCDW.Contains(medicineFirm))
                .Select(m => new
                {
                    m.SPM,
                    m.SCDW,
                    m.SPBH
                })
                .ToListAsync();

            return Json(medicine);
        }
    }
}
