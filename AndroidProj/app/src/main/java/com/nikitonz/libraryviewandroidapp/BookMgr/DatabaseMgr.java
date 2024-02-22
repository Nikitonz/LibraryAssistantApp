package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.nikitonz.libraryviewandroidapp.R;

import java.util.ArrayList;
import java.util.List;

public class DatabaseMgr {
    private static final String DATABASE_NAME = "books.db";
    private static final int DATABASE_VERSION = 1;

    private static final String TABLE_NAME = "books";
    private static final List<String> COLUMNS = new ArrayList<>();

    static {
        COLUMNS.add("title TEXT");
        COLUMNS.add("author TEXT");
        COLUMNS.add("genre TEXT");
        COLUMNS.add("publisher TEXT");
        COLUMNS.add("year INTEGER");
        COLUMNS.add("pagecount INTEGER");
        COLUMNS.add("language TEXT");
        COLUMNS.add("cover INTEGER");
        COLUMNS.add("available INTEGER");
    }

    private SQLiteDatabase database;

    private static class DatabaseHelper extends SQLiteOpenHelper {
        DatabaseHelper(Context context) {
            super(context, DATABASE_NAME, null, DATABASE_VERSION);
        }

        @Override
        public void onCreate(SQLiteDatabase db) {
            StringBuilder createTableQuery = new StringBuilder("CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (");
            for (int i = 0; i < COLUMNS.size(); i++) {
                createTableQuery.append(COLUMNS.get(i));
                if (i < COLUMNS.size() - 1) {
                    createTableQuery.append(", ");
                }
            }
            createTableQuery.append(")");
            db.execSQL(createTableQuery.toString());


            Cursor cursor = db.rawQuery("SELECT COUNT(*) FROM " + TABLE_NAME, null);
            if (cursor != null) {
                cursor.moveToFirst();
                int count = cursor.getInt(0);
                cursor.close();


                if (count == 0) {
                    addSampleBooks(db);
                }
            }
        }

        private void addSampleBooks(SQLiteDatabase db) {
            for (int i = 1; i <= 5; i++) {
                ContentValues values = new ContentValues();
                values.put("title", "Book " + i);
                values.put("author", "Author " + i);
                values.put("genre", "Genre " + i);
                values.put("publisher", "Publisher " + i);
                values.put("year", 2022 + i); // Пример года
                values.put("pagecount", 200 + i * 50); // Пример числа страниц
                values.put("language", "English"); // Пример языка
                values.put("cover", R.drawable.ic_launcher_background); // Пример обложки
                values.put("available", 1); // Пример доступности

                db.insert(TABLE_NAME, null, values);
            }
        }

        @Override
        public void onUpgrade(SQLiteDatabase sqLiteDatabase, int i, int i1) {

        }


    }

    public DatabaseMgr(Context context) {
        DatabaseHelper dbHelper = new DatabaseHelper(context);
        database = dbHelper.getWritableDatabase();
    }

    public void addBook(Book book) {
        ContentValues values = new ContentValues();
        values.put("title", book.getTitle());
        values.put("author", book.getAuthor());
        values.put("genre", book.getGenre());
        values.put("publisher", book.getPublisher());
        values.put("year", book.getYear());
        values.put("pagecount", book.getPageCount());
        values.put("language", book.getLanguage());
        values.put("cover", book.getCover());
        values.put("available", book.isAvailable() ? 1 : 0);
        database.insert(TABLE_NAME, null, values);
    }

    public List<Book> getAllBooks() {
        List<Book> bookList = new ArrayList<>();
        Cursor cursor = database.rawQuery("SELECT * FROM " + TABLE_NAME, null);
        if (cursor != null && cursor.moveToFirst()) {
            do {
                int titleIndex = cursor.getColumnIndex("title");
                int authorIndex = cursor.getColumnIndex("author");
                int genreIndex = cursor.getColumnIndex("genre");
                int publisherIndex = cursor.getColumnIndex("publisher");
                int yearIndex = cursor.getColumnIndex("year");
                int pageCountIndex = cursor.getColumnIndex("pagecount");
                int languageIndex = cursor.getColumnIndex("language");
                int availableIndex = cursor.getColumnIndex("available");
                int coverIndex = cursor.getColumnIndex("cover");

                if (titleIndex != -1 && authorIndex != -1 && genreIndex != -1 && publisherIndex != -1 &&
                        yearIndex != -1 && pageCountIndex != -1 && languageIndex != -1 &&
                        availableIndex != -1 && coverIndex != -1) {

                    String title = cursor.getString(titleIndex);
                    String author = cursor.getString(authorIndex);
                    String genre = cursor.getString(genreIndex);
                    String publisher = cursor.getString(publisherIndex);
                    int year = cursor.getInt(yearIndex);
                    int pageCount = cursor.getInt(pageCountIndex);
                    String language = cursor.getString(languageIndex);
                    boolean available = cursor.getInt(availableIndex) == 1;
                    int cover = cursor.getInt(coverIndex);

                    Book book = new Book(title, author, genre, publisher, year, pageCount, language, available, cover);
                    bookList.add(book);
                }
            } while (cursor.moveToNext());
            cursor.close();
        }
        return bookList;
    }

    // Другие методы для работы с базой данных
}