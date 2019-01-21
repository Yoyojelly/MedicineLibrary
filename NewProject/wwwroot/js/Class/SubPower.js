//权限
export default class SubPower{
    constructor(){
        //权限ID
        // this.subPowerID = Number;
        //权限名称
        // this.subPowerName = String;
        //所属超权限ID
        // this.supPowerID = Number;
        // 权限列表  --在fetchPower中定义赋值
        // this.powers= [];
        //权限树     --在parseTree中定义赋值
        // this.powerTree = [];
        //新增或删除权限fromData
        this.alterPowerFormData = new FormData();
        //改动的索引   --  例如   fromData[`alter${alterIndex}id`]
        this.alterIndex = 0;
        //权限组信息   --  fetchPowerTeam中定义赋值
        //this.powerTeams=[];
        this.powers= [];
    }
    //去后台获取power表的所有数据并赋值给this.power
    //也可以直接获取返回值
    fetchPower(successFunc){
        fetch("https://localhost:5001/Users/fetchSubPowers")
        .then((res)=>{
          return res.json()
        }).then((json)=>{
            this.powers = json;
            successFunc();
        });
    }
    fetchUserPower(successFunc){
        this.powers = [];
        fetch("https://localhost:5001/Users/fetchCurrentPowers")
            .then((res)=>{
                return res.json()
            }).then((json)=>{
                this.powers = json;
                
                successFunc();
            })
    }
    //获得权限组信息,参数是获取数据成功后执行的函数
    fetchPowerTeams(successFunc){
        this.powerTeams = [];
        fetch("https://localhost:5001/Users/fetchPowerTeams")
        .then((res)=>{
            return res.json()
        }).then((json)=>{
            this.powerTeams = json;
            successFunc();
        })
    }
    //把power解析成一个树，赋值给powerTree
    //树的结构：[{id,label,?data,children:[...]},{??}]
    //children在哪个{}内就表示是哪个的子权限
    //power => [{powerLevel,subPowerID,subPowerName,supPowerID}]
    //           权限层级    权限ID      权限名称     所属超权限ID
    parseTree(successFuc){
        this.powerTree = [];
        //在权限表中，一定先有超权限，再有子权限
        //所以一遍循环足以把所有的权限全部加入权限树内
        let id = 0;
        for(let i = 0 ; i < this.powers.length ; i++)
        {
            let data ={
                id:id++,
                subPowerID: this.powers[i].subPowerID,
                label:this.powers[i].subPowerName,
                url:this.powers[i].powerUrl
            }
            //如果权限的超权限是0，则直接压入权限树
            if(this.powers[i].supPowerID == 0){
                ///-------code--------////
                this.powerTree.push(data);
            }
            //如果权限的超权限不是0，则寻找对应的超权限，在它的超权限内压入child
            else{
                this.addChild(this.powerTree,
                          this.powers[i].supPowerID,
                          data);
            }
        }
        successFuc();
    }
    //寻找孩子的递归方法,传入一个数组，检测其有没有child
    //传入需要寻找的id,传入需要添加的对象
    //如果有，则检查
    addChild(arr,id,obj){
        //如果当前对象不是数组，则直接检查赋值
        if(!(arr instanceof Array)){
            if(arr.subPowerID == id){
                if(arr.children)
                {
                    arr.children.push(obj);
                    return true;
                }
                else
                {
                    arr.children = [];
                    arr.children.push(obj);
                    return true;
                }
            }
        }
        else if(arr instanceof Array){
            for(let i = 0;i<arr.length;i++)
            {
                //e=>arr[i]
                //当前对象的id是否等于需要寻找的id
                //如果是则赋值然后返回
                if(arr[i].subPowerID == id){
                    if(arr[i].children)
                    {
                        arr[i].children.push(obj);
                        return true;
                    }
                    else
                    {
                        arr[i].children = [];
                        arr[i].children.push(obj);
                        return true;
                    }
                }
                //寻找是否有孩子
                if(arr[i].children){
                    //如果有孩子则遍历孩子重复以上操作
                    var result = this.addChild(arr[i].children,id,obj);
                    if(result) return true;
                }
            }
            
        }
    }
    //新增权限----插入到newOrDelPower数组，保存需要save方法
    addPower(subPowerName,subPowerID,id){
        this.alterPowerFormData.append(`alter${this.alterIndex}`,"add");
        this.alterPowerFormData.append(`alter${this.alterIndex}Name`,subPowerName);
        this.alterPowerFormData.append(`alter${this.alterIndex}supPowerID`,subPowerID);
        this.alterPowerFormData.append(`alter${this.alterIndex}id`,id);
        this.alterIndex++;
    }
    //删除权限---
    delPower(subPowerID){
        //如果删除的是前面改动的权限，就把这个改动删掉就行了
        //同时也要删除该权限的子权限----还没做
        for(let i = 0 ; i < this.alterIndex ; i++){
            if(this.alterPowerFormData.get(`alter${i}id`)==subPowerID){
                this.alterPowerFormData.delete(`alter${i}`);
                this.alterPowerFormData.delete(`alter${i}Name`);
                this.alterPowerFormData.delete(`alter${i}subPowerID`);
                this.alterPowerFormData.delete(`alter${i}id`);
                return true;
            }   
        }
        //如果上面没有返回，则把删除放到formData里发送到服务器
        this.alterPowerFormData.append(`alter${this.alterIndex}`,"del");
        this.alterPowerFormData.append(`alter${this.alterIndex}subPowerID`,subPowerID);
        this.alterIndex++;
    }
    //上面做的改动，必须要保存才能生效。
    //保存面临一个问题：
    //如果在新增的那一个权限后面新增一个权限
    //此时先新增的权限并没有id
    //那么后新增的权限的超权限就是undefined
    //所以先假设给先新增的权限一个id,例如1000
    //到后台再解析出来保存
    savePower(successFunc){
        this.alterPowerFormData.append("num",this.alterIndex);

        fetch("https://localhost:5001/Users/saveSubPowers",{
            method:"post",
            body:this.alterPowerFormData
        }).then((res)=>{
            return res.json()
        }).then((json)=>{
            if(json){
                successFunc();
            }
        })
    }
    //获取该权限组所拥有的权限
    fetchTeamPowerByPowerTeamId(powerTeamId,successFunc){
        this.TeamPowers = [];
      fetch("https://localhost:5001/Users/fetchTeamPowerByPowerTeamId/"+powerTeamId)
      .then((res)=>{
          return res.json()
      }).then((json)=>{
          this.TeamPowers = json;
          successFunc();
      })
    }
    //保存该权限组的权限变动  
    // this.teamPowers => 原权限组的权限
    //powerTeamID=>该权限组id
    //powerIDs 新权限 数组
    savePowerTeamPower(powerTeamID,powerIDs,successFuc){
        let formData = new FormData();
        
        formData.append("powerTeamID",powerTeamID);
        //需要更改的项数
        let index = 0;
        //原有的powerID
        //检查原有ID的每一项，如果没有出现在新的权限里面，则删除改权限
        //同时在新权限里面删除该项，  循环完毕后再遍历剩余的新权限，全部添加
        let isAlter = false;
        for(let i = 0 ; i < this.TeamPowers.length ; i++){
          for(let n = 0 ; n < powerIDs.length; n++){
            //如果两者相等，说明没有变化，把该项在新权限里面删去
            if(powerIDs[n] == this.TeamPowers[i].subPowerID){
              powerIDs.splice(n,1);
              isAlter=true;
              break;
            }
          }
          //遍历完执行过操作，则前往下一此循环
          if(isAlter){
            isAlter = false;
            continue;
          }
          else{
            //遍历完未执行操作，说明该权限需要删除
            isAlter = false;
            formData.append(`alter${index}`,"del");
            formData.append(`alter${index}id`,this.TeamPowers[i].subPowerID);
            index ++;
            continue;
          }
        }
        for(let i = 0 ; i < powerIDs.length ; i++){
          formData.append(`alter${index}`,"add");
          formData.append(`alter${index}id`,powerIDs[i]);
          index++;
        }
        formData.append("index",index);

        fetch("https://localhost:5001/Users/savePowerTeamPower",{
          method:"post",
          body:formData
        }).then((res)=>{
          return res.json()
        }).then((json)=>{
        this.TeamPowers = json;
          successFuc();
        })
    }
}