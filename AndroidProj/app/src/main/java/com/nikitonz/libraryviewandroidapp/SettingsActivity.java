package com.nikitonz.libraryviewandroidapp;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import androidx.localbroadcastmanager.content.LocalBroadcastManager;

public class SettingsActivity extends AppCompatActivity {
    private EditText editTextIp;
    private EditText editTextPort;
    private EditText editTextDb;
    private EditText editTextUsername;
    private EditText editTextPassword;
    private Button buttonSave;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);

        editTextIp = findViewById(R.id.editTextIp);
        editTextPort = findViewById(R.id.editTextPort);
        editTextDb = findViewById(R.id.editTextDb);
        editTextUsername = findViewById(R.id.editTextUsername);
        editTextPassword = findViewById(R.id.editTextPassword);
        buttonSave = findViewById(R.id.buttonSave);

        loadSettings();

        buttonSave.setOnClickListener(v -> {
            saveSettings();
            notifyHomeFragmentToUpdate();
            finish();
        });
    }
    private void notifyHomeFragmentToUpdate() {


            Intent intent = new Intent("UPDATE_DATABASE");
            // Отправляем широковещательное сообщение
            LocalBroadcastManager.getInstance(this).sendBroadcast(intent);
        }

    private void loadSettings() {
        SharedPreferences sharedPreferences = getSharedPreferences("settings", Context.MODE_PRIVATE);
        editTextIp.setText(sharedPreferences.getString("ip", "192.168.100.3"));
        editTextPort.setText(sharedPreferences.getString("port", "1433"));
        editTextDb.setText(sharedPreferences.getString("db", "Библиотека"));
        editTextUsername.setText(sharedPreferences.getString("username", "Guest"));
        editTextPassword.setText(sharedPreferences.getString("password", "1"));
    }

    private void saveSettings() {
        SharedPreferences sharedPreferences = getSharedPreferences("settings", Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putString("ip", editTextIp.getText().toString());
        editor.putString("port", editTextPort.getText().toString());
        editor.putString("db", editTextDb.getText().toString());
        editor.putString("username", editTextUsername.getText().toString());
        editor.putString("password", editTextPassword.getText().toString());
        editor.apply();

        Toast.makeText(SettingsActivity.this, "Настройки сохранены", Toast.LENGTH_SHORT).show();

    }


}