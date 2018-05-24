/********************************************************************** */
/**用户相关************************************************************* */
/********************************************************************** */
export const getSession = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.sessionid
}
export const getUser = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.User
}
/********************************************************************** */
/**项目相关************************************************************* */
/********************************************************************** */
export const getProj = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.proj
}
export const getProjInfo = state => {
    state = state || JSON.parse(sessionStorage.getItem("Session"))
    return state.Proj
}
/********************************************************************** */
/********************************************************************** */
/********************************************************************** */
export const getCount = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.count
}
export const getYSToken = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.ystoken
}