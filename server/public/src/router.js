import Vue from 'vue'
import Router from 'vue-router'
import Login from './components/Login.vue'
import Main from './components/Main.vue'
import Home from './components/layout/Home.vue'
Vue.use(Router)

export default new Router({
    routes: [
        {
            path: '/login',
            component: Login,
            hidden: true
        },
        {
            path: '/',
            component: Home,
            name: '主页',
            iconCls: 'iconfont icon-home',//图标样式class
            children: [
                {
                    path: '/',
                    component: Main,
                    name: '主页',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/MenuLinkElement',
                    component: resolve => require(['./components/APIBuilder/MenuLinkElement.vue'], resolve),
                    name: '路由管理',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/MenuAuthElement',
                    component: resolve => require(['./components/APIBuilder/MenuAuthElement.vue'], resolve),
                    name: '路由权限管理',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/SysFieldManager',
                    component: resolve =>
                        require(['./components/business/SysFieldManager.vue'], resolve),
                    name: '字段管理',
                    meta: { redirectAuth: true },
                },
                // {
                //     path: '/FileManager',
                //     component: resolve => require(['./components/business/FileManager.vue'], resolve),
                //     name: '文件管理',
                //     hidden: true,
                //     meta: { redirectAuth: true },
                //     hidden: true
                // },
            ],
            meta: { redirectAuth: true }
        },
        {
            path: '/',
            component: Home,
            name: '人员管理',
            iconCls: 'iconfont icon-user',
            children: [
                {
                    path: '/UserManager',
                    component: resolve => require(['./components/business/UserManager.vue'], resolve),
                    name: '用户管理',
                    hidden: true,
                    meta: { redirectAuth: true },
                },
                {
                    path: '/UserProjManager',
                    component: resolve => require(['./components/business/UserProjManager.vue'], resolve),
                    name: '用户项目管理',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/UserDeptManager',
                    component: resolve => require(['./components/business/UserDeptManager.vue'], resolve),
                    name: '用户部门管理',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/UserRoleManager',
                    component: resolve => require(['./components/business/UserRoleManager.vue'], resolve),
                    name: '用户角色管理',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/RankingListElement',
                    component: resolve => require(['./components/APIBuilder/RankingListElement.vue'], resolve),
                    name: '红黑榜管理',
                    meta: { redirectAuth: true },
                },
            ],
            meta: { redirectAuth: true },
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
                    name: '系统设备管理',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/YSDeviceManager',
                    component: resolve => require(['./components/business/YSDeviceManager.vue'], resolve),
                    name: '萤石设备管理',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/MechanicalElement',
                    component: resolve => require(['./components/APIBuilder/MechanicalElement.vue'], resolve),
                    name: '机械设备检测管理',
                    meta: { redirectAuth: true }
                },
                {
                    path: '/DoorIOElement',
                    component: resolve => require(['./components/APIBuilder/DoorIOElement.vue'], resolve),
                    name: '进出记录管理',
                    meta: { redirectAuth: true }
                },
                // {
                //     path: '/DoorDeviceManager',
                //     component: resolve => require(['./components/business/DoorDeviceManager.vue'], resolve),
                //     name: '门禁设备管理',
                //     meta: { redirectAuth: true },
                // },
            ],
            meta: { redirectAuth: true },
        },
        {
            path: '/',
            component: Home,
            name: '事件管理',
            iconCls: 'iconfont icon-message',
            children: [
                {
                    path: '/Meeting',
                    component: resolve => require(['./components/business/Meeting.vue'], resolve),
                    name: '会议管理',
                    meta: { redirectAuth: true },
                },
                {
                    path: '/BigNewsElement',
                    component: resolve => require(['./components/APIBuilder/BigNewsElement.vue'], resolve),
                    name: '大事件管理',
                    meta: { redirectAuth: true }
                },
                {
                    path: '/QualityAcceptanceElement',
                    component: resolve => require(['./components/APIBuilder/QualityAcceptanceElement.vue'], resolve),
                    name: '质量验收管理',
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
})