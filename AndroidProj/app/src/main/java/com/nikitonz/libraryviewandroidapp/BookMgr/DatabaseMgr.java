package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.annotation.SuppressLint;
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
    private static final List<String> COLUMNS = new ArrayList<String>() {{
        add("[Название книги] TEXT");
        add("[Автор] TEXT");
        add("[Жанр] TEXT");
        add("[Издательство] TEXT");
        add("[Год выпуска] INTEGER");
        add("[Число страниц] INTEGER");
        add("[Язык книги] TEXT");
        add("[Обложка] INTEGER");
        add("'Доступность' INTEGER");
    }};

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
                values.put("[Название книги]", "Книга " + i);
                values.put("[Автор]", "Автор " + i);
                values.put("[Жанр]", "Жанр " + i);
                values.put("[Издательство]", "Издательство " + i);
                values.put("[Год выпуска]", 2022 + i);
                values.put("[Число страниц]", 200 + i * 50);
                values.put("[Язык книги]", "Английский");
                values.put("[Обложка]", R.drawable.ic_launcher_background);
                values.put("[Доступность]", 1);
                db.insert(TABLE_NAME, null, values);

            }
            ContentValues values = new ContentValues();
            values.put("[Название книги]", "Книга ");
            values.put("[Автор]", "Автор ");
            values.put("[Жанр]", "Жанр ");
            values.put("[Издательство]", "Издательство ");
            values.put("[Год выпуска]", 2022);
            values.put("[Число страниц]", 1);
            values.put("[Язык книги]", "Английский");

            values.put("[Доступность]", 1);
            db.insert(TABLE_NAME, null, values);
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
        values.put("[Название книги]", book.getTitle());
        values.put("[Автор]", book.getAuthor());
        values.put("[Жанр]", book.getGenre());
        values.put("[Издательство]", book.getPublisher());
        values.put("[Год выпуска]", book.getYear());
        values.put("[Число страниц]", book.getPageCount());
        values.put("[Язык книги]", book.getLanguage());
        values.put("[Обложка]", book.getCover());
        values.put("[Доступность]", book.isAvailable() ? 1 : 0);
        database.insert(TABLE_NAME, null, values);
    }

    public List<Book> getAllBooks() {
        List<Book> bookList = new ArrayList<>();
        Cursor cursor = database.rawQuery("SELECT * FROM " + TABLE_NAME, null);
        if (cursor != null && cursor.moveToFirst()) {
            do {
                @SuppressLint("Range") String title = cursor.getString(cursor.getColumnIndex("Название книги"));
                @SuppressLint("Range") String author = cursor.getString(cursor.getColumnIndex("Автор"));
                @SuppressLint("Range") String genre = cursor.getString(cursor.getColumnIndex("Жанр"));
                @SuppressLint("Range") String publisher = cursor.getString(cursor.getColumnIndex("Издательство"));
                @SuppressLint("Range") int year = cursor.getInt(cursor.getColumnIndex("Год выпуска"));
                @SuppressLint("Range") int pageCount = cursor.getInt(cursor.getColumnIndex("Число страниц"));
                @SuppressLint("Range") String language = cursor.getString(cursor.getColumnIndex("Язык книги"));
                @SuppressLint("Range") int available = cursor.getInt(cursor.getColumnIndex("Доступность"));
                @SuppressLint("Range") int cover = cursor.getInt(cursor.getColumnIndex("Обложка"));

                Book book = new Book(title != null ? title : "", author != null ? author : "", genre != null ? genre : "",
                        publisher != null ? publisher : "", year != -1 ? year : 0, pageCount != -1 ? pageCount : 0,
                        language != null ? language : "", available != -1 ? available == 1 : false, cover != -1 ? cover : 0);
                bookList.add(book);
            } while (cursor.moveToNext());
            cursor.close();
        }
        return bookList;
    }
}