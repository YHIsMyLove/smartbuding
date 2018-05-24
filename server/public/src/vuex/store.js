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
    },
    GETUSER(state) {
        state.User
    },
    /********************************************************************** */
    /**项目相关************************************************************* */
    /********************************************************************** */
    SETPROJ(state, newproj) {
        state.Proj.ProjID = newproj.ProjID
        state.Proj.ProjName = newproj.ProjName
        state.Proj.CityName = newproj.CityName
        state.Proj.ProvName = newproj.ProvName

        console.log(state)
    },
    GETPROJ(state) {
        state.Proj
    },
    SETCITY(state, city) {
        state.Proj.CityID = city
    },
    SETPROV(state, prov) {
        state.Proj.ProvID = prov
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
    plugins: [
        createPersistedState()
    ]
})