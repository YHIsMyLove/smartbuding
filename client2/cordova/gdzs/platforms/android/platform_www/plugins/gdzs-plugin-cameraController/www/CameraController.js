cordova.define("gdzs-plugin-cameraController.CameraController", function(require, exports, module) {
var exec = require('cordova/exec');

exports.gotoCameraListPage = function (arg0, success, error) {
    exec(success, error, 'CameraController', 'gotoCameraListPage', [arg0]);
};

});
