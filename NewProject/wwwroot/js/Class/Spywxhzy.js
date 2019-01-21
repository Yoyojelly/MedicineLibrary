//商品药物相互作用
export default class Spywxhzy {
    constructor() {
        //相互作用ID
        this.xhzybh = "";
        //产品ID1
        this.spbh1 = "";
        //产品ID2
        this.spbh2 = "";
        //作用效果
        this.zyxg = "";
    }
    addData(obj) {
        this.xhzybh = obj.xhzybh;
        this.spbh1 = obj.spbh1;
        this.spbh2 = obj.spbh2;
        this.zyxg = obj.zyxg;
    }
}