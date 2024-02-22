package com.nikitonz.libraryviewandroidapp.BookMgr;

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

    public BookAdapter(List<Book> bookList) {
        this.bookList = bookList;
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
        holder.yearIzdTextView.setText(String.valueOf(book.getYear()+", "+book.getPublisher()));


        holder.readMoreButton.setOnClickListener(v -> {
            Toast.makeText(v.getContext(), "DEBUG:clicked", Toast.LENGTH_LONG).show();
            holder.populartextView.setVisibility(View.VISIBLE);



        });
    }

    @Override
    public int getItemCount() {
        return bookList.size();
    }

    static class BookViewHolder extends RecyclerView.ViewHolder {
        TextView titleTextView;
        ImageView coverImageView;
        TextView authorTextView;
        TextView yearIzdTextView;
        Button readMoreButton;
        TextView populartextView;
        public BookViewHolder(@NonNull View itemView) {
            super(itemView);
            titleTextView = itemView.findViewById(R.id.titleTextView);
            coverImageView = itemView.findViewById(R.id.coverImageView);
            authorTextView = itemView.findViewById(R.id.authorTextView);
            yearIzdTextView = itemView.findViewById(R.id.yearIzdTextView);
            readMoreButton = itemView.findViewById(R.id.bReadMore);
            populartextView = itemView.findViewById(R.id.populartextView);
        }
    }
}
