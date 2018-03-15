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
    proj: "5aa905ae9b5dad48ecd2bb89"//黄石奥体中心
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