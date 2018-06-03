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

//全局引用图片上传组件
import UploadImage from "./components/businesscontrols/UploadImage.vue";
Vue.component("UploadImage", UploadImage)

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
                hidden: true,
                meta: { redirectAuth: true }
            },
            {
<<<<<<< HEAD
                path: '/MenuLinkElement',
                component: resolve => require(['./components/APIBuilder/MenuLinkElement.vue'], resolve),
                name: '路由表管理',
=======
                path: '/RankingListElement',
                component: resolve => require(['./components/APIBuilder/RankingListElement.vue'], resolve),
                name: '红黑榜管理',
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
            // {
            //     path: '/SystemUserProjManager',
            //     component: resolve => require(['./components/business/SystemUserProjManager.vue'], resolve),
            //     name: '用户项目管理',
            //     meta: { redirectAuth: true }
            // },
            {
                path: '/UserProjManager',
                component: resolve => require(['./components/business/UserProjManager.vue'], resolve),
                name: '用户项目管理',
>>>>>>> 72bef902ba622ae0f489c5b3a7807f336d63243e
                meta: { redirectAuth: true }
            },
            {
                path: '/RankingListElement',
                component: resolve => require(['./components/APIBuilder/RankingListElement.vue'], resolve),
                name: '红黑榜管理',
                hidden: true,
                meta: { redirectAuth: true }
            },
            {
                path: '/MenuAuthElement',
                component: resolve => require(['./components/APIBuilder/MenuAuthElement.vue'], resolve),
                name: '路由权限表管理',
                meta: { redirectAuth: true }
            },
        ],
        meta: { redirectAuth: true }
    },
<<<<<<< HEAD
    // {
    //     path: '/',
    //     component: Home,
    //     name: '用户管理',
    //     iconCls: 'fa fa-id-card-o',
    //     children: [
    //         {
    //             path: '/UserManager',
    //             component: resolve => require(['./components/business/UserManager.vue'], resolve),
    //             name: '用户管理',
    //             hidden: true,
    //             meta: { redirectAuth: true }
    //         },
    //         {
    //             path: '/UserProjManager',
    //             component: resolve => require(['./components/business/UserProjManager.vue'], resolve),
    //             name: '用户项目管理',
    //             meta: { redirectAuth: true }
    //         },
    //         {
    //             path: '/UserDeptManager',
    //             component: resolve => require(['./components/business/UserDeptManager.vue'], resolve),
    //             name: '用户部门管理',
    //             meta: { redirectAuth: true }
    //         },
    //         {
    //             path: '/UserRoleManager',
    //             component: resolve => require(['./components/business/UserRoleManager.vue'], resolve),
    //             name: '用户角色管理',
    //             meta: { redirectAuth: true }
    //         },
    //     ],
    //     meta: { redirectAuth: true }
    // },
    // {
    //     path: '/',
    //     component: Home,
    //     name: '设备管理',
    //     iconCls: 'fa fa-id-card-o',
    //     children: [
    //         {
    //             path: '/DevicesElement',
    //             component: resolve => require(['./components/APIBuilder/DevicesElement.vue'], resolve),
    //             name: '设备管理管理',
    //             meta: { redirectAuth: true }
    //         },
    //         {
    //             path: '/YSDeviceManager',
    //             component: resolve => require(['./components/business/YSDeviceManager.vue'], resolve),
    //             name: '萤石设备管理',
    //             meta: { redirectAuth: true }
    //         },
    //         {
    //             path: '/DoorDeviceManager',
    //             component: resolve => require(['./components/business/DoorDeviceManager.vue'], resolve),
    //             name: '门禁设备管理',
    //             meta: { redirectAuth: true }
    //         },
    //     ],
    //     meta: { redirectAuth: true },
    // },
    // {
    //     path: '/',
    //     component: Home,
    //     name: '会议管理',
    //     iconCls: 'fa fa-id-card-o',
    //     children: [
    //         {
    //             path: '/Meeting',
    //             component: resolve => require(['./components/business/Meeting.vue'], resolve),
    //             name: '会议管理',
    //             meta: { redirectAuth: true }
    //         },
    //     ],
    //     meta: { redirectAuth: true },
    // },
=======
    {
        path: '/',
        component: Home,
        name: '设备管理',
        iconCls: 'fa fa-id-card-o',
        children: [
            {
                path: '/DevicesElement',
                component: resolve => require(['./components/APIBuilder/DevicesElement.vue'], resolve),
                name: '设备管理管理',
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
>>>>>>> 72bef902ba622ae0f489c5b3a7807f336d63243e
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

