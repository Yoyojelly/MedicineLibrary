<template>
  <div class="powerteams">
    <div>
        <el-button type="primary" @click="()=>saveChanges()">保存</el-button>
        <el-button type="primary" @click="()=>refreachData()">刷新</el-button>
        <el-button type="danger" @click="()=>delPowerTeam()">删除</el-button>
    </div>
    <table>
        <tr style="width:300px">
            <td style="width:30px;">
            </td>
            <td style="width:200px">
                权限组名称
            </td>
            <td style="width:200px">
                权限组代码
            </td>
        </tr>
        <tr v-for="team in powerTeams"
            :class="{Selected:currentTeam == team.powerTeamID}"
            @click="()=>teamClick(team)"
            :key="team.powerTeamID">
            <!-- 数据库获取出来的权限组数据在这里显示 -->
            <td>
                {{powerTeams.indexOf(team)}}
            </td>
            <td>
                <input style="width:200px" v-if="team.powerTeamID == currentTeam" @blur="alterBlur(team)" v-model="team.powerTeamName" />
                <div v-else>{{team.powerTeamName}}</div>
            </td>
            <td>
                <input style="width:200px" v-if="team.powerTeamID == currentTeam" v-model="team.powerTeamID" disabled />
               <div v-else>{{team.powerTeamID}}</div>
            </td>
        </tr>
        <!-- 这里是新增的权限组数据 -->
        <tr v-for="Team in newTeams" :key="Team.id" @click="()=>newTeamClick(Team.powerTeamID)">
            <td style="width:200px;text-align:center">
                {{Team.powerTeamID}}
            </td>
            <td>
                <input style="width:200px" v-if="currentTeam == 'new'+Team.powerTeamID"
                       v-model="Team.powerTeamName">
                <div v-else>{{Team.powerTeamName}}</div>
            </td>
            <td>
                <input style="width:200px" v-if="currentTeam == 'new'+Team.powerTeamID" disabled>
            </td>
        </tr>
        <!-- 点击新增按钮 -->
        <tr @click="()=>teamClick('new')">
            <td>
                <div v-if="newTeam.is">new</div>
            </td>
            <td>
                <input v-if="newTeam.is" @blur="newTeamBlur()" v-model="newTeam.powerTeamName">
                <div v-else>点击新增</div>
            </td>
            <td>
                <input v-if="newTeam.is" disabled>
            </td>
        </tr>
    </table>
  </div>
</template>

<script>
import SubPower from '../Class/Subpower'
export default {
    data() {
        return {
            subPower:SubPower,
            currentTeam:Number,
            powerTeams:Array,
            alterTeams:[],
            newTeams:[],
            newTeam:{
                is:false,
                powerTeamID:0,
                powerTeamName:""
            },
            delTeams:[]
        }
    },
    methods: {
        newTeamBlur(){
            if(this.newTeam.powerTeamName!=""){
                let data = {
                     powerTeamName : this.newTeam.powerTeamName,
                     powerTeamID : this.newTeam.powerTeamID,
                }
                this.newTeams.push(data);
            }
            this.newTeam.is=false;
            this.newTeam.powerTeamID++;
            this.newTeam.powerTeamName = "";
        },
        //原有权限组的点击事件
        teamClick(team){
            // 如果点击的不是新增按钮
            if(team!='new'){
               this.newTeam.is=false;
               this.currentTeam = team.powerTeamID;
            }
            //如果点击的是新增按钮
            if(team=='new'){
                this.newTeam.is=true,
                this.currentTeam = -1;
            }
        },
        //修改权限组
        alterBlur(team){
            this.currentTeam = -1;
            this.alterTeams.push(team);
        },
        //保存更改
        saveChanges(){
            let index = 0 ;
            let formData = new FormData();
            for(let i = 0 ; i < this.alterTeams.length ; i++){
                formData.append(`alter${index}`,"alter");
                formData.append(`alter${index}name`,this.alterTeams[i].powerTeamName);
                formData.append(`alter${index}id`,this.alterTeams[i].powerTeamID);
                index++;
            }
            for(let i = 0 ; i < this.newTeams.length ; i++){
                formData.append(`alter${index}`,"add");
                formData.append(`alter${index}name`,this.newTeams[i].powerTeamName);
                index++;
            }
            for(let i = 0 ; i < this.delTeams.length ; i++){
                formData.append(`alter${index}`,"del");
                formData.append(`alter${index}id`,this.delTeams[i]);
                index++;
            }
            formData.append(`alterIndex`,index);
            fetch("https://localhost:5001/Users/savePowerTeams",{
                method:"post",
                body:formData
            }).then((res)=>{
                return res.json()
            }).then((json)=>{
                if(json) this.refreachData();
            })
        },
        newTeamClick(id){
            this.currentTeam = 'new'+id;
        },
        delPowerTeam(){
            //如果不是数字  说明是新增的权限
            if(isNaN(this.currentTeam)){
                for(let i = 0 ; i < this.newTeams.length ; i++){
                    if(this.currentTeam == 'new'+this.newTeams[i].powerTeamID){
                        this.newTeams.splice(i,1);
                    }
                }
            }
            //如果是,说明不是新加的权限,将其加入待删除数组中，再从显示列表删除
            else{
                this.delTeams.push(this.currentTeam);
                for(let i = 0 ; i < this.powerTeams.length ; i++){
                    if(this.powerTeams[i].powerTeamID == this.currentTeam){
                        this.powerTeams.splice(i,1);
                        break;
                    }
                }
            }
        },
        refreachData(){
            this.subPower = new SubPower();
            this.subPower.fetchPowerTeams(()=>{
                this.powerTeams = this.subPower.powerTeams;
            });
            this.currentTeam = -1;
            this.alterTeams=[];
            this.newTeams=[];
            this.newTeam={
                is:false,
                powerTeamID:0,
                powerTeamName:""
            };
            this.delTeams=[];
        }
    },
    created() {
        this.refreachData();
    },
}
</script>

<style scoped>
    td{
        border: 1px black solid;
    }
    .Selected{
        background: yellow;
    }
</style>



