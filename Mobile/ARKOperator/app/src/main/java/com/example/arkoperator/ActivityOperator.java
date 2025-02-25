package com.example.arkoperator;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.example.arkoperator.Sockets.WebSocketClient;

public class ActivityOperator extends Activity {
    private WebView videoFeed;
    private String streamUrl;
    private String readUrl;
    private WebSocketClient readClient;
    private WebSocketClient controlClient;
    private TextView txtLat;
    private TextView txtLong;


    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_operate);

        initializeFields();
        initializeComponents();
        setupWebView();
        beginSocket();
    }

    Handler handler = new Handler(Looper.getMainLooper())
    {
        @Override
        public void handleMessage(@NonNull Message msg) {
            super.handleMessage(msg);
            switch (msg.what){
                case 1:
                    break;
                case 2:
                    loadReadings(msg.obj.toString());
                    break;
                case 3:
                    break;
            }
        }
    };

    private void initializeFields() {
        String ip = getIntent().getStringExtra("ip");

        streamUrl = "http://" + ip + ":4000";
        readUrl = "ws://" + ip;
        readClient = new WebSocketClient(readUrl, 7777, handler, "");
        controlClient= new WebSocketClient(readUrl, 7878, handler, "");
    }

    private void initializeComponents() {
        videoFeed = findViewById(R.id.videoFeed);
        videoFeed.setWebViewClient(new WebViewClient());
        txtLat = findViewById(R.id.txtLatitude);
        txtLong = findViewById(R.id.txtLongitude);
    }

    @SuppressLint("SetJavaScriptEnabled")
    private void setupWebView() {
        WebSettings webSettings = videoFeed.getSettings();
        webSettings.setBuiltInZoomControls(true);
        webSettings.setDisplayZoomControls(false);
        webSettings.setLoadWithOverviewMode(true);
        webSettings.setUseWideViewPort(true);
        webSettings.setJavaScriptEnabled(true);
        WebView.setWebContentsDebuggingEnabled(false);
        videoFeed.loadUrl(streamUrl);
    }

    private void beginSocket(){
        readClient.connect();
    }

    private void loadReadings(String data){
        String[] info = data.split(",");
        txtLat.setText(info[0]);
        txtLong.setText(info[1]);
    }
}
