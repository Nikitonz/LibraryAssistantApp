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

    private SQLiteDatabase database;

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

    public DatabaseMgr(Context context) {
        DatabaseHelper dbHelper = new DatabaseHelper(context);
        database = dbHelper.getWritableDatabase();

        if (isEmpty()) {
            addSampleBooks();
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
            values.put("[Обложка]", 0); // Устанавливаем обложку в 0
            values.put("Доступность", 1); // Устанавливаем доступность
            values.put("[Краткое описание]", "Описание книги " + i);
            values.put("[Как часто брали]", i); // Начальное значение популярности
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
        values.put("[Краткое описание]", "Описание книги kj;w;ng;wjergn;wjingf;iwrkejgbn;elirkqjgbn;leirksjbg;qlerikgbqe;lrgjbuneiqar;lgberl;igbnaqe;lrigbujknerq;ilkgujbnei;rlgujbne;rilujgbn;eqarigbneirslgbulnareilgauberligubaerlgaikjuwbergl;ijurebgla;qjkusrewbg lerugbjli;ergubreu ipu gwiubhgiwuebgpwiubgiubg ui bgiuwbgpiuebwrgqpiebugniwrsuebgr");
        values.put("[Как часто брали]", 0);
        database.insert(TABLE_NAME, null, values);
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
        values.put("Доступность", book.isAvailable() ? 1 : 0);
        values.put("[Краткое описание]", book.getDescription());
        values.put("[Как часто брали]", book.getPopularity());
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
        query += " ORDER BY [Как часто брали] DESC";
        return getQueryResponse(query);
    }

    public List<Book> getBooks() {
        String query = "SELECT * FROM " + TABLE_NAME + " ORDER BY [Как часто брали] DESC LIMIT 20";
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
}