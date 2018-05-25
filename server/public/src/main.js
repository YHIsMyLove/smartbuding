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
import Home from './components/layout/Home.vue'
import Main from './components/Main.vue'
import axios from 'axios';

Vue.use(ElementUI)
Vue.use(VueRouter)
Vue.use(Vuex)

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
        iconCls: 'fa fa-id-card-o',//图标样式class
        children: [
            {
                path: '/',
                component: Main,
                name: '主页',
                meta: { redirectAuth: true },
                hidden: true
            },
            {
                path: '/SysFieldManager',
                component: resolve =>
                    require(['./components/business/SysFieldManager.vue'], resolve),
                name: '系统管理',
                meta: { redirectAuth: true },
            },
            {
                path: '/FileManager',
                component: resolve => require(['./components/business/FileManager.vue'], resolve),
                name: '文件管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/YSDeviceManager',
                component: resolve => require(['./components/business/YSDeviceManager.vue'], resolve),
                name: '萤石设备管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/DoorDeviceManager',
                component: resolve => require(['./components/business/DoorDeviceManager.vue'], resolve),
                name: '门禁设备管理',
                meta: { redirectAuth: true }
            },
        ],
        meta: { redirectAuth: true }
    },
    {
        path: '/',
        component: Home,
        name: '用户管理',
        iconCls: 'fa fa-id-card-o',
        children: [
            {
                path: '/UserManager',
                component: resolve => require(['./components/business/UserManager.vue'], resolve),
                name: '用户管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/SystemUserProjManager',
                component: resolve => require(['./components/business/SystemUserProjManager.vue'], resolve),
                name: '用户项目管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/UserProjManager',
                component: resolve => require(['./components/business/UserProjManager.vue'], resolve),
                name: '用户项目管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/UserDeptManager',
                component: resolve => require(['./components/business/UserDeptManager.vue'], resolve),
                name: '用户部门管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/UserRoleManager',
                component: resolve => require(['./components/business/UserRoleManager.vue'], resolve),
                name: '用户角色管理',
                meta: { redirectAuth: true }
            },
        ],
        meta: { redirectAuth: true }
    },
    {
        path: '/',
        component: Home,
        name: '设备管理',
        iconCls: 'fa fa-id-card-o',
        children: [
            {
                path: '/DeviceManager',
                component: resolve => require(['./components/business/DeviceManager.vue'], resolve),
                name: '系统设备管理',
                meta: { redirectAuth: true }
            },
        ],
        meta: { redirectAuth: true },
    },
    {
        path: '/',
        component: Home,
        name: '会议管理',
        iconCls: 'fa fa-id-card-o',
        children: [
            {
                path: '/Meeting',
                component: resolve => require(['./components/business/Meeting.vue'], resolve),
                name: '会议管理',
                meta: { redirectAuth: true }
            },
        ],
        meta: { redirectAuth: true },
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
        var Usersession = sessionStorage.getItem('Session')
        if (store.state.User.SessionID || Usersession) {
            next()
        } else {
            next({ path: '/login' })
        }
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

