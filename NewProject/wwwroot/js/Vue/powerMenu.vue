<template>
  <div>
    <div>
      <el-button type="primary" @click="refrechData()">
        更新
      </el-button>
    </div>
      <el-collapse accordion v-model="activeNames" @change="handleChange">
          <el-collapse-item 
            v-for="powerTeam in powerTeams"
            v-bind:key="powerTeam.powerTeamID"
            :title="powerTeam.powerTeamName"
            :name="powerTeam.powerTeamID">
              <div>
                  <powerTree :allowAddNewPower="false" :powerTeamID="powerTeam.powerTeamID"></powerTree>
              </div>
          </el-collapse-item>
      </el-collapse>
  </div>
</template>



<script>
import Subpower from '../Class/Subpower';
import powerTree from './powerTree.vue';

export default {
  data() {
    return {
        activeNames: ['1'],
        powerTeams:[],
        subPower:Subpower
    }
  },
  methods: {
       handleChange() {

      },
      addPowerTeams(){
          this.powerTeams = this.Subpower.powerTeams;
      },
      refrechData(){
        this.Subpower = new Subpower();
        this.Subpower.fetchPowerTeams(this.addPowerTeams);
      }
  },
  created() {
      this.refrechData();
  },
  components:{
      powerTree
  }
}
</script>

<style>
  .demo-table-expand {
    font-size: 0;
  }
  .demo-table-expand label {
    width: 90px;
    color: #99a9bf;
  }
  .demo-table-expand .el-form-item {
    margin-right: 0;
    margin-bottom: 0;
    width: 50%;
  }
</style>