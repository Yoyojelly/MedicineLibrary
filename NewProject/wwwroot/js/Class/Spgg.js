//商品规格
export default class Spgg {
    constructor() {
        //药品规格
        this.ggmc = "";
        //包装数量
        this.bzsj = "";
        //包装单位
        this.bzdw = "";
    }
    addData(obj) {
        this.ggmc = obj.ggmc;
        this.bzsj = obj.bzsj;
        this.bzdw = obj.bzdw;
    }
}