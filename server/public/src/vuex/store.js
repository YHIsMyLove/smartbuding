import Vue from 'vue'
import Vuex from 'vuex'
import * as actions from './actions'
import * as getters from './getters'
import axios from 'axios'

Vue.use(Vuex)

// 应用初始状态
const state = {
    count: 10,
    //系统总变量区域
    //项目ID
    proj: "5ac9d8dbe629f6134823c938",//临时替换成柳州项目
    //萤石Token
    ystoken: '',
    //人员session
    sessionid: '',
    //用户
    User: {
        UserID: '',
        SessionID: ''
    },
    //项目
    Proj: {
        ProjID: '',
        CityID: '',
        ProvID: ''
    },
}

// 定义所需的 mutations
const mutations = {
    /********************************************************************** */
    /**用户相关************************************************************* */
    /********************************************************************** */
    SETLOGIN(state, LOGINSTATE) {
        state.proj = "5ac9d8dbe629f6134823c938"
        state.sessionid = LOGINSTATE.SessionID
        state.ystoken = LOGINSTATE.YSToken
        state.User.UserID = LOGINSTATE.UserID
        state.User.SessionID = LOGINSTATE.SessionID
        sessionStorage.setItem("Session", JSON.stringify(state))
    },
    GETUSER(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.User
    },
    GETSESSION(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.sessionid
    },
    GETYSTOKEN(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.ystoken
    },
    /********************************************************************** */
    /**项目相关************************************************************* */
    /********************************************************************** */
    GETPROJ(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.proj
    },
    SETPROJ(state, newproj) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.proj = newproj
        state.Proj.ProjID = newproj
        sessionStorage.setItem('Session', JSON.stringify(state))
    },
    SETCITY(state, city) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.Proj.CityID = city
        sessionStorage.setItem('Session', JSON.stringify(state))
    },
    SETPROV(state, prov) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.Proj.ProvID = prov
        sessionStorage.setItem('Session', JSON.stringify(state))
    },
    GETPROJINFO(state, PROJINFO) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.Proj
    },
    /********************************************************************** */
    /********************************************************************** */
    /********************************************************************** */
    INCREMENT(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.count++
    },
    DECREMENT(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.count--
    },
}

// 创建 store 实例
export default new Vuex.Store({
    actions,
    getters,
    state,
    mutations
})