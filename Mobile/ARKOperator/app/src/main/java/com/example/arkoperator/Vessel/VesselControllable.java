package com.example.arkoperator.Vessel;

import java.util.List;

public interface VesselControllable {
    Vessel getVessel(int id);
    List<Vessel> getAllVessel();
    boolean addVessel(Vessel vessel);
    boolean removeVessel(int id);
    boolean updateVessel(Vessel vessel);

}
