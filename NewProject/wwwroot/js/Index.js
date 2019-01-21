import Spzsj from './Class/Spzsj';
import Spgg from './Class/Spgg';
import Spsyz from './Class/Spsyz';
import Spjjz from './Class/Spjjz';
import Spblfy from './Class/Spblfy';
import Spywxhzy from './Class/Spywxhzy';
import Sptsrqyy from './Class/Sptsrqyy';
import ElementUI, {Message} from 'element-ui';
import Vue from 'vue';
import powerTree from './vue/powerTree.vue';
import powerMenu from './vue/powerMenu.vue';
import powerTeam from './vue/powerTeam.vue';
import UserPowers from './vue/UserPowers.vue';
import LeftNav from './vue/LeftNav.vue';


Vue.use(ElementUI);
//左侧导航栏
var leftNav = new Vue({
    el:"#leftNav",
    data:{
        key:0,
    },
    methods:{

    },
    watch:{
        key:function(){
            // 25=>通用名库
            // 27=>用户管理
            // 34=>菜单管理
            // 35=>权限管理
            // 36=>菜单权限维护
            switch(this.key){
                case "tymk":
                    addAndToTab('tymk', '通用名库');
                    tymk.active = true;
                    break;
                case "yhgl":
                    addAndToTab('yhgl', '用户管理');
                    yhgl.active = true;
                    break;
                case "cdgl":
                    addAndToTab('cdgl', '菜单管理');
                    cdgl.active = true;
                    break;
                case "qxzgl":
                    addAndToTab('qxzgl', '权限组管理');
                    qxzgl.active = true;
                    break;
                case "qxcd":
                    addAndToTab('qxcd', '权限菜单管理');
                    qxcd.active = true; 
                    break;
                case "alterPwd":
                    xgpwd.dialogVisible = true;
                    break;
                case "logout":
                    window.location.href="/Home/Logout";
                    break;
                default:
                    Message.error("导航出错。");
                    break;
            }
        }
    },
    components:{
        LeftNav
    }
})
//头部导航栏
var topNav = new Vue({
    el: '#topnav',
    data: {
        navs: []
    },
    methods: {
        dbClickCutTab: (nav) => {
            let index = topNav.navs.indexOf(nav);
            topNav.navs.splice(index,1);
            let toTab = topNav.navs[index] || topNav.navs[index-1];
            if(toTab != null){
                let id = toTab.href;
                document.querySelector(id+"tab").click();
            }else{
                document.querySelector("#indextab").click();
            }
            // console.log(topNav.navs);
            // cutTab(id, "indextab");
        }
    }
});
//主页面
var index = new Vue({
    el: '#index',
    data: {
    }
});
//通用名库页面
var tymk = new Vue({
    el: '#tymk',
    data: {
        tymlist: [],
        medicineName : "",
        tymd : "",
        tymz : "",
        tymx: "",
        currentTym:"",
        active:false
    },
    watch: {
        medicineName: function (newMedicineName, oldMedicineName ) {
            this.debounceGetMedicines()
        },
        tymd: function (newMedicineName, oldMedicineName) {
            this.debounceGetMedicines()
        },
        tymz: function (newMedicineName, oldMedicineName) {
            this.debounceGetMedicines()
        },
        tymx: function (newMedicineName, oldMedicineName) {
            this.debounceGetMedicines()
        }
    },
    created: function () {
        //直到用户输入完成后才执行 getMedicines方法,用来查询相关药品
        this.debounceGetMedicines = _.debounce(this.getMedicines, 1000);
    },
    methods: {
        //点击修改词条的编辑时触发的事件
        toXgct: function (id) {
            xgct.active = true;
            addAndToTab("xgct", "修改词条");
            //把数据保存到FormData对象中，再进行数据传输
            let formData = new FormData();
            formData.append("medicineID", id);
            //获取该药品的所有相关属性
            fetch(`/Medicines/fetchMedicineById`, {
                method: "post",
                body: formData
            }).then((res) => {
                return res.json();
            }).then((json) => {
                xgct.medicine = json;
            });
            getTymkData(xgct);
        },
        getMedicines: function () {
            let formData = new FormData();
            formData.append("MedicineName", this.medicineName);
            formData.append("tymd", this.tymd);
            formData.append("tymz", this.tymz);
            formData.append("tymx", this.tymx);
            //获取对应的药物信息
            fetch("/medicines/cxMedicines", {
                method: "post",
                body: formData
            }).then((res) => {
                return res.json()
            }).then((json) => {
                tymk.tymlist = json;
            });
        },
        tymTrClick: function (tym) {
            this.currentTym = tym;
        },
        deleteMedicine: function () {
            if (this.currentTym != "") {
                fetch("/Medicines/deleteMedicine/" + this.currentTym)
                    .then((res) => {
                        return res.json()
                    }).then((json) => {
                        if (json == true) {
                            refreshTymk(
                                ()=>{
                                    Message.success("删除成功。");
                                }
                            );
                        } else if (json == false){
                        Message.error("删除失败。");
                    }
                    });
            } else {
                Message.error("请选择要删除的药品。");
            }
        },
        ////点击编辑时发生的跳往商品信息页面
        toSpxx: function (id) {
            spxx.active = true;
            addAndToTab("spxx", "商品信息");
            spxx.medicineID = id;
            let formData = new FormData();
            formData.append("MedicineID", id);
            fetch(`/Medicines/fetchMedicineById`, {
                method: "post",
                body: formData
            }).then((res) => {
                return res.json();
                }).then((json) => {
                spxx.medicine = json;
                });
            fetchSPZSJ(id);
            fetchSPBZCT(id);
        }
    }
});
//新增词条页面
var xzct = new Vue({
    el: '#xzct',
    data: {
        tymds: [],
        tymzs: [],
        tymxs: [],
        jglbs: [],
        ywlbs: [],
        active: false
    }
});
//修改词条页面
var xgct = new Vue({
    el: '#xgct',
    data: {
        medicine: {},
        tymds: [],
        tymzs: [],
        tymxs: [],
        jglbs: [],
        ywlbs: [],
        active: false
    }
});
//商品信息页面
var spxx = new Vue({
    el: "#spxx",
    data: {
        active: false,
        cxmedicineName: "",
        cxmedicineFirm: "",
        medicine: {},
        sps: [],
        bzct: "",
        currentSp:""
    },
    watch: {
        cxmedicineName: function (newMedicineName, oldMedicineName) {
            this.debounceGetSps()
        },
        cxmedicineFirm: function (newMedicineName, oldMedicineName) {
            this.debounceGetSps()
        },
    },
    methods: {
        toAddProductTab: function () {
            tjspxq.active = true;
            clearTjspxq();
            tjspxq.medicineID = this.medicineID;
            addAndToTab("tjspxq", "添加商品详情");
        },
        alterSpxq: function (spbh) {
            tjspxq.active = true;
            tjspxq.isTjNoAlter = false;
            tjspxq.medicineID = this.medicine.medicineID;
            fetchSPdata(spbh);
            addAndToTab("tjspxq", "添加商品详情");
        },
        //删除某个商品
        deleteSp: function () {
            fetch("/SPZSJs/deleteSp/" + this.currentSp)
                .then((res) => {
                    return res.json()
                }).then((json) => {
                    if (json == true) {
                        refreshSpxx();
                        this.$message.success("已成功删除商品。");
                    }
                    else {
                        this.$message.error("删除失败。");
                    }
                    
                });
        },
        getSps: function () {
            let formData = new FormData();
            formData.append("medicineID", this.medicine.medicineID);
            formData.append("medicineName", this.cxmedicineName);
            formData.append("medicineFirm", this.cxmedicineFirm);
            fetch("/SPZSJs/fetchMedicineByNameAndFirm", {
                method: "post",
                body: formData
            }).then((res) => {
                return res.json()
            }).then((json) => {
                this.sps = json;
            });
        }
    },
    created: function () {
        //直到用户输入完成后才执行 getMedicines方法
        this.debounceGetSps = _.debounce(this.getSps, 1000);
    },
});
//添加商品详情
var tjspxq = new Vue({
    el: "#tjspxq",
    data: {
        //添加还是更新，true是添加，false是更新
        isTjNoAlter: true,
        active: false,
        medicineID:"",
        //商品主数据
        SPZSJ:new Spzsj(),
        //是否看过该页面，主数据默认显示，所以默认看过
        isSPZSJ: true,
        //是否保存商品主数据表，具体算法在alterSpxqData方法内执行
        alterSPZSJ: false,
        //药品规格
        SPGG: new Spgg(),
        isSPGG: false,
        alterSPGG: false,
        //适应症
        SPSYZ: new Spsyz(),
        isSPSYZ: false,
        alterSPSYZ: false,
        //禁忌症
        SPJJZ: new Spjjz(),
        isSPJJZ: false,
        alterSPJJZ: false,
        //不良反应
        SPBLFY: new Spblfy(),
        isSPBLFY: false,
        alterSPBLFY: false,
        //药物相互作用
        SPYWXHZY: new Spywxhzy(),
        isSPYWXHZY: false,
        alterSPYWXHZY: false,
        //特殊人群用药
        SPTSRQYY: new Sptsrqyy(),
        isSPTSRQYY: false,
        alterSPTSRQYY: false,
        //用法用量
        isYFYL:false,
        //是否标准词条
        isStandard: false,
        //是否检查通过
        isCheck: false,
        formData:new FormData()
        //医保信息
        //SPYBXX: {
        //    GUID: "",
        //    //医保ID
        //    YBBH: "",
        //    //省
        //    SHENG: "",
        //    //城市
        //    SHI: "",
        //    //产品ID
        //    SPBH: "",
        //    //医保状态
        //    YBZT: "",
        //    //医保类别
        //    YBLB: "",
        //    //信息来源
        //    XXLY:""
        //},
        //isSPYBXX: false,
    },
    methods: {
        //检查是否全部查看过，并存储一下哪些表存储了数据
        checkState: function (state) {
            if (this.isSPZSJ && this.isSPGG && this.isSPSYZ && this.isSPBLFY && this.isSPJJZ
                && this.isSPYWXHZY && this.isSPTSRQYY && this.isYFYL) {
                    this.isCheck = true;
            } else {
                this.isCheck = false;
            }
        },
        saveSpxq: function () {
            //检查哪些商品详情表需要保存
            alterSpxqData();
            if (this.isCheck) {
                //--------商品主数据
                saveSpzsj();
                //--------药品规格
                saveSpgg();
                //--------适应症
                saveSpsyz();
                //--------禁忌症
                saveJjzmc();
                //--------不良反应
                saveBlfy();
                //--------相互作用
                saveXhzy();
                //--------特殊人群用药
                saveTsrqyy();
                switch (this.isTjNoAlter) {
                    //添加
                    case true:
                        fetch("/SPZSJs/saveSpzsj", {
                            method: "post",
                            body: this.formData
                        }).then((res) => {
                            return res.json()
                        }).then((json) => {
                            if (json == true) {
                                cutTab("tjspxqtab", "spxxtab");
                                clearTjspxq();
                                refreshSpxx();
                            }
                        });
                        break;
                    //修改
                    case false:
                        fetch("/SPZSJs/alterSpzsj", {
                            method: "post",
                            body: this.formData
                        }).then((res) => {
                            return res.json()
                            }).then((json) => {
                            if (json == true) {
                                cutTab("tjspxqtab", "spxxtab");
                                clearTjspxq();
                                refreshSpxx();
                            }
                        });
                        break;
                }
               
            }
            else {
                this.$message.error("没有确认状态。");
            }
        }
    }
})
//用户管理页面
var yhgl = new Vue({
    el: '#yhgl',
    data: {
        active: false,
        users: [],
        currentUser: "",
        currentPower: "",
        powers:[]
    },
    watch: {
        active: function () {
            fetchUsers();
        },
        currentUser: function () {
            fetchPowers();
        }
    },
    methods: {
        userClick: function (id, powerid) {
            this.currentUser = id;
            this.currentPower = powerid;
        },
        addNewYh: function () {
            xzyh.active = true;
            clearAddYh();
            addAndToTab("xzyh", "新增用户");
            xzyh.isAddNotAlter = true;

        },
        alterYh: function () {
            xzyh.active = true;
            addAndToTab("xzyh", "新增用户");
            xzyh.isAddNotAlter = false;
            xzyh.currentUser = this.currentUser;
            fetchUserById(this.currentUser);
            
        },
        deleteYh: function () {
            if (this.currentUser) {
                fetch("/Users/deleteUser/" + this.currentUser)
                    .then((res) => {
                        return res.json();
                    }).then((json) => {
                        if (json == true) {
                            this.$message.success("删除成功。");
                            fetchUsers();
                        }
                    });
            }
        }
    }
});
//新增用户
var xzyh = new Vue({
    el: "#xzyh",
    data: {
        active: false,
        isAddNotAlter: true,
        currentUser: "",
        currentPowerTeamName: "",
        isDisable: false,
        user: {
            user_code: "",
            user_name: "",
            zzjg_code: "",
            user_explain: "",
            isDisable: 0
        },
        powerTeams:[]
    },
    watch: {
        user: function () {
            if (this.user.isDisable == 0) {
                this.isDisable = false;
            }
            else {
                this.isDisable = true;
            }
            //
            for (let i = 0; i < this.powers.length; i++) {
                if (this.powers[i].powerID == this.user.powerID) {
                    this.currentPowerTeamName = this.powers[i].powerTeamName;
                }
            }
        }
    },
    methods: {
        saveUser: function () {
            //如果是添加
            if (this.isAddNotAlter) {
                let formData = new FormData();
                formData.append("user_code", this.user.user_code);
                formData.append("user_name", this.user.user_name);
                formData.append("zzjg_code", this.user.zzjg_code);
                formData.append("user_explain", this.user.user_explain);
                let index = 0;
                for(let i = 0 ; i < this.powerTeams.length;i++){
                    formData.append(`powerTeam${i}`, this.powerTeams[i]);
                    index++;
                }
                formData.append("index",index);
                //formData.append("isDisable", this.user.isDisable);
                //值为0不禁用，值为1禁用
                if (this.isDisable == true) {
                    formData.append("isDisable", 1);
                }
                else {
                    formData.append("isDisable", 0);
                }
                //获取选中的powerID
                fetch("/Users/addUser", {
                    method: "post",
                    body: formData
                }).then((res) => {
                    return res.json()
                }).then((json) => {
                    if (json == true) {
                        fetchUsers();
                        cutTab("xzyhtab", "yhgltab");
                        clearAddYh();
                        this.powerTeams = [];
                        this.UserPowers = new UserPowers();
                    }
                });
            }
            //如果是修改
            else {
                let formData = new FormData();
                formData.append("userID", this.currentUser);
                formData.append("user_code", this.user.user_code);
                formData.append("user_name", this.user.user_name);
                formData.append("zzjg_code", this.user.zzjg_code);
                formData.append("user_explain", this.user.user_explain);
                //formData.append("isDisable", this.user.isDisable);
                //值为0不禁用，值为1禁用
                if (this.isDisable == true) {
                    formData.append("isDisable", 1);
                }
                else {
                    formData.append("isDisable", 0);
                }
                //权限信息
                let index = 0;
                for(let i = 0 ; i < this.powerTeams.length;i++){
                    formData.append(`powerTeam${i}`, this.powerTeams[i]);
                    index++;
                }
                formData.append(`index`, index);
                fetch("/Users/alterUser", {
                    method: "post",
                    body: formData
                }).then((res) => {
                    return res.json()
                }).then((json) => {
                    if (json == true) {
                        fetchUsers();
                        cutTab("xzyhtab", "yhgltab");
                        clearAddYh();
                        // this.UserPowers = new UserPowers();
                    }
                });
            }
        },
        refreshUser: function () {

        },
        resetPwd: function () {

        },
        func(arr){
            this.powerTeams=arr;
        }
    },
    components:{
        UserPowers
    }
})
//权限管理页面
var qxzgl = new Vue({
    el: '#qxzgl',
    data:{
        active:false,
    },
    components:{
        powerTeam
    }
});
//菜单管理页面
window.cdgl = new Vue({
    el:"#cdgl",
    data:{
        active:false,
    },
    components:{
        powerTree
    }
})
// 菜单权限管理
var qxcd = new Vue({
    el:"#qxcd",
    data:{
        active:false
    },
    components:{
        powerMenu
    }
})
//修改密码
var xgpwd = new Vue({
    el:"#xgpwd",
    data:{
        form:{
            oldPwd:"",
            newPwd:"",
            againNewPwd:""
        },
        dialogVisible:false
    },
    methods:{
        hanldeClose(done) {
            this.$confirm('确认关闭？')
                .then(() => {
                    done();
                })
                .catch(() =>{});
        },
        submit(){
            if(this.form.oldPwd==""){
                this.$message.error("必须输入原密码");
            }else if(this.form.newPwd=="" || this.form.againNewPwd==""){
                this.$message.error("必须输入新密码");
            }else if(this.form.newPwd!=this.form.againNewPwd){
                this.$message.error("两次输入新密码必须相同");
            }else{
                var formData = new FormData();
                formData.append("oldPwd",this.form.oldPwd);
                formData.append("newPwd",this.form.newPwd);
                fetch("/Home/toAlterPwd", {
                    method: "post",
                    body: formData
                }).then( (res) => {
                    return res.json();
                    }).then( (json) => {
                        if (json) {
                            this.$message("修改成功");
                            this.dialogVisible = false;
                        } else {
                            this.$message.error("修改失败，可能是原密码错误。");
                        }
                });
            }
        },
        concel(){
            this.dialogVisible = false;
        }
    }
})
//通用名大中小应该先选大类，再选中类，再选小类
//  ---取中类数据   tymdSelected();
//  ---取小类数据   tymxSelected();
window.getTymkData = function(obj) {
    //通用名大类
    fetch("/Medicines/fetchTymd", {
        method: "post"
    }).then(function (res) {
        return res.json()
    }).then(function (json) {
        obj.tymds = json;
        return;
        });
    //药物类别
    fetch("/Medicines/fetchTymlb", {
        method: "post"
    }).then(function (res) {
        return res.json()
    }).then(function (json) {
        obj.ywlbs = json;
        return;
        });
    //监管类别
    fetch("/Medicines/fetchJglb", {
        method: "post"
    }).then(function (res) {
        return res.json()
    }).then(function (json) {
        obj.jglbs = json;
        return;
    });
}
//通用名大被选中时，取回中类的数据
window.tymdSelected = function(pageName) {
    //获取大类的id
    //页面传过来一个参数，如果是修改词条页面则传xgct,如果是新增则传'xzct'
    switch (pageName) {
        case 'xgct':
            var dlid = document.querySelector(`#xgctForm`).elements["tymd"].value;
            break;
        case 'xzct':
            dlid = document.querySelector(`#xzctForm`).elements["tymd"].value;
            break;
        default:
            Message.error("发生未知错误。");
            return;
    }
        ////通用名中类
    fetch(`/Medicines/fetchTymz/${dlid}`, {
        method:"get"
    }).then(function (res) {
        return res.json()
    }).then(function (json) {
        if(pageName == 'xgct'){
            xgct.tymzs = json;
        }else{
            xzct.tymzs = json;
        }
        return;
    }); 
}
//通用名中被选中时，取回小类的数据
window.tymxSelected = function(pageName) {
    //获取中类的id
    switch (pageName) {
        case 'xgct':
            var zlid = document.querySelector(`#xgctForm`).elements["tymz"].value;
            break;
        case 'xzct':
            zlid = document.querySelector(`#xzctForm`).elements["tymz"].value;
            break;
        default:
            Message("发生未知错误。");
            return;
    }
    ////通用名中类
    fetch(`/Medicines/fetchTymx/${zlid}`, {
        method: "get"
    }).then(function (res) {
        return res.json()
    }).then(function (json) {
        if(pageName == 'xgct'){
            xgct.tymxs = json;
        }else{
            xzct.tymxs = json;
        }
        return;
    });
}
//添加且导航到该标签
function addAndToTab(tabName, tabText) {
    //如果数组内没有该项，则添加
    if (arrIsNotHave(topNav.navs, { id: `${tabName}tab`, href: `#${tabName}`, text: tabText })) {
        topNav.navs.push({ id:`${tabName}tab`, href: `#${tabName}`, text: tabText });
    }
    setTimeout(() => {
        //$("#xzcttab").click();
        document.querySelector(`#${tabName}tab`).click();
    }, 10);
}
//判断一个数组内是否不含有某个项,第一个参数是数组，第二个参数是项
function arrIsNotHave(arr, item) {
    for (let i = 0; i <= arr.length; i++) {
        //如果是{msg = ""}格式，会无法判断相等，这时候都转化为json字符串，可以判断
        //猜测是因为{}这样声明是一个对象，所以无法判断
        if (arr[i] == item || JSON.stringify(arr[i]) == JSON.stringify(item)) {
            return false;
        }
    }
    return true;
}
//新增按钮的事件，添加新增词条页面
window.addNew = function() {
    if (arrIsNotHave(topNav.navs, {id:"xzcttab" , href: "#xzct", text: "新增词条" })) {
        topNav.navs.push({ id: "xzcttab", href: "#xzct", text: "新增词条" });
        xzct.active = true;
        getTymkData(xzct);
    }
    setTimeout(() => {
        //$("#xzcttab").click();
        document.querySelector("#xzcttab").click();
    }, 10);
}
//保存词条
window.saveMedicine = function() {
    let form = document.querySelector("#xzctForm"),
        formData = new FormData(form);
    fetch("/Medicines/addTym", {
        method: "post",
        body: formData
    }).then((res) => {
        return res.json();
    }).then((json) => {
        //保存成功
        if(json==1){
            getMedicineData();
            form.reset();
            refreshTymk(
                ()=>{
                    Message.success("已成功添加词条。");
                }
            );
            cutTab("xzcttab","tymktab");
        }
        //保存失败
        else if(json==0){
            Message.error("保存失败！");
        }
        //词条已存在
        else if(json==2){
            Message.warning("词条已存在！");
        }
    });
}
//获取药品主数据
function getMedicineData() {
    fetch("/Medicines/fetchMedicines", {
        method: "post"
    }).then(function (res) {
        return res.json()
    }).then(function (json) {
        tymk.tymlist = json;
        return;
    });
}
//修改密码
window.cgPwd = function(event) {
    var newPwd = document.querySelector("#newPwd").value,
        newPwdAgain = document.querySelector("#newPwdAgain").value;
    let form = document.querySelector("#alterPwd"),
        formData = new FormData(form);
    if (newPwd != newPwdAgain) {
        Message.error("两次输入密码不一致。");
    } else {
        fetch("/Home/toAlterPwd", {
            method: "post",
            body: formData
        }).then(function (res) {
            return res.json();
            }).then(function (json) {
                if (json == "0") {
                    Message.success("修改密码成功");
                } else {
                    Message.error("修改密码失败");
                }
        });
    }
}
//通用名库刷新
window.refreshTymk = function(successFunc) {
    getMedicineData();
    tymk.currentTym = "";
    if(successFunc){
        successFunc();
    }else{
        Message("通用名库已刷新。");
    }
}
//剪掉头部导航栏的某个标签并导航到另一个标签
function cutTab(DeleteTabId,toTabId) {
    let navs = topNav.navs;
    
    for (let i = 0; i < navs.length; i++) {
        if (navs[i].id == DeleteTabId) {
            navs.splice(i, 1);
        }
    }
    document.querySelector(`#${toTabId}`).click();
}
//快捷菜单通用名库点击事件
window.tymkClick = function(){
    tymk.active = true;
    addAndToTab("tymk", "通用名库");
}
//修改词条页面的保存事件
window.xgctSave = function() {
    let form = document.querySelector("#xgctForm");
    let formData = new FormData(form);
    
    //检查新词条和旧词条是否相同
    if (formData.get("medicineName") == xgct.medicine.medicineName &&
        formData.get("tymd") == xgct.medicine.dlid &&
        formData.get("tymz") == xgct.medicine.zlid &&
        formData.get("tymx") == xgct.medicine.xlid &&
        formData.get("ywlb") == xgct.medicine.lbid &&
        formData.get("jglb") == xgct.medicine.jglbid) {
        Message("没有发生改动。");
    } else {
        formData.append("medicineID", xgct.medicine.medicineID);
        fetch("/medicines/alterMedicine", {
            method: "post",
            body: formData
        }).then((res) => {
            return res.json();
        }).then((json) => {
            if (json) {
                cutTab("xgcttab", "tymktab");
                refreshTymk(
                    ()=>{
                        Message.success("词条已成功修改。");
                    }
                );
            } else{
                Message.error("词条修改失败。");
            }
        });
    }
}
//获取用户列表
window.fetchUsers = function() {
    fetch("/Home/fetchUsers")
        .then((res) => {
            return res.json()
        }).then((json) => {
            yhgl.users = json;
        });
}
//获取权限列表
function fetchPowers() {
    var id = yhgl.currentUser;
    fetch("/Home/fetchPowers/"+id)
        .then((res) => {
            return res.json()
        }).then((json) => {
            yhgl.powers = json;
        });
}
//保存商品主数据
function saveSpzsj() {
    tjspxq.formData.append("PZWH", tjspxq.SPZSJ.pzwh);
    tjspxq.formData.append("JX", tjspxq.SPZSJ.jx);
    tjspxq.formData.append("SPM", tjspxq.SPZSJ.spm);
    tjspxq.formData.append("SCDW", tjspxq.SPZSJ.scdw);
    tjspxq.formData.append("XXLY", tjspxq.SPZSJ.xxly);
    tjspxq.formData.append("isStandard", tjspxq.SPZSJ.isStandard);
    tjspxq.formData.append("YWCF", tjspxq.SPZSJ.ywcf);
    tjspxq.formData.append("SMSSYZ", tjspxq.SPZSJ.smssyz);
    tjspxq.formData.append("SMSJJZ", tjspxq.SPZSJ.smsjjz);
    tjspxq.formData.append("SMSYFYL", tjspxq.SPZSJ.smsyfyl);
    tjspxq.formData.append("BLFY", tjspxq.SPZSJ.blfy);
    tjspxq.formData.append("medicineID", tjspxq.medicineID);
    tjspxq.formData.append("alterSpzsj", true);
    tjspxq.formData.append("bzct", tjspxq.isStandard);
}
//保存商品规格
function saveSpgg() {
    //---------药品规格
    //药品规格，包装数量，包装单位
    if (tjspxq.SPGG.ggmc != "") tjspxq.formData.append("GGMC", tjspxq.SPGG.ggmc);
    if (tjspxq.SPGG.bzsl != "") tjspxq.formData.append("BZSL", tjspxq.SPGG.bzsl);
    if (tjspxq.SPGG.bzdw != "") tjspxq.formData.append("BZDW", tjspxq.SPGG.bzdw);
    //if (tjspxq.SPGG != "" || tjspxq.BZSL != "" || tjspxq.BZDW != "") tjspxq.alterSPGG = true;
    if (tjspxq.SPGG.ggmc != "" || tjspxq.SPGG.bzsl != "" || tjspxq.SPGG.bzdw != "")
        tjspxq.formData.append("alterSPGG", true);
}
//保存商品适应症
function saveSpsyz() {
    if (tjspxq.SPSYZ.SYZMC != "") {
        tjspxq.formData.append("SYZMC", tjspxq.SPSYZ.syzmc);
        //tjspxq.alterSPSYZ = true;
        tjspxq.formData.append("alterSPSYZ", true);
    }
}
//保存商品禁忌症
function saveJjzmc() {
    if (tjspxq.SPJJZ.JJZMC != "") {
        tjspxq.formData.append("JJZMC", tjspxq.SPJJZ.jjzmc);
        tjspxq.formData.append("alterSPJJZ", true);
    }
}
//保存商品不良反应
function saveBlfy() {
    if (tjspxq.SPBLFY.BLFYMC != "") {
        tjspxq.formData.append("BLFYMC", tjspxq.SPBLFY.blfymc);
        tjspxq.formData.append("alterSPBLFY", true);
    }
}
//保存商品相互作用
function saveXhzy() {
    if (tjspxq.SPYWXHZY.SPBH1 != "") tjspxq.formData.append("SPBH1", tjspxq.SPYWXHZY.spbh1);
    if (tjspxq.SPYWXHZY.SPBH2 != "") tjspxq.formData.append("SPBH2", tjspxq.SPYWXHZY.spbh2)
    if (tjspxq.SPYWXHZY.ZYXG != "") tjspxq.formData.append("ZYXG", tjspxq.SPYWXHZY.zyxg);
    tjspxq.formData.append("alterSPYWXHZY", tjspxq.alterSPYWXHZY);
}
//保存商品特殊人群用药
function saveTsrqyy() {
    if (tjspxq.SPTSRQYY.RQLX != "") tjspxq.formData.append("RQLX", tjspxq.SPTSRQYY.rqlx)
    if (tjspxq.SPTSRQYY.YYSM != "") tjspxq.formData.append("YYSM", tjspxq.SPTSRQYY.yysm);
    if (tjspxq.SPTSRQYY.YYSM != "" || tjspxq.SPTSRQYY.RQLX != "")
        //tjspxq.alterSPTSRQYY = true;
        tjspxq.formData.append("alterSPTSRQYY", true);
}
//获取商品主数据
function fetchSPZSJ(id) {
    fetch("/SPZSJs/fetchSPZSJ/"+id)
        .then((res) => {
            return res.json()
        }).then((json) => {
            spxx.sps = json;
        });
}
//获取商品标准词条
function fetchSPBZCT(id) {
    fetch("/SPZSJs/fetchSPBZCT/" + id)
        .then((res) => {
            return res.json()
        }).then((json) => {
            spxx.bzct = json;
        });
}
//检查哪些商品详情表需要保存
function alterSpxqData() {
    //药品规格
    if (tjspxq.SPGG.GGMC != null ||
        tjspxq.SPGG.BZSL != null ||
        tjspxq.SPGG.BZDW != null) {
        tjspxq.alterSPGG = true;
    }
    //商品适应症
    if (tjspxq.SPSYZ.SYZBH != null) {
        tjspxq.alterSPSYZ = true;
    }
    //商品禁忌症
    if (tjspxq.SPJJZ.JJZMC != null) {
        tjspxq.alterSPJJZ = true;
    }
    //不良反应
    if (tjspxq.SPJJZ.JJZMC != null) {
        tjspxq.alterSPJJZ = true;
    }
    //药物相互作用---如果两者都不为空则存储
    if (tjspxq.SPYWXHZY.spbh1 != "" && tjspxq.SPYWXHZY.spbh2 != "") {
        if (tjspxq.SPYWXHZY.spbh1 == tjspxq.SPZSJ.spbh ||
            tjspxq.SPYWXHZY.spbh2 == tjspxq.SPZSJ.spbh) {
            tjspxq.alterSPYWXHZY = true;
        } else {
            Message.error("相互作用保存失败：产品ID必须有一个和当前商品相同。");
        }
    }
}
//清空商品信息
function clearTjspxq() {
    tjspxq.isTjNoAlter = true;
    tjspxq.medicineID = "";
    //商品主数据
    tjspxq.SPZSJ = new Spzsj();
    //是否看过该页面，主数据默认显示，所以默认看过
    tjspxq.isSPZSJ = true,
    //是否保存商品主数据表，具体算法在alterSpxqData方法内执行
    tjspxq.alterSPZSJ = false;
    //药品规格
    tjspxq.SPGG = new Spgg();
    tjspxq.isSPGG = false;
    tjspxq.alterSPGG= false;
    //适应症
    tjspxq.SPSYZ = [];
    tjspxq.isSPSYZ= false;
    tjspxq.alterSPSYZ = false;
    //禁忌症
    tjspxq.SPJJZ = new Spjjz();
    tjspxq.isSPJJZ= false;
    tjspxq.alterSPJJZ= false;
    //不良反应
    tjspxq.SPBLFY = new Spblfy();
    tjspxq.isSPBLFY= false;
    tjspxq.alterSPBLFY= false;
    //药物相互作用
    tjspxq.SPYWXHZY = new Spywxhzy();
    tjspxq.isSPYWXHZY = false,
    tjspxq.alterSPYWXHZY = false,
    //特殊人群用药
    tjspxq.SPTSRQYY = new Sptsrqyy();
    tjspxq.isSPTSRQYY = false;
    tjspxq.alterSPTSRQYY = false;
    //用法用量
    tjspxq.isYFYL= false;
    //是否标准词条
    tjspxq.isStandard= false;
    //是否检查通过
    tjspxq.isCheck= false;
    tjspxq.formData= new FormData();
}
//清空添加用户信息
function clearAddYh() {
    xzyh.isAddNotAlter = true;
        xzyh.currentUser = "";
        xzyh.currentPowerTeamName = "";
        xzyh.isDisable = false;
        xzyh.user = {
            user_code: "",
            user_name: "",
            zzjg_code: "",
            user_explain: "",
            isDisable: 0,
            powerID: ""
        };
        xzyh.powers = {};
}
//点击商品编辑获取商品子表数据并显示
function fetchSPdata(spbh) {
    fetch("/SPZSJs/fetchSPdata/" + spbh)
        .then((res) => {
            return res.json();
        }).then((json) => {
            clearTjspxq();
            tjspxq.isTjNoAlter = false;
            tjspxq.formData.append("spbh", spbh);
            if (json.spzsj != null) {
                tjspxq.medicineID = json.spzsj.medicineID;
                tjspxq.SPZSJ = json.spzsj;
            }
            if (json.spgg != null) tjspxq.SPGG = json.spgg;
            if (json.spjjz != null) tjspxq.SPJJZ = json.spjjz;
            if (json.spblfy != null) tjspxq.SPBLFY = json.spblfy;
            if (json.spywxhzy != null) tjspxq.SPYWXHZY = json.spywxhzy;
            if (json.sptsrqyy != null) tjspxq.SPTSRQYY = json.sptsrqyy;
            if (json.spsyz != null) tjspxq.SPSYZ = json.spsyz;
            if (json.spywxhzy != null) tjspxq.SPYWXHZY = json.spywxhzy;
        });
}
//刷新商品详情页面
window.refreshSpxx = function() {
    
    var id = spxx.medicine.medicineID;
    let formData = new FormData();
    formData.append("MedicineID", id);
    fetch(`/Medicines/fetchMedicineById`, {
        method: "post",
        body: formData
    }).then((res) => {
        return res.json();
    }).then((json) => {
        console.log(json);
        spxx.medicine = json;
    });
    fetchSPZSJ(id);
    fetchSPBZCT(id);
}
//获取用户
function fetchUserById(id) {
    fetch("/Users/fetchUserById/" + id)
        .then((res) => {
            return res.json()
        }).then((json) => {
            xzyh.user = json;
        });
}
//获取所有的权限管理的列表
function fetchAllPowers() {
    fetch("/Users/fetchAllPowers")
        .then((res) => {
            return res.json()
        }).then((json) => {
            qxzgl.powers = json;
        });
}
getMedicineData();