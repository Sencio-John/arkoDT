package com.example.arkoperator;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.Toast;

import androidx.activity.OnBackPressedCallback;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.arkoperator.Adapters.CustomVesselsAdapter;
import com.example.arkoperator.Vessel.Vessel;
import com.example.arkoperator.Vessel.VesselService;

import java.util.ArrayList;
import java.util.Objects;

public class VesselListActivity extends AppCompatActivity {
    private Toolbar toolbar;
    private Button btnAddDevice;
    private RecyclerView lsDevices;
    private CustomVesselsAdapter adapter;
    private VesselService vesselService;
    private ArrayList<Vessel> vessels;

    static final int STATE_CONNECTED = 1;
    static final int STATE_CONNECTION_FAILED = 2;
    static final int STATE_MESSAGE_RECEIVED = 3;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_list_vessel);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.vessel), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        initFields();
        initComponents();
        initEventListeners();
        loadDevices();
    }

    Handler handler = new Handler(Looper.getMainLooper())
    {
        @Override
        public void handleMessage(@NonNull Message msg) {
            super.handleMessage(msg);
            switch (msg.what){
                case STATE_CONNECTED:
                    Toast.makeText(getApplicationContext(),"Connected", Toast.LENGTH_LONG).show();
                    break;
                case STATE_CONNECTION_FAILED:
                    Toast.makeText(getApplicationContext(),"Connection Failed", Toast.LENGTH_LONG).show();
                    break;
                case STATE_MESSAGE_RECEIVED:
                    Toast.makeText(getApplicationContext(), msg.obj.toString(), Toast.LENGTH_LONG).show();
                    break;
            }
        }
    };

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == android.R.id.home) {
            finish();
        }
        return super.onOptionsItemSelected(item);
    }

    private void initFields(){
        vesselService = new VesselService(VesselListActivity.this);
    }

    private void initComponents() {
        toolbar = findViewById(R.id.bltoolbar);
        setSupportActionBar(toolbar);
        Objects.requireNonNull(getSupportActionBar()).setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeAsUpIndicator(R.drawable.ic_back_white);
        btnAddDevice = findViewById(R.id.btnAddDevice);
        lsDevices = findViewById(R.id.lsDevices);
    }

    private void initEventListeners() {
        btnAddDevice.setOnClickListener(view -> {
            Intent viewBluetooth = new Intent(VesselListActivity.this, BluetoothActivity.class);
            startActivity(viewBluetooth);
            finish();
        });

        OnBackPressedCallback callback = new OnBackPressedCallback(true) {
            @Override
            public void handleOnBackPressed() {
                // put something on dashboard I guess
                finish();
            }
        };
    }

    private void loadDevices(){

        vessels = vesselService.getAllVessel();
        adapter = new CustomVesselsAdapter(this, vessels);
        adapter.setOnItemClickListener(vessel -> {
            Intent startControl = new Intent(VesselListActivity.this, VesselActivity.class);
            startControl.putExtra("vesselID", vessel.getId());
            startActivity(startControl);
            finish();
        });
        lsDevices.setAdapter(adapter);
        lsDevices.setLayoutManager(new LinearLayoutManager(this));
    }


}
