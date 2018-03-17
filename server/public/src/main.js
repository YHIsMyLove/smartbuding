import Vue from 'vue'
import App from './App.vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-default/index.css'
import VueRouter from 'vue-router'
import store from './vuex/store'
import Vuex from 'vuex'
import NProgress from 'nprogress'//页面顶部进度条
import 'nprogress/nprogress.css'

import Login from './components/Login.vue'
import Home from './components/Home.vue'
import Main from './components/Main.vue'
import UserManager from './components/business/UserManager.vue'
import UserDeptManager from './components/business/UserDeptManager.vue'
import UserProjManager from './components/business/UserProjManager.vue'
import UserRoleManager from './components/business/UserRoleManager.vue'
import SysFieldManager from './components/nav1/SysFieldManager.vue'
import SystemManager from './components/business/SystemManager.vue'
import DeviceRoleManager from './components/business/DeviceRoleManager.vue'
import DeviceManager from './components/business/DeviceManager.vue'
import SystemRoleManager from './components/business/SystemRoleManager.vue'

Vue.use(ElementUI)
Vue.use(VueRouter)
Vue.use(Vuex)

const routes = [
    {
        path: '/login',
        component: Login,
        hidden: true
    },
    {
        path: '/',
        component: Home,
        name: '系统管理',
        iconCls: 'fa fa-id-card-o',//图标样式class
        children: [
            { path: '/', component: Main, name: '主页' },
            { path: '/SysFieldManager', component: SysFieldManager, name: '系统管理' },
        ]
    },
    {
        path: '/',
        component: Home,
        name: '用户管理',
        iconCls: 'fa fa-id-card-o',
        children: [
            { path: '/UserManager', component: UserManager, name: '用户管理' },
            { path: '/UserProjManager', component: UserProjManager, name: '用户项目管理' },
            { path: '/UserDeptManager', component: UserDeptManager, name: '用户部门管理' },
            { path: '/UserRoleManager', component: UserRoleManager, name: '用户角色管理' },
            // { path: '/vuex', component: VuexComp, name: 'Vuex' }
        ]
    },
    {
        path: '/',
        component: Home,
        name: '设备管理',
        iconCls: 'fa fa-id-card-o',
        children: [
            { path: '/DeviceManager', component: DeviceManager, name: '设备管理' },
        ]
    },
    {
        path: '/',
        component: Home,
        name: '权限管理',
        iconCls: 'fa fa-id-card-o',
        children: [
            { path: '/DeviceRoleManager', component: DeviceRoleManager, name: '设备权限管理' },
            { path: '/SystemRoleManager', component: SystemRoleManager, name: '系统权限管理' },
        ]
    },
    // {
    //     path: '/',
    //     component: Home,
    //     name: '导航三',
    //     iconCls: 'fa fa-address-card',
    //     leaf: true,
    //     children: [
    //         { path: '/page6', component: Page6, name: '导航三' }
    //     ]
    // },
    // {
    //     path: '/',
    //     component: Home,
    //     name: 'Charts',
    //     iconCls: 'fa fa-bar-chart',
    //     children: [
    //         { path: '/echarts', component: echarts, name: 'echarts' }
    //     ]
    // },
    {
        path: '*',
        redirect: '/',
        hidden: true
    }
]

const router = new VueRouter({
    routes
})

router.beforeEach((to, from, next) => {
    NProgress.start();
    next()
})

router.afterEach(transition => {
    NProgress.done();
});

new Vue({
    el: '#app',
    template: '<App/>',
    router,
    store,
    components: { App }
}).$mount('#app')

