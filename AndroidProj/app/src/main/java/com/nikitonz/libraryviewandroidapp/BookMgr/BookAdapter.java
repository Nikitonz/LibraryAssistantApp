package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.nikitonz.libraryviewandroidapp.R;

import java.util.List;

public class BookAdapter extends RecyclerView.Adapter<BookAdapter.BookViewHolder> {
    private List<Book> bookList;
    private Context context;

    public BookAdapter(List<Book> bookList, Context context) {
        this.bookList = bookList;
        this.context = context;
    }

    @NonNull
    @Override
    public BookViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_book, parent, false);
        return new BookViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull BookViewHolder holder, int position) {
        Book book = bookList.get(position);
        holder.titleTextView.setText(book.getTitle());
        holder.coverImageView.setImageResource(book.getCover());
        holder.authorTextView.setText(book.getAuthor());
        holder.yearIzdTextView.setText(String.valueOf(book.getYear() + ", " + book.getPublisher()));

        holder.readMoreButton.setOnClickListener(v -> {


            Intent intent = new Intent(context, BookDetailActivity.class);
            //intent.putExtra();
            context.startActivity(intent);
        });
    }

    @Override
    public int getItemCount() {
        return bookList.size();
    }

    public void setBooks(List<Book> books) {
        this.bookList = books;
        notifyDataSetChanged();
    }

    static class BookViewHolder extends RecyclerView.ViewHolder {
        TextView titleTextView;
        ImageView coverImageView;
        TextView authorTextView;
        TextView yearIzdTextView;
        Button readMoreButton;

        public BookViewHolder(@NonNull View itemView) {
            super(itemView);
            titleTextView = itemView.findViewById(R.id.titleTextView);
            coverImageView = itemView.findViewById(R.id.coverImageView);
            authorTextView = itemView.findViewById(R.id.authorTextView);
            yearIzdTextView = itemView.findViewById(R.id.yearIzdTextView);
            readMoreButton = itemView.findViewById(R.id.bReadMore);
        }
    }
}