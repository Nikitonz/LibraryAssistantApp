package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.content.Intent;
import android.os.Bundle;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import com.nikitonz.libraryviewandroidapp.R;



public class BookDetailActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book_detail);

        Intent intent = getIntent();
        if (intent != null) {
            try {
                String title = intent.getStringExtra("title");
                String author = intent.getStringExtra("author");
                String yearIzdPublisher = intent.getStringExtra("year") + ", " + intent.getStringExtra("publisher");
                String genre = intent.getStringExtra("genre");
                String langPageCount = intent.getStringExtra("language") + ", " + intent.getStringExtra("pageCount");
                String description = intent.getStringExtra("description");

                TextView titleTextView = findViewById(R.id.titleTextView);
                TextView authorTextView = findViewById(R.id.authorTextView);
                TextView genreTextView = findViewById(R.id.genreTextView);
                TextView yearTextView = findViewById(R.id.yearIzdTextView);
                TextView langPageCounttextView = findViewById(R.id.langPageCounttextView);
                TextView descriptionTextView = findViewById(R.id.descriptionTextView);

                titleTextView.setText(title);
                authorTextView.setText(author);
                yearTextView.setText(yearIzdPublisher);
                genreTextView.setText(genre);
                langPageCounttextView.setText(langPageCount);
                descriptionTextView.setText(description);
            }
            catch (Exception e) {};
        }
    }
}
