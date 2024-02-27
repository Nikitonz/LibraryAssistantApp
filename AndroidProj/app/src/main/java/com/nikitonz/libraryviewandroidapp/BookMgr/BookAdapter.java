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

import java.util.ArrayList;
import java.util.List;

public class BookAdapter extends RecyclerView.Adapter<BookAdapter.BookViewHolder> {
    private List<Book> bookList;
    private List<Book> filteredList;
    private Context context;

    public BookAdapter(List<Book> bookList, Context context) {
        this.bookList = bookList;
        this.filteredList = new ArrayList<>(bookList);
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
        Book book = filteredList.get(position);
        holder.titleTextView.setText(book.getTitle());
        holder.coverImageView.setImageResource(book.getCover());
        holder.authorTextView.setText(book.getAuthor());
        holder.yearIzdTextView.setText(String.valueOf(book.getYear() + ", " + book.getPublisher()));

        holder.readMoreButton.setOnClickListener(v -> {
            TextView popularTextView = ((Activity) context).findViewById(R.id.populartextView);
            if (popularTextView != null) {
                popularTextView.setVisibility(popularTextView.getVisibility() == View.VISIBLE ? View.GONE : View.VISIBLE);
            } else {
                Toast.makeText(context, "TextView not found", Toast.LENGTH_SHORT).show();
            }

            Intent intent = new Intent(context, BookDetailActivity.class);
            intent.putExtra("book_title", book.getTitle());
            intent.putExtra("book_author", book.getAuthor());
            intent.putExtra("book_year", book.getYear());

            context.startActivity(intent);
        });
    }

    @Override
    public int getItemCount() {
        return filteredList.size();
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

    public void filterList(List<Book> filteredBooks) {
        filteredList.clear();
        if (filteredBooks.isEmpty()) {
            filteredList.addAll(bookList);
        } else {
            filteredList.addAll(filteredBooks);
        }
        notifyDataSetChanged();
    }
}