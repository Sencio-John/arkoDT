package com.example.arkoperator.Sockets;

import android.util.Log;

import androidx.annotation.NonNull;

import android.os.Handler;
import android.os.Message;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.WebSocket;
import okhttp3.WebSocketListener;
import okio.ByteString;

public class WebSocketClient {

    private WebSocket webSocket;
    private String url;
    private int port;
    private String path;
    private Message msg;
    private Handler handler;

    public WebSocketClient(String url, int port, Handler handler, String path) {
        this.url = url;
        this.port = port;
        this.handler = handler;
        this.path = path;
    }

    public void connect() {
        OkHttpClient client = new OkHttpClient.Builder()
                .readTimeout(3, TimeUnit.SECONDS)
                .build();

        Request request = new Request.Builder()
                .url(url +":"+ port)
                .build();

        webSocket = client.newWebSocket(request, new WebSocketListener() {
            @Override
            public void onOpen(@NonNull WebSocket webSocket, @NonNull okhttp3.Response response) {
                handler.obtainMessage(1).sendToTarget();
                webSocket.send("Hello from Android!");
            }

            @Override
            public void onMessage(@NonNull WebSocket webSocket, @NonNull String text) {
                handler.obtainMessage(2, text.trim()).sendToTarget();
                Log.d("WebSocketMessage", text);
            }

            @Override
            public void onMessage(@NonNull WebSocket webSocket, @NonNull ByteString bytes) {
                Log.d("WebSocket", "Received bytes: " + bytes.hex());
            }

            @Override
            public void onClosing(@NonNull WebSocket webSocket, int code, @NonNull String reason) {
                webSocket.close(1000, null);
                Log.d("WebSocket", "Closing: " + reason);
            }

            @Override
            public void onFailure(@NonNull WebSocket webSocket, @NonNull Throwable t, okhttp3.Response response) {
                handler.obtainMessage(3).sendToTarget();;
                Log.d("WebSocket", "Error: " + t.getMessage());
            }
        });
    }

    public void sendMessage(String message) {
        if (webSocket != null) {
            webSocket.send(message);
        } else {
            Log.e("WebSocket", "WebSocket is not connected");
        }
    }

    public void closeWebSocket() {
        if (webSocket != null) {
            webSocket.close(1000, "Goodbye");
        }
    }
}
