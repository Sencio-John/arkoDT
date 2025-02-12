package com.example.arkoperator;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.Objects;

public class VesselActivity extends AppCompatActivity {

    Button btnAddDevice;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_vessel);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.vessel), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Toolbar toolbar = findViewById(R.id.bltoolbar);
        setSupportActionBar(toolbar);
        Objects.requireNonNull(getSupportActionBar()).setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeAsUpIndicator(R.drawable.ic_back_white);

        initializeComponents();
        initializeEventListeners();
    }

    private void initializeComponents() {
        btnAddDevice = findViewById(R.id.btnAddDevice);
    }

    private void initializeEventListeners() {
        btnAddDevice.setOnClickListener(view -> {
            Intent viewBluetooth = new Intent(VesselActivity.this, BluetoothActivity.class);
            startActivity(viewBluetooth);
        });
    }


}
