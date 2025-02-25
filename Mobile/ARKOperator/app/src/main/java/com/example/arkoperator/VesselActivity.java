package com.example.arkoperator;

import android.content.Intent;
import android.os.Bundle;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.TextView;

import androidx.activity.OnBackPressedCallback;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import com.example.arkoperator.Vessel.Vessel;
import com.example.arkoperator.Vessel.VesselService;

import java.util.Objects;

public class VesselActivity extends AppCompatActivity {
    private TextView lblStatus;
    private TextView lblVesselName;
    private TextView lblNetworkName;
    private TextView lblIP;
    private Button btnOperate;
    private Toolbar toolbar;
    private int vesselID;
    private Vessel vessel;
    private VesselService vesselService;

    private Intent startVesselList;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_vessel);
        initFields();
        initComponents();
        initEventListeners();
        loadDetails();

    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        if (item.getItemId() == android.R.id.home) {
            startActivity(startVesselList);
            finish();
        }

        return super.onOptionsItemSelected(item);
    }

    private void initFields(){
        vesselID = getIntent().getIntExtra("vesselID", -1);
        vesselService = new VesselService(VesselActivity.this);
        startVesselList = new Intent(VesselActivity.this, VesselListActivity.class);
        vessel = vesselService.getVessel(vesselID);
    }

    private void initComponents(){
        toolbar = findViewById(R.id.bltoolbar);
        setSupportActionBar(toolbar);
        Objects.requireNonNull(getSupportActionBar()).setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeAsUpIndicator(R.drawable.ic_back_white);
        lblStatus = findViewById(R.id.txtStatus);
        lblVesselName = findViewById(R.id.txtVesselName);
        lblNetworkName = findViewById(R.id.txtNetworkName);
        lblIP = findViewById(R.id.txtIP);
        btnOperate = findViewById(R.id.btnOperate);
    }

    private void initEventListeners(){
        OnBackPressedCallback callback = new OnBackPressedCallback(true) {
            @Override
            public void handleOnBackPressed() {
                startActivity(startVesselList);
                finish();
            }
        };
        getOnBackPressedDispatcher().addCallback(this, callback);

        btnOperate.setOnClickListener(view -> {
            Intent startOperating = new Intent(VesselActivity.this, ActivityOperator.class);
            startOperating.putExtra("ip", vessel.getIpAddress());
            startActivity(startOperating);
        });
    }

    private void loadDetails(){
        lblStatus.setText(R.string.lblOffline);
        lblVesselName.setText(vessel.getVesselName());
        lblNetworkName.setText(vessel.getNetworkName());
        lblIP.setText(vessel.getIpAddress());
    }

}
