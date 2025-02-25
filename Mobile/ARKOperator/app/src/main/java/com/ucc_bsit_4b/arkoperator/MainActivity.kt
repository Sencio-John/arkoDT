package com.example.myapp

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val btnShowDialog = findViewById<Button>(R.id.btnShowDialog)

        btnShowDialog.setOnClickListener {
            showDialog()
        }
    }

    private fun showDialog() {
        // Create the dialog layout
        val dialogView = layoutInflater.inflate(R.layout., null)

        val etKey = dialogView.findViewById<EditText>(R.id.etKey)
        val etPassword = dialogView.findViewById<EditText>(R.id.etPassword)
        val btnCancel = dialogView.findViewById<Button>(R.id.btnCancel)
        val btnVerify = dialogView.findViewById<Button>(R.id.btnVerify)

        val dialog = AlertDialog.Builder(this)
            .setView(dialogView)
            .setCancelable(false) // Prevent closing the dialog by tapping outside
            .create()

        // Cancel button action
        btnCancel.setOnClickListener {
            etKey.text.clear()
            etPassword.text.clear()
            dialog.dismiss()
        }

        // Verify button action
        btnVerify.setOnClickListener {
            val key = etKey.text.toString()
            val password = etPassword.text.toString()
            Toast.makeText(this, "Key: $key\nPassword: $password", Toast.LENGTH_SHORT).show()
            dialog.dismiss() // Close the dialog
        }

        // Show the dialog
        dialog.show()
    }
}
