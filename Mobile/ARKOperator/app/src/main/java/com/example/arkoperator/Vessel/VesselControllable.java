package com.example.arkoperator.Vessel;

import java.util.ArrayList;

public interface VesselControllable {
    Vessel getVessel(int id);
    ArrayList<Vessel> getAllVessel();
    boolean addVessel(Vessel vessel);
    boolean removeVessel(int id);
    boolean updateVessel(Vessel vessel);

}
