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
import DeptManager from './components/business/DeptManager.vue'
import SysFieldManager from './components/nav1/SysFieldManager.vue'
import SystemManager from './components/business/SystemManager.vue'


import OrganizationManager from './components/business/OrganizationManager.vue'
import DeviceManager from './components/business/DeviceManager.vue'

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
            { path: '/UserManager', component: UserManager, name: '用户管理' },
            { path: '/DeptManager', component: DeptManager, name: '部门管理' },

            // { path: '/SystemManager', component: SystemManager, name: '系统管理' },
            // { path: '/DeviceManager', component: DeviceManager, name: '设备管理' },
            // { path: '/OrganizationManager', component: OrganizationManager, name: '组织架构管理' },
            //{ path: '/ShowCaseManager', component: ShowCaseManager, name: '案例展示' },
            // { path: '/LeaveMessageManager', component: LeaveMessageManager, name: '留言管理' },
            // { path: '/ActivityManager', component: ActivityManager, name: '活动管理' },
            // { path: '/form', component: Form, name: '表单提交' },
            // { path: '/tabs', component: Tabs, name: '标签页' },
        ]
    },
    // {
    //     path: '/',
    //     component: Home,
    //     name: '系统管理',
    //     iconCls: 'fa fa-id-card-o',
    //     children: [
    //         { path: '/vuex', component: VuexComp, name: 'Vuex' },
    //         { path: '/page5', component: Page5, name: '测试页面' }
    //     ]
    // },
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

