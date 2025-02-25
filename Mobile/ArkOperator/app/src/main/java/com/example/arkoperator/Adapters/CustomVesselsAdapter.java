package com.example.arkoperator.Adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.arkoperator.R;
import com.example.arkoperator.Vessel.Vessel;

import java.util.ArrayList;

public class CustomVesselsAdapter extends RecyclerView.Adapter<CustomVesselsAdapter.MyVH> {

    Context context;
    ArrayList<Vessel> vessels;
    private OnItemClickListener onItemClickListener;

    public CustomVesselsAdapter(Context context, ArrayList<Vessel> vessels) {
        this.context = context;
        this.vessels = vessels;
    }

    public interface OnItemClickListener {
        void onItemClick(Vessel vessel);
    }

    public void setOnItemClickListener(OnItemClickListener listener) {
        this.onItemClickListener = listener;
    }

    @NonNull
    @Override
    public CustomVesselsAdapter.MyVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(context);
        View view = inflater.inflate(R.layout.row_custom_devices, parent, false);

        return new CustomVesselsAdapter.MyVH(view);
    }

    @Override
    public void onBindViewHolder(@NonNull CustomVesselsAdapter.MyVH holder, int index) {
        holder.vesselName.setText(vessels.get(index).getVesselName());
        holder.networkName.setText(vessels.get(index).getNetworkName());
        holder.vesselStatus.setText(R.string.lblOffline);

        holder.itemView.setOnClickListener(v -> {
            if (onItemClickListener != null) {
                onItemClickListener.onItemClick(vessels.get(index));  // This will trigger the onItemClick method in the activity
            }
        });
    }

    public void updateStatus(int position, String status) {
        if (position >= 0 && position < vessels.size()) {
            vessels.get(position).setStatus(status);
            notifyItemChanged(position, "status_update");
        }
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
            vesselName = itemView.findViewById(R.id.txtVesselName);
            networkName = itemView.findViewById(R.id.txtNetworkName);
            vesselStatus = itemView.findViewById(R.id.txtStatus);
        }
    }
}
