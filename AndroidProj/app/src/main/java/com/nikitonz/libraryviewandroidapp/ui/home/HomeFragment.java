package com.nikitonz.libraryviewandroidapp.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.nikitonz.libraryviewandroidapp.BookMgr.Book;
import com.nikitonz.libraryviewandroidapp.BookMgr.BookAdapter;
import com.nikitonz.libraryviewandroidapp.BookMgr.DatabaseMgr;
import com.nikitonz.libraryviewandroidapp.R;

import java.util.List;

public class HomeFragment extends Fragment {
    private RecyclerView recyclerViewBooks;
    private EditText searchEditText;
    private BookAdapter bookAdapter;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        recyclerViewBooks = root.findViewById(R.id.recyclerViewBooks);
        searchEditText = root.findViewById(R.id.searchEditText);


        recyclerViewBooks.setLayoutManager(new GridLayoutManager(getContext(), 2));
        DatabaseMgr db = new DatabaseMgr(getContext());
        List<Book> books = db.getAllBooks();
        bookAdapter = new BookAdapter(books);
        recyclerViewBooks.setAdapter(bookAdapter);

        return root;
    }
}