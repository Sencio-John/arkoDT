package com.example.arkoperator;

import static android.view.View.INVISIBLE;
import static android.view.View.VISIBLE;

import android.annotation.SuppressLint;
import android.app.Dialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.view.MenuItem;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import androidx.annotation.NonNull;
import androidx.appcompat.content.res.AppCompatResources;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.example.arkoperator.Adapters.CustomBluetoothAdapter;
import com.example.arkoperator.Bluetooth.BluetoothService;
import com.example.arkoperator.Bluetooth.BluetoothClient;

import java.io.IOException;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;
import java.util.UUID;

@SuppressLint({"MissingPermission", "SetTextI18n"})
public class BluetoothActivity extends AppCompatActivity {

    BluetoothService bluetoothService;
    BluetoothClient client;
    Intent btEnablingIntent;
    ListView lstBondedDeviceView;
    ImageButton btnRefresh;
    Button btnAuthCancel;
    Button btnAuthSend;
    Button btnAddDevice;
    Button btnVesselCancel;
    TextView txtLoadingMessage;
    TextView txtTitleAuth;
    TextView txtAuthError;
    TextView txtDescriptionAuth;
    TextView txtVesselName;
    TextView txtNetwork;
    TextView txtLinkModNetwork;
    TextView txtIP;
    EditText inputKey;
    EditText inputPassword;
    Dialog authDialog;
    Dialog loadingDialog;
    Dialog vesselDialog;
    int requestCodeForEnable;
    static final int STATE_CONNECTED = 3;
    static final int STATE_CONNECTION_FAILED = 4;
    static final int STATE_MESSAGE_RECEIVED = 5;
    private static final UUID MY_UUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB");

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_bluetooth);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.bluetooth), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Toolbar toolbar = findViewById(R.id.bltoolbar);
        setSupportActionBar(toolbar);
        Objects.requireNonNull(getSupportActionBar()).setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeAsUpIndicator(R.drawable.ic_back_white);

        initializeFields();
        initializeComponents();
        initializeEventListeners();
        initializeBluetooth();
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == android.R.id.home) {
            Intent viewVessels = new Intent(BluetoothActivity.this, VesselActivity.class);
            startActivity(viewVessels);
            finish();
        }
        return super.onOptionsItemSelected(item);
    }

    private void initializeFields() {
        bluetoothService = new BluetoothService();
        btEnablingIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
        requestCodeForEnable = 1;
    }

    private void initializeComponents(){

        lstBondedDeviceView = findViewById(R.id.listViewPaired);
        btnRefresh = findViewById(R.id.btnRefresh);
        buildDialogs();
        // loading dialog
        txtLoadingMessage = loadingDialog.findViewById(R.id.txtLoadingMessage);
        // auth dialog
        txtAuthError = authDialog.findViewById(R.id.txtAuthError);
        btnAuthCancel = authDialog.findViewById(R.id.btnCancelDialogAuth);
        btnAuthSend = authDialog.findViewById(R.id.btnSendDialogAuth);
        inputKey = authDialog.findViewById(R.id.editTextKey);
        inputPassword = authDialog.findViewById(R.id.editTextPassword);
        txtTitleAuth = authDialog.findViewById(R.id.dialogTitle);
        txtDescriptionAuth = authDialog.findViewById(R.id.dialogDescription);
        // vessel dialog
        btnVesselCancel = vesselDialog.findViewById(R.id.btnCancelDialogVessel);
        btnAddDevice = vesselDialog.findViewById(R.id.btnAddDevice);
        txtVesselName = vesselDialog.findViewById(R.id.txtVesselName);
        txtIP = vesselDialog.findViewById(R.id.txtIP);
        txtNetwork = vesselDialog.findViewById(R.id.txtNetworkName);
        txtLinkModNetwork = vesselDialog.findViewById(R.id.linkModifyNet);
    }

    private void buildDialogs(){
        authDialog = new Dialog(BluetoothActivity.this);
        authDialog.setContentView(R.layout.dialog_custom_auth);
        Objects.requireNonNull(authDialog.getWindow()).setLayout(ViewGroup.LayoutParams.WRAP_CONTENT,ViewGroup.LayoutParams.WRAP_CONTENT);
        authDialog.getWindow().setBackgroundDrawable(AppCompatResources.getDrawable(getApplicationContext(),R.drawable.custom_dialog_bg));
        authDialog.setCancelable(false);

        loadingDialog = new Dialog(BluetoothActivity.this);
        loadingDialog.setContentView(R.layout.loading_screen);
        Objects.requireNonNull(loadingDialog.getWindow()).setLayout(ViewGroup.LayoutParams.WRAP_CONTENT,ViewGroup.LayoutParams.WRAP_CONTENT);
        loadingDialog.getWindow().setBackgroundDrawable(AppCompatResources.getDrawable(getApplicationContext(),R.drawable.custom_dialog_bg));
        loadingDialog.setCancelable(false);

        vesselDialog = new Dialog(BluetoothActivity.this);
        vesselDialog.setContentView(R.layout.dialog_custom_vessel);
        Objects.requireNonNull(vesselDialog.getWindow()).setLayout(ViewGroup.LayoutParams.WRAP_CONTENT,ViewGroup.LayoutParams.WRAP_CONTENT);
        vesselDialog.getWindow().setBackgroundDrawable(AppCompatResources.getDrawable(getApplicationContext(),R.drawable.custom_dialog_bg));
        vesselDialog.setCancelable(false);
    }

    private void initializeEventListeners(){

        btnRefresh.setOnClickListener(view ->
                showBondedDevices(bluetoothService.getBondedDevices())
        );

        lstBondedDeviceView.setOnItemClickListener((adapterView, view, i, l) -> {
            txtLoadingMessage.setText(R.string.lblConnecting);
            loadingDialog.show();
            BluetoothDevice device = (BluetoothDevice) view.getTag();

            if (device != null) {
                new Handler().post(() -> client = new BluetoothClient(handler, device, MY_UUID));
            } else {
                loadingDialog.dismiss();
                Toast.makeText(getApplicationContext(), "Error: Device not found!", Toast.LENGTH_LONG).show();
            }
        });

        btnAuthSend.setOnClickListener(view ->{
            txtLoadingMessage.setText(R.string.lblValidating);
            if (client != null){
                try {
                    String auth = view.getTag().toString();
                    if (auth.equals("C")){
                        setAuthDialog(auth);
                        client.send("C:" + inputKey.getText() + "," + inputPassword.getText() + "," + "username");
                    } else if (auth.equals("N")) {
                        client.send("N:" + inputKey.getText() + "," + inputPassword.getText() + "," + "username");
                    }
                } catch (IOException e) {
                    Toast.makeText(getApplicationContext(), "Error Sending Message!", Toast.LENGTH_SHORT).show();
                }
            }
            loadingDialog.show();
        });

        btnAddDevice.setOnClickListener(view -> {
            disconnect();
        });

        btnAuthCancel.setOnClickListener(view ->{
            disconnect();
            cleanDialog();
            authDialog.dismiss();
        });

        txtLinkModNetwork.setOnClickListener(view -> {
            setAuthDialog("N");
            authDialog.show();
        });

        btnVesselCancel.setOnClickListener(view -> {
            disconnect();
            cleanDialog();
            authDialog.dismiss();
            vesselDialog.dismiss();
        });

    }

    private void disconnect(){
        try {
            if (client != null){
                client.closeConnection();
            }
        } catch (IOException e) {
            Toast.makeText(getApplicationContext(), "Error: Closing connection. " + e, Toast.LENGTH_LONG).show();
        }
    }

    private void setAuthDialog(String auth) {
        if(auth.equals("C") ){
            btnAuthSend.setTag(auth);
            txtTitleAuth.setText(R.string.lblAuthBluetoothTitle);
            txtDescriptionAuth.setText(R.string.lblBTAuthDescription);
        } else if(auth.equals("N") ){
            btnAuthSend.setTag(auth);
            txtTitleAuth.setText(R.string.lblNetworkAuthTitle);
            txtDescriptionAuth.setText(R.string.lblNTAuthDescription);
        }
    }

    private void cleanDialog() {
        inputKey.setText("");
        inputPassword.setText("");
        txtAuthError.setVisibility(INVISIBLE);
    }

    Handler handler = new Handler(Looper.getMainLooper())
    {
        @Override
        public void handleMessage(@NonNull Message msg) {
            super.handleMessage(msg);
            switch (msg.what){
                case STATE_CONNECTED:
                    client.start();
                    loadingDialog.dismiss();
                    setAuthDialog("C");
                    authDialog.show();
                    Toast.makeText(getApplicationContext(),"Connected", Toast.LENGTH_LONG).show();
                    break;
                case STATE_CONNECTION_FAILED:
                    loadingDialog.dismiss();
                    Toast.makeText(getApplicationContext(),"Connection Failed", Toast.LENGTH_LONG).show();
                    break;
                case STATE_MESSAGE_RECEIVED:
                    loadingDialog.dismiss();
                    evaluateResponse(msg.obj.toString());
                    break;
            }
        }
    };

    private void initializeBluetooth(){
        if (!bluetoothService.isBluetoothAvailable()){
            Toast.makeText(getApplicationContext(), "Bluetooth does not support on this Device", Toast.LENGTH_LONG).show();
            return;
        }

        if (!bluetoothService.isBluetoothOn()){
            requestBluetoothOn();
        }
        bluetoothService.getBondedDevices();
        showBondedDevices(bluetoothService.getBondedDevices());
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if(requestCode==requestCodeForEnable)
        {
            if(resultCode==RESULT_OK)
            {
                Toast.makeText(getApplicationContext(),"Bluetooth is Enabled", Toast.LENGTH_LONG).show();
            }else if(resultCode==RESULT_CANCELED)
            {
                Toast.makeText(getApplicationContext(),"Bluetooth Enabling Declined", Toast.LENGTH_LONG).show();
            }
        }
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        Intent viewBluetooth = new Intent(BluetoothActivity.this, VesselActivity.class);
        startActivity(viewBluetooth);
    }

    @SuppressLint("MissingPermission")
    private void showBondedDevices(List<BluetoothDevice> devices){

        if (bluetoothService.isBluetoothOn()){
            CustomBluetoothAdapter customBluetoothAdapter = new CustomBluetoothAdapter(getApplicationContext(), devices);
            lstBondedDeviceView.setAdapter(customBluetoothAdapter);
        } else {
            requestBluetoothOn();
        }
    }

    private void requestBluetoothOn() {
        startActivityForResult(btEnablingIntent, requestCodeForEnable);
    }

    private void evaluateResponse(String data) {
        String[] info = data.split(":|,");
        if(info[1].length() > 1){
            if (info[0].equals("Auth")){
                authDialog.dismiss();
                txtVesselName.setText(info[3]);
                txtNetwork.setText(info[4]);
                txtIP.setText(info[2]);
                vesselDialog.show();
            }else if (info[0].equals("Net")){

            }
        } else {
            txtAuthError.setVisibility(VISIBLE);
            txtAuthError.setText("Invalid Credentials");
        }

        Toast.makeText(getApplicationContext(), "Message Received: " + Arrays.toString(info), Toast.LENGTH_SHORT).show();
    }

}
