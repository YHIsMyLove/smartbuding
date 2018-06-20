package cn.yooling.app.gdzs.plugin;

import android.content.Intent;
import android.content.IntentFilter;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.videogo.EzvizBroadcastReceiver;
import com.videogo.LoginSelectActivity;
import com.videogo.constant.Constant;


public class MainActivity extends AppCompatActivity {

    private EzvizBroadcastReceiver receiver;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        receiver = new EzvizBroadcastReceiver();
        IntentFilter filter = new IntentFilter();
        filter.addAction(Constant.OAUTH_SUCCESS_ACTION);
        registerReceiver(receiver, filter);

        Button btn = findViewById(R.id.button);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(MainActivity.this, LoginSelectActivity.class));
            }
        });
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        unregisterReceiver(receiver);
    }
}
