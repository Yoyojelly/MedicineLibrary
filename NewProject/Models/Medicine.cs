using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace NewProject.Models
{
    public class Medicine : Table
    {
        //产品ID
        public int MedicineID { get; set; }
        //标准通用名
        public string MedicineName { get; set; }
        //标准词条的商品id
        public int bzctID { get; set; }
        //外键--大类ID
        public int DLID { get; set; }
        [Display(Name = "大类")]
        public TYMD TYMD { get; set; }
        //外键--中类ID
        public int ZLID { get; set; }
        [Display(Name = "中类")]
        public TYMZ TYMZ { get; set; }
        //外键--小类ID
        [Display(Name = "小类")]
        public int XLID { get; set; }
        [Display(Name = "小类")]
        public TYMX TYMX { get; set; }
        //外键--通用名类别
        public int LBID { get; set; }
        [Display(Name = "类别")]
        public TYMLB TYMLB { get; set; }
        //外键--监管类别
        public int jglbID { get; set; }
        [Display(Name = "类别")]
        public JGLB JGLB { get; set; }
        //一个通用名可能有多个商品
        public List<SPZSJ> SPZSJ { get; set; }
    }
  
    ///<summary>
    ///通用名库页面专用数据模型
    ///</summary>
    public struct TymkMedicineData
    {
        public int MedicineID;
        public string MedicineName;
        public int dlid;
        public int zlid;
        public int xlid;
        public string dname;
        public string zname;
        public string xname;
    }
    ///<summary>
    ///商品信息界面专用模型
    ///</summary>
    public struct SpxqMedicineData
    {
        public int MedicineID;
        public string MedicineName;
        public int dlid;
        public int zlid;
        public int xlid;
        public string dname;
        public string zname;
        public string xname;
        public int lbid;
        public string lbname;
        public int jglbid;
        public string jglbname;
    }
    public class MedicineFunc
    {
        #region 字段和构造函数
        private MyDbContext myDbContext;
        #region 药品对象 Medicine
        private Medicine medicine;
        public Medicine Medicine{
            get{
                return this.medicine;
            }
            set{
                this.medicine = value;
            }
        }
        public static List<TYMD> TYMDs;
        #endregion
        #region 药品列表，暂时无用，已注释
        // private static List<Medicine> medicines;
        // public List<Medicine> Medicines{
        //     get
        //     {
        //         return medicines;
        //     }
        //     set
        //     {
        //         medicines = value;
        //     }
        // }
        #endregion
        #region 静态字段
        //通用名列表
        private static List<TymkMedicineData> tymkMedicineData;
        public List<TymkMedicineData> TymkMedicineData{
            get{
                return tymkMedicineData;
            }
            set{
                tymkMedicineData = value;
            }
        }
        //监管类别
        private static List<JGLB> JGLBs;
        //药物类别
        private static List<TYMLB> TYMLBs;
        #endregion
        #region 构造函数
        /// <summary>
        /// 依赖_dbContext
        /// </summary>
        public MedicineFunc(MyDbContext _myDbContext)
        {
            myDbContext = _myDbContext;
        }
        #endregion
        #endregion
       
        #region  方法 
        #endregion
        ///<summary>
        ///传入MedicineID，赋值给medicine.
        ///</summary>
        public async Task findMedicineAsync(int id) => this.medicine = await myDbContext.Medicine.FirstOrDefaultAsync(m=>m.MedicineID == id);
        ///<summary>
        ///给medicines赋值。
        ///</summary>
        // public async Task findMedicinesAsync() => this.Medicines = await myDbContext.Medicine.ToListAsync();
        
        ///</summary>
        ///TymkMedicineData（通用名库专用数据)赋值
        ///</summary>
        public async Task<List<TymkMedicineData>> findTymkMedicineDataAsync()
        {
            if(TymkMedicineData == null)
            {
                TymkMedicineData = await myDbContext.Medicine
                    .Include(m => m.TYMD)
                    .Include(m => m.TYMZ)
                    .Include(m => m.TYMX)
                    .Select(m => new TymkMedicineData
                    {
                        MedicineID = m.MedicineID,
                        MedicineName = m.MedicineName,
                        dlid = m.DLID,
                        dname = m.TYMD.DNAME,
                        zlid = m.ZLID,
                        zname = m.TYMZ.ZNAME,
                        xlid = m.XLID,
                        xname = m.TYMX.XNAME,
                    }).ToListAsync();
                    return TymkMedicineData;
            }
            return TymkMedicineData;
        }
        
        ///<summary>
        ///清空通用名库专用数据列表
        ///</summary>
        public void clearTymkMedicineData() => TymkMedicineData = null;

        ///<summary>
        ///删除商品主数据,返回bool,最后对TymkMedicineData重新复制。
        ///</summary>
        public async Task<bool> deleteMedicineAsync(int id)
        {
            this.medicine = await myDbContext.Medicine.FirstOrDefaultAsync(m=>m.MedicineID == id);
            try{
                myDbContext.Remove(medicine);
                await myDbContext.SaveChangesAsync();
                return true;
            }catch{
                return false;
            }finally{
                //先清空，以防数据还未赋值时有其他用户来获取数据。
                clearTymkMedicineData();
                await findTymkMedicineDataAsync();
            }
        }
 
        ///<summary>
        ///添加词条 
        ///</summary>
        ///<return>0:保存失败，1:保存成功，2:该药品名已存在
        public async Task<int> addMedicineAsync(string MedicineName,int dlid,int zlid,int xlid,int lbid,int jglb)
        {
            //如果该词条已存在
            Medicine medicine1 = await myDbContext.Medicine.FirstOrDefaultAsync(m=>m.MedicineName==MedicineName);
            if(medicine1!=null) return 2;
            //
            medicine = new Medicine();
            medicine.MedicineName = MedicineName;
            medicine.DLID = dlid;
            medicine.ZLID = zlid;
            medicine.XLID = xlid;
            medicine.jglbID = jglb;
            medicine.LBID = lbid;
            try{
                myDbContext.Add(medicine);
                await myDbContext.SaveChangesAsync();
                clearTymkMedicineData();
                return 1;
            }
            catch{
                return 0;
            }
        }
    
        ///<summary>
        ///修改词条
        ///</summary>
        public async Task<bool> alterMedicine(int medicineID,string medicineName,int dlid,int zlid,int xlid,int lbid,int jglbid)
        {
            medicine = await myDbContext.Medicine.FirstOrDefaultAsync(m=>m.MedicineID == medicineID);
            if(medicine==null) return false;
            medicine.MedicineName = medicineName;
            medicine.DLID = dlid;
            medicine.ZLID = zlid;
            medicine.XLID = xlid;
            medicine.LBID = lbid;
            medicine.jglbID = jglbid;

            try
            {
                await myDbContext.SaveChangesAsync();
                clearTymkMedicineData();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        ///<summary>
        ///查询商品信息
        ///</summary>
        public async Task<SpxqMedicineData> fetchSpxqMedicineData(int id)
        {
            //先把数据在数据库里提取出来
            var medicine = await myDbContext.Medicine
                .Include(m => m.TYMD)
                .Include(m => m.TYMZ)
                .Include(m => m.TYMX)
                .Include(m => m.TYMLB)
                .Include(m => m.JGLB)
                .FirstOrDefaultAsync(m=>m.MedicineID == id);
            SpxqMedicineData spxqMedicineData = new SpxqMedicineData();
            //如果不存在该药品，则返回默认值
            if(medicine==null) return spxqMedicineData;

            spxqMedicineData.MedicineID = medicine.MedicineID;
            spxqMedicineData.MedicineName = medicine.MedicineName;
            spxqMedicineData.dlid = medicine.DLID;
            spxqMedicineData.dname = medicine.TYMD.DNAME;
            spxqMedicineData.zlid = medicine.ZLID;
            spxqMedicineData.zname = medicine.TYMZ.ZNAME;
            spxqMedicineData.xlid = medicine.XLID;
            spxqMedicineData.xname = medicine.TYMX.XNAME;
            spxqMedicineData.lbid = medicine.LBID;
            spxqMedicineData.lbname = medicine.TYMLB.LBNAME;
            spxqMedicineData.jglbid = medicine.jglbID;
            spxqMedicineData.jglbname = medicine.JGLB.lbName;

            return spxqMedicineData;
        }
        
        ///<summary>
        ///查询通用名大列表,使用了静态字段。
        ///</summary>
        public async Task<List<TYMD>> fetchTymdListAsync()
        {
            if(TYMDs!=null) return TYMDs;
            TYMDs = await myDbContext.TYMD.ToListAsync();
            return TYMDs;
        }
        ///<summary>
        ///查询通用名中列表,      拟用静态字典重写
        ///</summary>
        public async Task<List<TYMZ>> fetchTymzListAsync(int id) => await myDbContext.TYMZ.Where(m=>m.DLID == id).ToListAsync();
    
        ///<summary>
        ///查询通用名小列表
        ///</summary>
        public async Task<List<TYMX>> fetchTymxListAsync(int id) => await myDbContext.TYMX.Where(m=>m.ZLID==id).ToListAsync();
        
        ///<summary>
        ///查询监管类别,静态。
        ///</summary>
        public async Task<List<JGLB>> fetchJglbAsync()
        {
            if(JGLBs!=null) return JGLBs;
            JGLBs = await myDbContext.JGLBs.ToListAsync();
            return JGLBs;
        }
        
        ///<summary>
        ///查询药物类别,静态。
        ///</summary>
        public async Task<List<TYMLB>> fetchTymlbAsync()
        {
            if(TYMLBs!=null) return TYMLBs;
            TYMLBs = await myDbContext.TYMLBs.ToListAsync();
            return TYMLBs;
        }
        ///<summary>
        ///根据条件查询药物
        ///</summary>
        public async Task<List<TymkMedicineData>> fetchMedicinesAsync(string MedicineName,string dname,string zname,string xname)
        {
            return await myDbContext.Medicine
                .Where(m => m.MedicineName.Contains(MedicineName))
                .Where(m => m.TYMD.DNAME.Contains(dname))
                .Where(m => m.TYMZ.ZNAME.Contains(zname))
                .Where(m => m.TYMX.XNAME.Contains(xname))
                .Select(m => new TymkMedicineData
                {
                    MedicineName = m.MedicineName,
                    MedicineID = m.MedicineID,
                    dlid = m.TYMD.DLID,
                    dname = m.TYMD.DNAME,
                    zlid = m.TYMZ.ZLID,
                    zname = m.TYMZ.ZNAME,
                    xlid = m.TYMX.XLID,
                    xname = m.TYMX.XNAME
                }).ToListAsync();
        }

    }
}
