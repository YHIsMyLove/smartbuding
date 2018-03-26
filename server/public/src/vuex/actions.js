//test
export const increment = ({ commit }) => {
    commit('INCREMENT')
}
export const decrement = ({ commit }) => {
    commit('DECREMENT')
}

export const getProj = ({ commit }) => {
    commit('GETPROJ')
}
//获取session
export const getSession = ({ commit }) => {
    commit('GETSESSION')
}
//设置session
export const setLogin = ({ commit }, loginState) => {
    commit('SETLOGIN', loginState)
}