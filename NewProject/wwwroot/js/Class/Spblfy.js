//商品不良反应
export default class Spblfy {
    constructor() {
        //不良反应ID
        this.blfybh = "";
        //不良反应名称
        this.blfymc = "";
    }
    addData(obj) {
        this.blfybh = obj.blfybh;
        this.blfymc = obj.blfymc;
    }
}