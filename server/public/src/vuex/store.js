import Vue from 'vue'
import Vuex from 'vuex'
import * as actions from './actions'
import * as getters from './getters'
// import axios from 'axios'
import createPersistedState from 'vuex-persistedstate'

Vue.use(Vuex)

// 应用初始状态
const state = {
    //用户
    User: {
        UserID: '',
        UserName: '',
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
    }
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
        state.User.UserName = LOGINSTATE.UserName
    },
    GETUSER(state) {
        state.User = state.User
        state.User
    },
    /********************************************************************** */
    /**项目相关************************************************************* */
    /********************************************************************** */
    GETPROJ(state) {
        state.Proj = state.Proj
        state.Proj
    },
    SETPROJ(state, newproj) {
        state.Proj.ProjID = newproj.ProjID
        state.Proj.ProjName = newproj.ProjName
        state.Proj.CityName = newproj.CityName
        state.Proj.ProvName = newproj.ProvName
    },
    SETCITY(state, city) {
        state.Proj = state.Proj
        state.Proj.CityID = city
    },
    SETPROV(state, prov) {
        state.Proj = state.Proj
        state.Proj.ProvID = prov
    }
<<<<<<< HEAD
=======
    /********************************************************************** */
>>>>>>> 72bef902ba622ae0f489c5b3a7807f336d63243e
    /********************************************************************** */
    /**权限相关************************************************************* */
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