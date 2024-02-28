package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.widget.Toast;

import com.nikitonz.libraryviewandroidapp.R;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class DatabaseMgr {

    private static class DatabaseHelper extends SQLiteOpenHelper {
        DatabaseHelper(Context context) {
            super(context, DATABASE_NAME, null, DATABASE_VERSION);
        }

        @Override
        public void onCreate(SQLiteDatabase db) {
            db.execSQL("CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "[Название книги] TEXT, " +
                    "[Автор] TEXT, " +
                    "[Жанр] TEXT, " +
                    "[Издательство] TEXT, " +
                    "[Год выпуска] INTEGER, " +
                    "[Число страниц] INTEGER, " +
                    "[Язык книги] TEXT, " +
                    "[Обложка] INTEGER, " +
                    "'Доступность' INTEGER, " +
                    "[Краткое описание] TEXT, " +
                    "[Как часто брали] INTEGER)");
        }

        @Override
        public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
            db.execSQL("DROP TABLE IF EXISTS " + TABLE_NAME);
            onCreate(db);
        }
    }
    private static final String DATABASE_NAME = "books.db";
    private static final int DATABASE_VERSION = 1;
    private static final String TABLE_NAME = "books";

    private SQLiteDatabase database;
    private Connection connection;

    public DatabaseMgr(Context context) {
        DatabaseHelper dbHelper = new DatabaseHelper(context);
        database = dbHelper.getWritableDatabase();
        connectToRemoteDatabase(context.getApplicationContext());

        if (isEmpty()) {
            addSampleBooks();
        }



        if (connection != null) {
            synchronizeLocalDatabaseWithRemote();
        }
    }








    private boolean isEmpty() {
        Cursor cursor = database.rawQuery("SELECT COUNT(*) FROM " + TABLE_NAME, null);
        int count = 0;
        if (cursor != null) {
            cursor.moveToFirst();
            count = cursor.getInt(0);
            cursor.close();
        }
        return count == 0;
    }

    private void addSampleBooks() {
        for (int i = 1; i <= 20; i++) {
            ContentValues values = new ContentValues();
            values.put("[Название книги]", "Книга " + i);
            values.put("[Автор]", "Автор " + i);
            values.put("[Жанр]", "Жанр " + i);
            values.put("[Издательство]", "Издательство " + i);
            values.put("[Год выпуска]", 2022 + i);
            values.put("[Число страниц]", 200 + i * 50);
            values.put("[Язык книги]", "Английский");
            values.put("[Обложка]", 0);
            values.put("Доступность", 1);
            values.put("[Краткое описание]", "Описание книги " + i);
            values.put("[Как часто брали]", i);
            database.insert(TABLE_NAME, null, values);
        }

        ContentValues values = new ContentValues();
        values.put("[Название книги]", "Книга " );
        values.put("[Автор]", "Автор ");
        values.put("[Жанр]", "Жанр " );
        values.put("[Издательство]", "Издательство " );
        values.put("[Год выпуска]", 2022 );
        values.put("[Число страниц]", 200 );
        values.put("[Язык книги]", "Английский");
        values.put("[Обложка]", R.drawable.ic_launcher_background);
        values.put("Доступность", 1);
        values.put("[Краткое описание]", "Описание книги kj;w;ng;wjueацуоатцутуошатцуаз ташзщ   уоцагшщзитуй2   ашщзг аш и  гшаруашщ груашгщ    шщагрцу ашгрцуашгцура шгур шщгцуарш щгцура шгцурацушгарцушагрцуагцр ацшгур ацшуг ацуршгар цугарц ушгарцугшарцушгарцшугарцушгарцушгарцуша цугар цугша цруагш цруагшцур ацугшар цущгшарцущгшарцущшгар цущшагцурщагцур ашгцуа цушагрцушагцруашщцуращшцурацщшугарцушгарцшугарцшугарцушгарцушга gr");
        values.put("[Как часто брали]", 0);
        database.insert(TABLE_NAME, null, values);
    }



    public List<Book> getBooks(String searchCriteria) {
        String query = "SELECT * FROM " + TABLE_NAME;
        if (searchCriteria != null && !searchCriteria.isEmpty()) {
            query += " WHERE " +
                    "[Название книги] LIKE '%" + searchCriteria + "%' OR " +
                    "[Автор] LIKE '%" + searchCriteria + "%' OR " +
                    "[Жанр] LIKE '%" + searchCriteria + "%' OR " +
                    "[Издательство] LIKE '%" + searchCriteria + "%'";
        }
        query += " ORDER BY [Название книги] ASC";
        return getQueryResponse(query);
    }

    public List<Book> getBooks() {
        String query = "SELECT * FROM " + TABLE_NAME + " ORDER BY [Название книги] ASC LIMIT 20";
        return getQueryResponse(query);
    }

    private List<Book> getQueryResponse(String query) {
        List<Book> bookList = new ArrayList<>();
        Cursor cursor = database.rawQuery(query, null);
        if (cursor != null && cursor.moveToFirst()) {
            do {
                int titleIndex = cursor.getColumnIndex("Название книги");
                String title = titleIndex != -1 ? cursor.getString(titleIndex) : "";
                int authorIndex = cursor.getColumnIndex("Автор");
                String author = authorIndex != -1 ? cursor.getString(authorIndex) : "";
                int genreIndex = cursor.getColumnIndex("Жанр");
                String genre = genreIndex != -1 ? cursor.getString(genreIndex) : "";
                int publisherIndex = cursor.getColumnIndex("Издательство");
                String publisher = publisherIndex != -1 ? cursor.getString(publisherIndex) : "";
                int yearIndex = cursor.getColumnIndex("Год выпуска");
                int year = yearIndex != -1 ? cursor.getInt(yearIndex) : 0;
                int pageCountIndex = cursor.getColumnIndex("Число страниц");
                int pageCount = pageCountIndex != -1 ? cursor.getInt(pageCountIndex) : 0;
                int languageIndex = cursor.getColumnIndex("Язык книги");
                String language = languageIndex != -1 ? cursor.getString(languageIndex) : "";
                int coverIndex = cursor.getColumnIndex("Обложка");
                int cover = coverIndex != -1 ? cursor.getInt(coverIndex) : 0;
                int availableIndex = cursor.getColumnIndex("Доступность");
                boolean available = availableIndex != -1 && cursor.getInt(availableIndex) == 1;
                int descriptionIndex = cursor.getColumnIndex("Краткое описание");
                String description = descriptionIndex != -1 ? cursor.getString(descriptionIndex) : "";
                int popularityIndex = cursor.getColumnIndex("Как часто брали");
                int popularity = popularityIndex != -1 ? cursor.getInt(popularityIndex) : 0;

                Book book = new Book(title, author, genre, publisher, year, pageCount, language, available, cover, description, popularity);
                bookList.add(book);
            } while (cursor.moveToNext());
            cursor.close();
        }
        return bookList;
    }



    private void connectToRemoteDatabase(Context context) {
        String url = "jdbc:sqlserver://127.0.0.1:1433;databaseName=Библиотека";
        String username = "Guest";
        String password = "1";

        try {
            connection = DriverManager.getConnection(url, username, password);
        } catch (SQLException e) {
            e.printStackTrace();
            connection = null;
            Toast.makeText(context, "Cannot establish connection", Toast.LENGTH_SHORT).show();
        }
    }

    private void synchronizeLocalDatabaseWithRemote() {
        new Thread(() -> {
            if (connection != null) {
                try {
                    Statement statement = connection.createStatement();
                    ResultSet resultSet = statement.executeQuery("SELECT * FROM your_view_name");

                    createNewLocalTable("new_books");
                    fillNewLocalTable(resultSet);

                    boolean tablesAreEqual = checkTablesEquality();
                    if (tablesAreEqual) {
                        dropNewLocalTable();
                    } else {
                        dropMainLocalTable();
                        renameNewLocalTable();
                    }
                    resultSet.close();
                    statement.close();
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
        }).start();
    }

    private void createNewLocalTable(String tablename) throws SQLException {

        String createTableQuery = "CREATE TABLE IF NOT EXISTS " + tablename + " (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "[Название книги] TEXT, " +
                "[Автор] TEXT, " +
                "[Жанр] TEXT, " +
                "[Издательство] TEXT, " +
                "[Год выпуска] INTEGER, " +
                "[Число страниц] INTEGER, " +
                "[Язык книги] TEXT, " +
                "[Обложка] INTEGER, " +
                "'Доступность' INTEGER, " +
                "[Краткое описание] TEXT, " +
                "[Как часто брали] INTEGER)";
        database.execSQL(createTableQuery);
    }

    private void fillNewLocalTable(ResultSet resultSet) throws SQLException {

        while (resultSet.next()) {
            String title = resultSet.getString("title");
            String author = resultSet.getString("author");

            String insertQuery = "INSERT INTO new_books ([Название книги], [Автор], ...) VALUES " +
                    "('" + title + "', '" + author + "', ...)";
            database.execSQL(insertQuery);
        }
    }

    private boolean checkTablesEquality() {

        String countMainTableQuery = "SELECT COUNT(*) FROM " + TABLE_NAME;
        String countNewTableQuery = "SELECT COUNT(*) FROM new_books";

        Cursor cursorMainTable = database.rawQuery(countMainTableQuery, null);
        Cursor cursorNewTable = database.rawQuery(countNewTableQuery, null);

        int countMain = 0, countNew = 0;
        if (cursorMainTable.moveToFirst()) {
            countMain = cursorMainTable.getInt(0);
        }
        if (cursorNewTable.moveToFirst()) {
            countNew = cursorNewTable.getInt(0);
        }

        cursorMainTable.close();
        cursorNewTable.close();

        return countMain == countNew;
    }

    private void dropNewLocalTable() throws SQLException {

        String dropQuery = "DROP TABLE IF EXISTS new_books";
        database.execSQL(dropQuery);
    }

    private void dropMainLocalTable() throws SQLException {

        String dropQuery = "DROP TABLE IF EXISTS " + TABLE_NAME;
        database.execSQL(dropQuery);
    }

    private void renameNewLocalTable() throws SQLException {

        String renameQuery = "ALTER TABLE new_books RENAME TO " + TABLE_NAME;
        database.execSQL(renameQuery);
    }
}