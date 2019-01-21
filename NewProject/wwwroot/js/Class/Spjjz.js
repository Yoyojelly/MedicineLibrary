//商品禁忌症
export default class Spjjz {
    constructor() {
        //禁忌症ID
        this.jjzbh = "";
        //禁忌症名称
        this.jjzmc = "";
    }
    addData(obj) {
        this.jjzbh = obj.jjzbh;
        this.jjzmc = obj.jjzmc;
    }
}