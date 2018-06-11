// Import Vue
import Vue from 'vue';

// Import F7
import Framework7 from 'framework7/dist/framework7.esm.bundle.js';

// Import F7 Vue Plugin
import Framework7Vue from 'framework7-vue/dist/framework7-vue.esm.bundle.js';

// Import F7 Styles
import Framework7Styles from 'framework7/dist/css/framework7.css';

// Import Icons and App Custom Styles
import IconsStyles from './css/icons.css';
import AppStyles from './css/app.css';

// muse ui
// import MuseUI from 'muse-ui';
// import 'muse-ui/dist/muse-ui.css';

// Import Routes
import Routes from './routes.js'

// Import App Component
import App from './app';

import ProjectMain from './pages/project/main.vue'
import DoorsMain from './pages/doors/main.vue'
import WorkersContacts from './pages/workers/contacts.vue'
import WorkersLocation from './pages/workers/location.vue'
import DatePicker from './pages/f7-date-picker.vue'
import DataPicker from './pages/f7-data-picker.vue'

String.prototype.format = function(args) {
  var result = this;
  if (arguments.length > 0) {
      if (arguments.length == 1 && typeof (args) == "object") {
          for (var key in args) {
              if(args[key]!=undefined){
                  var reg = new RegExp("({" + key + "})", "g");
                  result = result.replace(reg, args[key]);
              }
          }
      }
      else {
          for (var i = 0; i < arguments.length; i++) {
              if (arguments[i] != undefined) {
                  var reg= new RegExp("({)" + i + "(})", "g");
                  result = result.replace(reg, arguments[i]);
              }
          }
      }
  }
  return result;
}

// Init F7 Vue Plugin
Vue.use(Framework7Vue, Framework7)
// muse ui
// Vue.use(MuseUI);

Vue.component('project-main', ProjectMain)
Vue.component('doors-main', DoorsMain)
Vue.component('workers-contacts', WorkersContacts)
Vue.component('workers-location', WorkersLocation)
Vue.component('f7-date-picker', DatePicker)
Vue.component('f7-data-picker', DataPicker)

// Init App
new Vue({
  el: '#app',
  template: '<app/>',
  // Init Framework7 by passing parameters here
  framework7: {
    id: 'io.framework7.testapp', // App bundle ID
    name: 'Framework7', // App name
    theme: 'auto', // Automatic theme detection
    // App routes
    routes: Routes,
    dialog: {
      // set default title for all dialog shortcuts
      title: '提示',
      // change default "OK" button text
      buttonOk: '确定',
      buttonCancel: '取消'
    }
  },
  // Register App Component
  components: {
    app: App
  }
});
