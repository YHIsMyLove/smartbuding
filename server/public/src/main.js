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
import YSDeviceManager from './components/business/YSDeviceManager.vue'
import SystemRoleManager from './components/business/SystemRoleManager.vue'
import Meeting from './components/business/Meeting.vue'
import FileManager from './components/business/FileManager.vue'
import MeetingMinutesManager from './components/business/MeetingMinutesManager.vue'

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
            {
                path: '/', component: Main, name: '主页',
                meta: { redirectAuth: true }
            },
            {
                path: '/SysFieldManager', component: SysFieldManager, name: '系统管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/FileManager', component: FileManager, name: '文件管理',
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
                path: '/UserManager', component: UserManager, name: '用户管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/UserProjManager', component: UserProjManager, name: '用户项目管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/UserDeptManager', component: UserDeptManager, name: '用户部门管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/UserRoleManager', component: UserRoleManager, name: '用户角色管理',
                meta: { redirectAuth: true }
            },
            // { path: '/vuex', component: VuexComp, name: 'Vuex' }
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
                path: '/DeviceManager', component: DeviceManager, name: '设备管理',
                meta: { redirectAuth: true }
            },
            {
                path: '/YSDeviceManager', component: YSDeviceManager, name: '萤石设备管理',
                meta: { redirectAuth: true }
            },
        ],
        meta: { redirectAuth: true }
    },
    {
        path: '/',
        component: Home,
        name: '会议管理',
        iconCls: 'fa fa-id-card-o',
        children: [
            {
                path: '/Meeting', component: Meeting, name: '会议管理',
                meta: { redirectAuth: true }
            },
        ],
        meta: { redirectAuth: true }
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
        var session = sessionStorage.getItem('Session')
        if (store.state.sessionid || session) {
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

