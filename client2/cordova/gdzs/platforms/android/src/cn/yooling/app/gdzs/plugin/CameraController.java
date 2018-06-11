package cn.yooling.app.gdzs.plugin;

import android.content.Intent;
import android.util.Log;
import android.widget.Toast;

import com.videogo.openapi.EZOpenSDK;

import org.apache.cordova.CallbackContext;
import org.apache.cordova.CordovaPlugin;
import org.json.JSONArray;
import org.json.JSONException;

public class CameraController extends CordovaPlugin {

    @Override
    protected void pluginInitialize() {
        super.pluginInitialize();
        initSDK();
    }

    private void initSDK() {
        /**
         * sdk日志开关，正式发布需要去掉
         */
        EZOpenSDK.showSDKLog(true);

        /**
         * 设置是否支持P2P取流,详见api
         */
        EZOpenSDK.enableP2P(true);

        /**
         * APP_KEY请替换成自己申请的
         */
        EZOpenSDK.initLib(cordova.getActivity().getApplication(), "443eed7d6dab47739915d6a237dcad34");
    }

    @Override
    public boolean execute(String action, JSONArray args, CallbackContext callbackContext) throws JSONException {

        if ("gotoCameraListPage".equals(action))
        {
            try {
                final String accessToken = args.getString(0);
                final CallbackContext callback = callbackContext;
                cordova.getThreadPool().execute(new Runnable() {
                    public void run() {

                        Intent toIntent = new Intent(cordova.getActivity(), com.videogo.ui.cameralist.EZCameraListActivity.class);
                        toIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                        EZOpenSDK.getInstance().setAccessToken(accessToken);
                        cordova.getActivity().startActivity(toIntent);
                        callback.success("success");
                    }
                });
            }
            catch (JSONException e)
            {
                Log.e("CameraController", "access token is valid.error msg: " + e.getMessage());
                Toast.makeText(cordova.getActivity(), "参数Access Token是空", Toast.LENGTH_LONG).show();
            }
            return true;
        }
        return false;
    }
}
