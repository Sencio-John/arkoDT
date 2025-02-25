package com.example.arkoperator.Vessel;

public class Vessel {

    private int id;
    private String vesselName;
    private String networkName;
    private String ipAddress;
    private String token;
    private String status;

    public Vessel(String vesselName, String networkName, String ipAddress, String token) {
        this.vesselName = vesselName;
        this.networkName = networkName;
        this.ipAddress = ipAddress;
        this.token = token;
        status = "Offline";
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getVesselName() {
        return vesselName;
    }

    public void setVesselName(String vesselName) {
        this.vesselName = vesselName;
    }

    public String getNetworkName() {
        return networkName;
    }

    public void setNetworkName(String networkName) {
        this.networkName = networkName;
    }

    public String getIpAddress() {
        return ipAddress;
    }

    public void setIpAddress(String ipAddress) {
        this.ipAddress = ipAddress;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }
}
