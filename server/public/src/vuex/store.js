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
    //城市ID
    cityid: '',
    //省份ID
    provid: '',
    //萤石Token
    ystoken: '',
    //人员session
    sessionid: ''
}

// 定义所需的 mutations
const mutations = {
    INCREMENT(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.count++
    },
    DECREMENT(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.count--
    },
    //请求选择的项目
    GETPROJ(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.proj
    },
    GETSESSION(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.sessionid
    },
    GETYSTOKEN(state) {
        state = state || JSON.parse(sessionStorage.getItem("Session"))
        state.ystoken
    },
    SETLOGIN(state, LOGINSTATE) {
        state.sessionid = LOGINSTATE.SessionID
        state.ystoken = LOGINSTATE.YSToken
        sessionStorage.setItem("Session", JSON.stringify(state))
    }
}

// 创建 store 实例
export default new Vuex.Store({
    actions,
    getters,
    state,
    mutations
})