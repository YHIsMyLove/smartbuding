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
    proj: "5aa905ae9b5dad48ecd2bb89",
    //城市ID
    cityid: '',
    //省份ID
    provid: '',
    //萤石Token
    ystoken: 'at.6r8wpspz9p6k5omn0d20oq7j5l139vwq-6ihmyg8top-1kdce8f-81yi0iht8',
    //人员session
    sessionid: ''
}

// 定义所需的 mutations
const mutations = {
    INCREMENT(state) {
        state.count++
    },
    DECREMENT(state) {
        state.count--
    },
    //请求选择的项目
    GETPROJ(state) {
        state.proj
    }
}

// 创建 store 实例
export default new Vuex.Store({
    actions,
    getters,
    state,
    mutations
})