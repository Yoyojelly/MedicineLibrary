//商品主数据
export default class Spzsj {
    //
    //    这里用"" 而不用""
    //    如果用户输入了数据又删除了，此时的数据是""
    //    有助于判断是否需要存储该表
    //
    constructor() {
        //商品编号
        this.spbh = "";
        //批准文号
        this.pzwh = "";
        //原始品名
        this.yspm = "";
        //标准通用名称
        this.tym = "";
        //剂型
        this.jx = "";
        //商品名
        this.spm = "";
        //生产单位
        this.scdw = "";
        //监管类别
        this.jglb = "";
        //药物类别
        this.ywlb = "";
        //说明书适应症
        this.smssyz = "";
        //说明书用法用量
        this.smsyfyl = "";
        //服药与进食
        this.fyyjs = "";
        //说明书禁忌症
        this.smssjz = "";
        //不良反应
        this.blfy = "";
        //药物相互作用
        this.ywxhzy = "";
        //信息来源
        this.xxly = "";
        //药物成分
        this.ywcf = "";
    }

    //后台获取的json数据直接赋值
    addData(obj) {
        //json.spzsj
        this.spbh = obj.spbh;
        this.pzwh = obj.pzwh;
        this.jx = obj.jx;
        this.spm = obj.spm;
        this.scdw = obj.scdw;
        this.xxly = obj.xxly;
        this.ywcf = obj.ywcf;
        this.smssyz = obj.smssyz;
        this.smsyfyl = obj.smsyfyl;
        //this.fyyjs = obj.fyyjs;
        this.smsjjz = obj.smsjjz;
        this.blfy = obj.blfy;
        this.ywxhzy = obj.ywxhzy;
        this.tsrqyy = obj.tsrqyy;
    }
}