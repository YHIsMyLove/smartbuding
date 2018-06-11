cordova.define('cordova/plugin_list', function(require, exports, module) {
module.exports = [
  {
    "id": "cordova-plugin-splashscreen.SplashScreen",
    "file": "plugins/cordova-plugin-splashscreen/www/splashscreen.js",
    "pluginId": "cordova-plugin-splashscreen",
    "clobbers": [
      "navigator.splashscreen"
    ]
  },
  {
    "id": "gdzs-plugin-cameraController.CameraController",
    "file": "plugins/gdzs-plugin-cameraController/www/CameraController.js",
    "pluginId": "gdzs-plugin-cameraController",
    "clobbers": [
      "cordova.plugins.CameraController"
    ]
  }
];
module.exports.metadata = 
// TOP OF METADATA
{
  "cordova-plugin-whitelist": "1.3.3",
  "cordova-plugin-splashscreen": "5.0.2",
  "gdzs-plugin-cameraController": "1.0.0"
};
// BOTTOM OF METADATA
});