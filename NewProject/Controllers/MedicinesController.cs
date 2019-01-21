using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Cors;

namespace NewProject.Controllers
{
    [EnableCors("cors")]
    public class MedicinesController : Controller
    {
        private readonly MyDbContext _context;
        private readonly MedicineFunc medicineFunc;

        public MedicinesController(MyDbContext context)
        {
            _context = context;
            medicineFunc = new MedicineFunc(_context);
        }
        
        //取通用名库主数据
        public async Task<IActionResult> fetchMedicines() => Json(await medicineFunc.findTymkMedicineDataAsync());
        //保存词条
        [HttpPost]
        public async Task<IActionResult> addTym()=>
            Json(
                await medicineFunc.addMedicineAsync(            /*返回0：失败，1：成功，2：已存在。 */
                    MedicineName:Request.Form["MedicineName"],  /*传递药品名 */
                    dlid:Convert.ToInt32(Request.Form["tymd"]), /*传递通用名大ID */
                    zlid:Convert.ToInt32(Request.Form["tymz"]), /*传递通用名中ID */
                    xlid:Convert.ToInt32(Request.Form["tymx"]), /*传递通用名小ID */
                    lbid:Convert.ToInt32(Request.Form["ywlb"]), /*传递通用名药物类别ID */
                    jglb:Convert.ToInt32(Request.Form["jglb"])  /*传递通用名监管类别ID */
                )
            );
        [HttpPost]
        //修改词条
        public async Task<IActionResult> alterMedicine()=>
            Json(
                await medicineFunc.alterMedicine(
                    medicineID:Convert.ToInt32(Request.Form["medicineID"]),
                    medicineName:Request.Form["medicineName"].ToString(),
                    dlid:Convert.ToInt32(Request.Form["tymd"]),
                    zlid:Convert.ToInt32(Request.Form["tymz"]),
                    xlid:Convert.ToInt32(Request.Form["tymx"]),
                    lbid:Convert.ToInt32(Request.Form["ywlb"]),
                    jglbid:Convert.ToInt32(Request.Form["jglb"])
                )
            );
        
        //获取商品详情信息
        [HttpPost]
        public async Task<IActionResult> fetchMedicineById() => Json(await  medicineFunc.fetchSpxqMedicineData(Convert.ToInt32(Request.Form["medicineID"])));
        //post获取大中小通用名，和通用名类别监管类别
        [HttpPost]
        public async Task<IActionResult> fetchTymd() => Json(await medicineFunc.fetchTymdListAsync());
        //查询通用名中列表
        [HttpGet]
        public async Task<IActionResult> fetchTymz(int id) => Json(await medicineFunc.fetchTymzListAsync(id));
        //查询通用名小列表
        [HttpGet]
        public async Task<IActionResult> fetchTymx(int id) => Json(await medicineFunc.fetchTymxListAsync(id));
        //查询监管类别列表
        [HttpPost]
        public async Task<IActionResult> fetchJglb() => Json(await medicineFunc.fetchJglbAsync());
        //查询通用名类别列表
        [HttpPost]
        public async Task<IActionResult> fetchTymlb() => Json(await medicineFunc.fetchTymlbAsync());

        [HttpPost]
        //根据条件查询药物
        public async Task<IActionResult> cxMedicines() =>
            Json( await medicineFunc.fetchMedicinesAsync(
                MedicineName:Request.Form["MedicineName"], /*传入通用名   */
                dname: Request.Form["tymd"],               /*传入通用名大 */
                xname: Request.Form["tymz"],               /*传入通用名中 */
                zname: Request.Form["tymx"]                /*传入通用名小 */
            ));
        [HttpGet]
        //删除指定的药物
        public async Task<IActionResult> deleteMedicine(int id) => Json(await medicineFunc.deleteMedicineAsync(id));

    }
}
