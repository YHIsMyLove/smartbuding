//test
export const getCount = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.count
}

export const getProj = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.proj
}

export const getSession = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.sessionid
}
export const getYSToken = state => {
    state = JSON.parse(sessionStorage.getItem("Session"))
    return state.ystoken
}