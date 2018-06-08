import Vue from 'vue'
import App from './App.vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-default/index.css'
import store from './vuex/store'
import Vuex from 'vuex'
import NProgress from 'nprogress'//页面顶部进度条
import 'nprogress/nprogress.css'

import Login from './components/Login.vue'
import Main from './components/Main.vue'
import Home from './components/layout/Home.vue'
import axios from 'axios';
import router from './router'

import VCharts from 'v-charts'
Vue.use(VCharts)
Vue.use(ElementUI)
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


// let data = JSON.parse(window.sessionStorage.getItem('MenuInfo'))
// if (data) {
//     router.addRoutes(data)
//     window.sessionStorage.removeItem('isLoadNodes')
// }

//store
router.beforeEach((to, from, next) => {
    if (to.meta.redirectAuth) {
        NProgress.start();
        if (store.state.User.UserID) {
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

