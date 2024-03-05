package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Handler;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;

public class DatabaseMgr{

    private Connection connection;
    private final Context context;
    private OnConnectionEstablishedListener connectionListener;
    private OnConnectionEstablishedListener connectionNOTEstablishedListener;
    private final ProgressBar progressBar;

    public DatabaseMgr(Context context, ProgressBar pb) {
        this.context = context;
        progressBar = pb;
    }

    public void setOnConnectionEstablishedListener(OnConnectionEstablishedListener listener) {
        this.connectionListener = listener;
    }

    public void setOnConnectionNOTEstablishedListener(OnConnectionEstablishedListener listener) {
        this.connectionNOTEstablishedListener = listener;
    }

    public void getBooks(String searchTerm, OnQueryResponseListener listener) {
        String query = "USE Библиотека; SELECT * FROM ТаблицаДляМобилок('" + searchTerm + "')";
        getQueryResponse(query, listener);
    }

    public void getBooks(OnQueryResponseListener listener) {
        String query = "USE Библиотека; SELECT TOP(20) * FROM ТаблицаДляМобилок('')";
        getQueryResponse(query, listener);
    }

    private void getQueryResponse(String query, OnQueryResponseListener listener) {
        progressBar.setVisibility(View.VISIBLE);
        Executor executor = Executors.newSingleThreadExecutor();
        executor.execute(() -> {
            List<Book> bookList = new ArrayList<>();
            try {
                if (connection != null) {
                    Statement statement = connection.createStatement();
                    ResultSet resultSet = statement.executeQuery(query);

                    while (resultSet.next()) {
                        String title = resultSet.getString("Название");
                        String author = resultSet.getString("Автор");
                        String genre = resultSet.getString("Жанр");
                        String publisher = resultSet.getString("Издательство");
                        int year = resultSet.getInt("Год выпуска");
                        int pageCount = resultSet.getInt("Число страниц");
                        String language = resultSet.getString("Язык книги");
                        byte[] cover = resultSet.getBytes("Обложка");
                        boolean availability = resultSet.getBoolean("Доступность");
                        String description = resultSet.getString("Краткое описание");
                        int popularity = resultSet.getInt("Как часто брали");

                        Book book = new Book(title, author, genre, publisher, year, pageCount, language, availability, cover, description, popularity);
                        bookList.add(book);
                    }

                    resultSet.close();
                    statement.close();
                    if (listener != null) {
                        listener.onQueryResponse(bookList);
                    }
                } else {
                    Log.e("DatabaseMgr", "Connection is null");
                    new Handler(context.getMainLooper()).post(() -> {
                        Toast.makeText(context, "Нет соединения", Toast.LENGTH_SHORT).show();
                    });
                }
            } catch (SQLException e) {
                Log.e("DatabaseMgr", "Error executing query: " + e.getMessage());
            }

            new Handler(context.getMainLooper()).post(() -> {
                progressBar.setVisibility(View.GONE);
            });
        });
    }

    private String getPreference(String key, String defaultValue) {
        SharedPreferences sharedPreferences = context.getSharedPreferences("settings", Context.MODE_PRIVATE);
        return sharedPreferences.getString(key, defaultValue);
    }

    public void connectToRemoteDatabase() {
        final int TIMEOUT_SECONDS = 60;
        progressBar.setVisibility(View.VISIBLE);
        Executor executor = Executors.newSingleThreadExecutor();
        executor.execute(() -> {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);

            try {
                connection = null;
                String ip = getPreference("ip", "192.168.100.3");
                String port = getPreference("port", "1433");
                String db = getPreference("db", "Библиотека");
                String username = getPreference("username", "Guest");
                String password = getPreference("password", "1");

                Class.forName("net.sourceforge.jtds.jdbc.Driver");
                String connectURL = "jdbc:jtds:sqlserver://" + ip + ":" + port + ";databaseName=" + db + ";user=" + username + ";password=" + password + ";";

                DriverManager.setLoginTimeout(TIMEOUT_SECONDS);
                connection = DriverManager.getConnection(connectURL);
            } catch (SQLException | ClassNotFoundException e) {
                Log.e("DatabaseMgr", "Error: " + e.getMessage());
            }

            new Handler(context.getMainLooper()).post(() -> {
                if (connection == null) {
                    if (connectionListener != null) {
                        connectionListener.onConnectionNOTEstablished();
                    }
                } else {
                    if (connectionListener != null) {
                        connectionListener.onConnectionEstablished();
                    }
                }
                progressBar.setVisibility(View.GONE);
            });
        });
    }


}