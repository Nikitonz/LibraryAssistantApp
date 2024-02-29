package com.nikitonz.libraryviewandroidapp.BookMgr;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.nikitonz.libraryviewandroidapp.R;

import java.util.List;

public class BookAdapter extends RecyclerView.Adapter<BookAdapter.BookViewHolder> {
    private List<Book> bookList;
    private final Context context;

    public BookAdapter(List<Book> bookList, Context context) {
        this.bookList = bookList;
        this.context = context;
    }
    public static byte[] hexStringToByteArray(String hexString) {
        int len = hexString.length();
        byte[] data = new byte[len / 2];
        for (int i = 0; i < len; i += 2) {
            data[i / 2] = (byte) ((Character.digit(hexString.charAt(i), 16) << 4)
                    + Character.digit(hexString.charAt(i+1), 16));
        }
        return data;
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

        holder.authorTextView.setText(book.getAuthor());
        holder.yearIzdTextView.setText(String.valueOf(book.getYear() + ", " + book.getPublisher()));

        byte[] coverBytes = book.getCover();
        if (coverBytes != null) {
            Bitmap bitmap = BitmapFactory.decodeByteArray(coverBytes,0,coverBytes.length);
            holder.coverImageView.setImageBitmap(bitmap);
        } else {
            holder.coverImageView.setImageResource(R.drawable.ic_menu_book);
        }

        holder.readMoreButton.setOnClickListener(v -> {
            try {
                Intent intent = new Intent(context, BookDetailActivity.class);
                intent.putExtra("title", book.getTitle());
                intent.putExtra("author", book.getAuthor());
                intent.putExtra("genre", book.getGenre());
                intent.putExtra("publisher", book.getPublisher());
                intent.putExtra("year", String.valueOf(book.getYear()));
                intent.putExtra("pageCount", String.valueOf(book.getPageCount()));
                intent.putExtra("language", book.getLanguage());
                intent.putExtra("available", book.isAvailable());
                intent.putExtra("cover", book.getCover());
                intent.putExtra("description", book.getDescription());
                context.startActivity(intent);

            }
            catch (Exception e){}

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