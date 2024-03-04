package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Handler;
import android.os.StrictMode;
import android.util.Log;
import android.widget.Toast;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class DatabaseMgr {
    private Connection connection;
    private Context context;
    public DatabaseMgr(Context c) {
        this.context=c;
        connectToRemoteDatabase();
    }

    public List<Book> getBooks(String searchTerm) {
        String query = "USE Библиотека; SELECT * FROM ТаблицаДляМобилок('"+searchTerm+"')";
        //if (searchCriteria != null && !searchCriteria.isEmpty())
        return getQueryResponse(query);
    }

    public List<Book> getBooks() {
        String query = "USE Библиотека; SELECT TOP(20) * FROM ТаблицаДляМобилок('')";
        return getQueryResponse(query);
    }

    private List<Book> getQueryResponse(String query) {
        List<Book> bookList = new ArrayList<>();
        try {
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
        } catch (Exception e) {
            Log.e("AT RECEIVING", e.toString());
        }
        return bookList;
    }

    private String getPreference(String key, String defaultValue) {
        SharedPreferences sharedPreferences = context.getSharedPreferences("settings", Context.MODE_PRIVATE);
        return sharedPreferences.getString(key, defaultValue);
    }
    @SuppressLint("NewApi")
    public void connectToRemoteDatabase() {
        String ip = getPreference("ip", "192.168.100.3");
        String port = getPreference("port", "1433");
        String db = getPreference("db", "Библиотека");
        String username = getPreference("username", "Guest");
        String password = getPreference("password", "1");
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);
        try {
            Class.forName("net.sourceforge.jtds.jdbc.Driver");
            String connectURL = "jdbc:jtds:sqlserver://" + ip + ":" + port + ";databaseName=" + db + ";user=" + username + ";password=" + password + ";";
            connection = DriverManager.getConnection(connectURL);
        } catch (SQLException | ClassNotFoundException e) {
            Log.e("AT CONN", e.toString());
        }

        if (connection != null) {
            Toast.makeText(context, "Connected", Toast.LENGTH_SHORT).show();
        } else {
            Toast.makeText(context, "NOT Connected", Toast.LENGTH_SHORT).show();
        }
    }

    public void fetchBooksInBackground() {
        new Thread(new Runnable() {
            @Override
            public void run() {
                // Выполните ваш код, требующий длительного времени, здесь

                // После завершения выполнения долгой операции, обновите пользовательский интерфейс
                new Handler(context.getMainLooper()).post(new Runnable() {
                    @Override
                    public void run() {
                        // Обновление пользовательского интерфейса здесь
                    }
                });
            }
        }).start();
    }


}

