//商品适应症
export default class Spsyz {
    constructor() {
        //适应症ID
        this.syzbh = "";
        //标准适应症名称
        this.syzmc = "";
    }
    addData(obj) {
        this.syzbh = obj.syzbh;
        this.syzmc = obj.syzmc;
    }
}
