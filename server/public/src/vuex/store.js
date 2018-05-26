import Vue from 'vue'
import Vuex from 'vuex'
import * as actions from './actions'
import * as getters from './getters'
import axios from 'axios'
import createPersistedState from 'vuex-persistedstate'

Vue.use(Vuex)

// 应用初始状态
const state = {
    //用户
    User: {
        UserID: '',
        SessionID: '',
        YSToken: ''
    },
    //项目
    Proj: {
        ProjID: '',
        ProjName: '',
        CityID: '',
        CityName: '',
        ProvID: '',
        ProvName: ''
    },
}

// 定义所需的 mutations
const mutations = {
    /********************************************************************** */
    /**用户相关************************************************************* */
    /********************************************************************** */
    SETLOGIN(state, LOGINSTATE) {
        state.User.YSToken = LOGINSTATE.YSToken
        state.User.UserID = LOGINSTATE.UserID
        state.User.SessionID = LOGINSTATE.SessionID
        sessionStorage.setItem("UserSession", JSON.stringify(state.User))
        //sessionStorage.setItem("Proj", JSON.stringify(state.Proj))
        console.log(JSON.stringify(state.Proj))
        state.User = JSON.parse(sessionStorage.getItem("UserSession"))
        //state.Proj = JSON.parse(sessionStorage.getItem("Proj"))
    },
    GETUSER(state) {
        state.User = state.User || JSON.parse(sessionStorage.getItem("UserSession"))
        state.User
    },
    /********************************************************************** */
    /**项目相关************************************************************* */
    /********************************************************************** */
    GETPROJ(state) {
        state.Proj = state.Proj || JSON.parse(sessionStorage.getItem("UserSession"))
        state.Proj
    },
    SETPROJ(state, newproj) {
        state.Proj.ProjID = newproj.ProjID
        state.Proj.ProjName = newproj.ProjName
        state.Proj.CityName = newproj.CityName
        state.Proj.ProvName = newproj.ProvName
        sessionStorage.setItem('UserSession', JSON.stringify(state.Proj))
        state.Proj = JSON.parse(sessionStorage.getItem("UserSession"))
        console.log(`设置项目后:${sessionStorage.getItem("UserSession")}`)
    },
    SETCITY(state, city) {
        state.Proj = state.Proj || JSON.parse(sessionStorage.getItem("UserSession"))
        state.Proj.CityID = city
        sessionStorage.setItem('UserSession', JSON.stringify(state.Proj))
        state.Proj = JSON.parse(sessionStorage.getItem("UserSession"))
    },
    SETPROV(state, prov) {
        state.Proj = state.Proj || JSON.parse(sessionStorage.getItem("UserSession"))
        state.Proj.ProvID = prov
        sessionStorage.setItem('UserSession', JSON.stringify(state.Proj))
        state.Proj = JSON.parse(sessionStorage.getItem("UserSession"))
    },
    /********************************************************************** */
    /********************************************************************** */
    /********************************************************************** */
}

// 创建 store 实例
export default new Vuex.Store({
    actions,
    getters,
    state,
    mutations,
    plugins: [createPersistedState()]
})