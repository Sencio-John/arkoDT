package com.example.arkoperator.Adapters;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.arkoperator.Vessel.Vessel;

import java.util.ArrayList;

public class CustomVesselsAdapter extends RecyclerView.Adapter<CustomVesselsAdapter.MyVH> {

    Context context;
    ArrayList<Vessel> vessels;

    public CustomVesselsAdapter(Context context, ArrayList<Vessel> vessels) {
        this.context = context;
        this.vessels = vessels;
    }

    @NonNull
    @Override
    public CustomVesselsAdapter.MyVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        return null;
    }

    @Override
    public void onBindViewHolder(@NonNull CustomVesselsAdapter.MyVH holder, int position) {

    }

    @Override
    public int getItemCount() {
        return vessels.size();
    }

    public static class MyVH extends RecyclerView.ViewHolder{
        TextView vesselName;
        TextView networkName;
        TextView vesselStatus;
        public MyVH(@NonNull View itemView) {
            super(itemView);
        }
    }
}
