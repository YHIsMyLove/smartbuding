<template>
    <f7-login-screen id="login-screen">
      <f7-view>
        <f7-page login-screen>
          <f7-login-screen-title>登录</f7-login-screen-title>
          <f7-list form>
            <f7-list-item>
              <f7-label>用户名</f7-label>
              <f7-input name="username" placeholder="请输入用户名" type="text" value="lbgongfu"></f7-input>
            </f7-list-item>
            <f7-list-item>
              <f7-label>密码</f7-label>
              <f7-input name="password" type="password" placeholder="请输入密码" value="123456"></f7-input>
            </f7-list-item>
          </f7-list>
          <f7-list>
            <f7-list-button title="登录" @click="login"></f7-list-button>
            <f7-block-footer>
              
            </f7-block-footer>
          </f7-list>
        </f7-page>
      </f7-view>
    </f7-login-screen>
</template>
