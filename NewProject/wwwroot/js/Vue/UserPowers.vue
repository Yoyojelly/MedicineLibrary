<template>
    <div>
        <el-transfer
            style="text-align:left; display:inline-block"
            v-model="powerTeams"
            :titles="['其他权限','拥有权限']"
            :props="{
                key:'powerTeamID',
                label:'powerTeamName'
                }"
            :button-texts="['移除','添加']"
            :format="{
                noChecked:'${total}',
                hasChecked:'${checked}/${total}'
                }"
             @change="$emit('put-power',powerTeams)"
             :data="data">
            <span slot-scope="{ option }">{{ option.powerTeamID }} - {{ option.powerTeamName }}</span>
        </el-transfer>
    </div>
</template>

<script>
import SubPower from '../Class/Subpower';

export default {
    props:["currentUser"],
    data() {
        return {
            data:[],
            subPower:new SubPower(),
            powerTeams:[]
        }
        },
    methods: {
        fetchUserPowersByUserId(){
            fetch("/Users/fetchUserPowersByUserId/"+this.currentUser)
            .then((res)=>{
                return res.json()
            }).then((json)=>{
                let arr = [];
                for(let i = 0 ; i < json.length ; i ++){
                    arr.push(json[i].powerTeamID);
                }
                this.powerTeams = arr;
            })
        }
    },
    watch: {
        currentUser:function(){
            this.fetchUserPowersByUserId();
        }
    },
    created() {
        this.subPower.fetchPowerTeams(()=>{
            this.data = this.subPower.powerTeams;
        })
        if(this.currentUser!=""){
            this.fetchUserPowersByUserId();
        }
    },
}
</script>

<style scoped>

</style>
