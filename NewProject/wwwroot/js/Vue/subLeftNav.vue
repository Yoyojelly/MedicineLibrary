<template>
    <div>
            <el-submenu v-if="menu.children" v-for="menu in menuTree" :key="menu.id" :index="menu.subPowerID.toString()">
                <template slot="title">
                    <i class="el-icon-location"></i>
                    <span v-if="menu.id" slot="title">{{menu.label}}</span>
                </template>
                <my-self :menuTree="menu.children"></my-self>
            </el-submenu>
            <el-menu-item v-else-if="!menu.children" :index="menu.url">
                <i class="el-icon-menu"></i>
                <span slot="title">{{menu.label}}</span>
            </el-menu-item>
    </div>
</template>

<script>
export default {
    name:"mySelf",
    props:["menuTree"]
}
</script>

<style>

</style>
