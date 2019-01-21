<template>
        <el-menu 
        class="left-menu"
        :collapse="true"
        background-color="#545c64"
        text-color="#fff"
        active-text-color="#ffd04b"
        v-on:select="handleSelect"
        >
            <el-menu-item index="user">
                <i class="el-icon-info"></i>
                <span slot="title">{{currentUser.user_name}}</span>
            </el-menu-item>
            <el-submenu v-if="menu.children" v-for="menu in menuTree" :key="menu.id" :index="menu.url">
                <template slot="title">
                    <i class="el-icon-location"></i>
                    <span v-if="menu.id" slot="title">{{menu.label}}</span>
                </template>
                <sub-left-nav :menuTree="menu.children"></sub-left-nav>
            </el-submenu>
            <el-menu-item v-else-if="!menu.children" index="asdf">
                <i class="el-icon-menu"></i>
                <span slot="title">{{menu.url}}</span>
            </el-menu-item>
            <el-submenu class="settingMenu" index="allSetting">
                <template slot="title" index="setting">
                    <i class="el-icon-setting"></i>
                    <span slot="title">设置</span>
                </template>
                <el-menu-item index="alterPwd">
                    <i class="el-icon-setting"></i>
                    <span slot="title">修改密码</span>
                </el-menu-item>
                <el-menu-item index="logout">
                    <i class="el-icon-setting"></i>
                    <span slot="title">注销</span>
                </el-menu-item>
            </el-submenu>
        </el-menu>
</template>

<script>
import Subpower from '../Class/Subpower';
import subLeftNav from './subLeftNav.vue'

export default {
    props:["myPage"],
    name:"myMenu",
    data() {
        return {
            subPower:Subpower,
            currentUser:{},
            menuTree:[],
        }
    },
    methods: {
        //获取当前用户
        fetchCurrentUser(){
            fetch("/Users/fetchCurrentUser")
            .then((res)=>{
                return res.json()
            }).then((json)=>{
                this.currentUser = json;
            })
        },
        handleSelect(index){
            // 25=>通用名库
            // 27=>用户管理
            // 34=>菜单管理
            // 35=>权限管理
            // 36=>菜单权限维护
            // switch(index){
            //     case 25:
                
            // }
            this.$emit('update:key', index )
        }
        
    },
    created() {
        //获取当前用户
        this.fetchCurrentUser();
        this.subPower = new Subpower();
        //获取用户的权限
        this.subPower.fetchUserPower(()=>{
            this.subPower.parseTree(()=>{
                this.menuTree = this.subPower.powerTree;
            });
        })
    },
    components:{
        subLeftNav
    }
}
</script>

<style>
  .left-menu{
      height: 1000px;
      position: fixed;
      margin-top: 0;
  }
</style>
