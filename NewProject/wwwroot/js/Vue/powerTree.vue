<template>
  <div class="custom-tree-container">
    <div v-if="allowAddNewPower">
      <el-button @click="()=>refrechData()">更新</el-button>
    </div>
    <div class="block">
      <el-tree
        :data="powerTree"
        :show-checkbox="!allowAddNewPower"
        node-key="subPowerID"
        ref="tree"
        default-expand-all
        :expand-on-click-node="false">
        <span class="custom-tree-node" slot-scope="{ node, data }">
          <span>{{ node.label }}</span>
          <span v-if="allowAddNewPower">
            <el-button
              type="text"
              size="mini"
              @click="() => append(data)">
              Append
            </el-button>
            <el-button
              type="text"
              size="mini"
              @click="() => remove(node, data)">
              Delete
            </el-button>
          </span>
        </span>
      </el-tree>
    </div>
    <el-button v-if="allowAddNewPower||allowAddNewPower==undefined" type="text" size="mini" @click="()=>saveNewPower()">
      保存
    </el-button>
    <el-button v-else type="text" size="mini" @click="()=>savePowerTeamPower()">
      保存
    </el-button>
  </div>
</template>

<script>
  import SubPower from '../Class/Subpower';
  import * as signalR from '@aspnet/signalr';

  // let subPower = new SubPower();

  // let id = 1000;

  export default {
    props:["allowAddNewPower","powerTeamID"],
    data() {
      return {
        subPower: SubPower,
        id:1000,
        powerTree: [],
        TeamPower: [],
        //用来连接signalR
        connection:{}
      }
    },

    methods: {
      //添加权限
      append(data) {
        //先弹框，输入需要新增的权限名称
        this.$prompt("请输入权限名称","名称",{
          confirmButtonText:"确定",
          cancleButtonText:"取消",
        }).then(({value})=>{
          this.$message({
            type:'success',
            message:`新增权限：${value},若不保存此改动将会丢失`
          });
          //先将改动在页面显示
          const newChild = { id: this.id++, label: value, children: [] };
          if (!data.children) {
            this.$set(data, 'children', []);
          }
          data.children.push(newChild);
          //将改动保存到subPower对象内，以便后续保存
          this.subPower.addPower(value,data.subPowerID||data.id,newChild.id);
        }).catch(()=>{
          this.$message({
            type:'info',
            message:"取消输入"
          });
        });
      },
      //移除权限
      remove(node, data) {
        const parent = node.parent;
        const children = parent.data.children || parent.data;
        const index = children.findIndex(d => d.id === data.id);
        children.splice(index, 1);
        this.subPower.delPower(data.subPowerID);
      },
      saveNewPower(){
        this.subPower.savePower(this.refrechData);
        
        this.connection.invoke("alterMessage").catch((err)=>console.log(err));
      },
      //获取权限树
      fetData(){
        this.subPower.parseTree(()=>{});
        this.powerTree = JSON.parse(JSON.stringify(this.subPower.powerTree));
        this.$message({
        type:'success',
        message:`获取数据成功。`
        });
        if(!this.allowAddNewPower)
        if(this.TeamPower!=[]){
          this.renderSelect();
        }
      },
      //获取了权限组的权限数据之后，渲染选择框
      parseTeamPower(){
        let arr = [];
        for(let i = 0 ; i < this.subPower.TeamPowers.length ; i++){
          arr.push(this.subPower.TeamPowers[i].subPowerID);
        }
        this.TeamPower = arr;
        if(!this.allowAddNewPower)
        if(this.powerTree!=[]){
          this.renderSelect();
        }
      },
      //保存该权限组所拥有的权限
      savePowerTeamPower(){
        //需要存储的powerID
        let powerIDs = this.$refs.tree.getCheckedKeys();
        
        this.subPower.savePowerTeamPower(this.powerTeamID,powerIDs,this.parseTeamPower).then();
      },
      //渲染选择框
      //bug:当渲染的一个节点有子节点时，其子节点全部选中
      // --- 修复：  只渲染最终的子节点
      //  ---  如果其超权限不是0，则放弃渲染该项
      renderSelect(){
        //先遍历所有权限的数组
        for(let i = 0 ; i < this.subPower.powers.length ; i++){
          //在每一项中遍历拥有的权限的每一项
          for(let n = 0 ; n < this.TeamPower.length ; n++ ){
            //如果该权限有子权限
            //即 该权限 this.TeamPower[n] == 超权限 this.subPower.powers[i].supPowerID
            //删除该权限
            if(this.TeamPower[n] == this.subPower.powers[i].supPowerID)
            this.TeamPower.splice(n,1);
          }
        }
        //
        this.$refs.tree.setCheckedKeys(this.TeamPower);
      },
      refrechData(){
        this.subPower = new SubPower();
        this.subPower.fetchPower(this.fetData);
      }
    },
    created() {
      // if(this.allowAddNewPower == undefined){
      //   this.allowAddNewPower = true;
      // }
        this.refrechData();
        //如果是权限组维护界面，需要显示该权限组的权限
        if(!this.allowAddNewPower){
          this.subPower.fetchTeamPowerByPowerTeamId(this.powerTeamID,this.parseTeamPower);
        }
        //连接signalR
        this.connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

        this.connection.on("alterPowerTree",()=>{
          this.$notify({
            title:'有新的改动',
            message:"请更新权限树。"
          });
        });
         this.connection.start()
        //  .then(()=>{
        //    this.connection.invoke("AddToGroup","myGroup");
        //  })
         .catch((err)=>console.log(err));
    },
  };
</script>