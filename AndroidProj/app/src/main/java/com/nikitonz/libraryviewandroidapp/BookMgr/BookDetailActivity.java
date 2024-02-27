package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.os.Bundle;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import com.nikitonz.libraryviewandroidapp.R;

public class BookDetailActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book_detail);


        String title = getIntent().getStringExtra("book_title");
        String author = getIntent().getStringExtra("book_author");
        int year = getIntent().getIntExtra("book_year", 0);


        TextView titleTextView = findViewById(R.id.titleTextView);
        TextView authorTextView = findViewById(R.id.authorTextView);
        TextView yearTextView = findViewById(R.id.yearTextView);

        titleTextView.setText(title);
        authorTextView.setText(author);
        yearTextView.setText(String.valueOf(year));
    }
}
