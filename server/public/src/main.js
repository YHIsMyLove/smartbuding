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
import Main from './components/Main.vue'
import Home from './components/layout/Home.vue'
import axios from 'axios';

import VCharts from 'v-charts'
Vue.use(VCharts)
Vue.use(ElementUI)
Vue.use(VueRouter)
Vue.use(Vuex)

import echarts from 'echarts'
Vue.prototype.$echarts = echarts

/*************************************************************************** */
/*************************************************************************** */
/*************************************************************************** */
//全局引用图片上传组件
import UploadImage from "./components/businesscontrols/UploadImage.vue";
Vue.component("UploadImage", UploadImage)
//全局引用选择Icon组件
import SelectIcon from "./components/businesscontrols/SelectIcon.vue";
Vue.component("SelectIcon", SelectIcon)
//全局引用选择路由组件
import SelectMenuLink from "./components/businesscontrols/SelectMenuLink.vue";
Vue.component("SelectMenuLink", SelectMenuLink)
//全局引用选择用户组件
import SelectUser from "./components/businesscontrols/SelectUser.vue";
Vue.component("SelectUser", SelectUser)
//全局引用选择用户角色
import SelectRole from "./components/businesscontrols/SelectRole.vue";
Vue.component("SelectRole", SelectRole)
/*************************************************************************** */
/*************************************************************************** */
/*************************************************************************** */

var routes = [
    {
        path: '/login',
        component: Login,
        hidden: true
    },
    {
        path: '/',
        component: Home,
        name: '系统管理',
        iconCls: 'iconfont icon-home',//图标样式class
        children: [
            {
                path: '/',
                component: Main,
                name: '主页',
                // iconCls: 'iconfont icon-home',
                meta: { redirectAuth: true },
            },
            {
                path: '/SysFieldManager',
                component: resolve =>
                    require(['./components/business/SysFieldManager.vue'], resolve),
                name: '系统管理',
                meta: { redirectAuth: true },
                hidden: true
            },
            // {
            //     path: '/FileManager',
            //     component: resolve => require(['./components/business/FileManager.vue'], resolve),
            //     name: '文件管理',
            //     hidden: true,
            //     meta: { redirectAuth: true },
            //     hidden: true
            // },
            {
                path: '/MenuLinkElement',
                component: resolve => require(['./components/APIBuilder/MenuLinkElement.vue'], resolve),
                name: '路由表管理',
                meta: { redirectAuth: true },
                hidden: true
            },
            // {
            //     path: '/RankingListElement',
            //     component: resolve => require(['./components/APIBuilder/RankingListElement.vue'], resolve),
            //     name: '红黑榜管理',
            //     hidden: true,
            //     meta: { redirectAuth: true },
            //     hidden: true
            // },
            {
                path: '/MenuAuthElement',
                component: resolve => require(['./components/APIBuilder/MenuAuthElement.vue'], resolve),
                name: '路由权限表管理',
                meta: { redirectAuth: true },
                hidden: true
            },
        ],
        meta: { redirectAuth: true }
    },
    {
        path: '/',
        component: Home,
        name: '用户管理',
        iconCls: 'iconfont icon-user',
        children: [
            {
                path: '/UserManager',
                component: resolve => require(['./components/business/UserManager.vue'], resolve),
                name: '用户管理',
                hidden: true,
                meta: { redirectAuth: true },
                hidden: true
            },
            {
                path: '/UserProjManager',
                component: resolve => require(['./components/business/UserProjManager.vue'], resolve),
                name: '用户项目管理',
                meta: { redirectAuth: true },
                hidden: true
            },
            {
                path: '/UserDeptManager',
                component: resolve => require(['./components/business/UserDeptManager.vue'], resolve),
                name: '用户部门管理',
                meta: { redirectAuth: true },
                hidden: true
            },
            {
                path: '/UserRoleManager',
                component: resolve => require(['./components/business/UserRoleManager.vue'], resolve),
                name: '用户角色管理',
                meta: { redirectAuth: true },
                hidden: true
            },
        ],
        meta: { redirectAuth: true },
        hidden: true
    },
    {
        path: '/',
        component: Home,
        name: '设备管理',
        iconCls: 'iconfont icon-setting',
        children: [
            {
                path: '/DevicesElement',
                component: resolve => require(['./components/APIBuilder/DevicesElement.vue'], resolve),
                name: '设备管理管理',
                meta: { redirectAuth: true },
                hidden: true
            },
            {
                path: '/YSDeviceManager',
                component: resolve => require(['./components/business/YSDeviceManager.vue'], resolve),
                name: '萤石设备管理',
                meta: { redirectAuth: true },
                hidden: true
            },
            {
                path: '/DoorDeviceManager',
                component: resolve => require(['./components/business/DoorDeviceManager.vue'], resolve),
                name: '门禁设备管理',
                meta: { redirectAuth: true },
                hidden: true
            },
        ],
        meta: { redirectAuth: true },
        hidden: true
    },
    {
        path: '/',
        component: Home,
        name: '会议管理',
        iconCls: 'iconfont icon-message',
        children: [
            {
                path: '/Meeting',
                component: resolve => require(['./components/business/Meeting.vue'], resolve),
                name: '会议管理',
                meta: { redirectAuth: true },
                hidden: true
            },
        ],
        meta: { redirectAuth: true },
        hidden: true
    },
    {
        path: '*',
        redirect: '/',
        hidden: true
    }
]

const router = new VueRouter({
    routes
})

//store
router.beforeEach((to, from, next) => {
    if (to.meta.redirectAuth) {
        NProgress.start();
        if (store.state.User.UserID) {
            next()
        } else {
            next({ path: '/login' })
        }
        // if (!store.state.Proj.ProjID) {
        //     next({ path: '/' })
        // } else {
        //     next()
        // }
    } else {
        NProgress.start();
        next()
    }
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

