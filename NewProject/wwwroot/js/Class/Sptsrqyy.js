//商品特殊人群用药
export default class Sptsrqyy {
    constructor() {
        //人群类型
        this.rqlx = "";
        //用药说明
        this.yysm = "";
    }
    addData(obj) {
        this.rqlx = obj.rqlx;
        this.yysm = obj.yysm;
    }
}