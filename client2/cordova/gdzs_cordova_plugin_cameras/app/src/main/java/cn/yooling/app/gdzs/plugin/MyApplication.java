package cn.yooling.app.gdzs.plugin;

import android.app.Application;

import com.ezvizuikit.open.EZUIKit;
import com.videogo.openapi.EZOpenSDK;

public class MyApplication extends Application {
    public static final String APP_KEY = "443eed7d6dab47739915d6a237dcad34";

    @Override
    public void onCreate() {
        super.onCreate();
        initSDK();
    }

    private void initSDK() {
        {
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
            EZOpenSDK.initLib(this, APP_KEY);
        }
    }
}
