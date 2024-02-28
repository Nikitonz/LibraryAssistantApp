package com.nikitonz.libraryviewandroidapp.ui.home;

import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;

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
    private TextView popularTextView;
    private BookAdapter bookAdapter;
    private List<Book> allBooks;
    private DatabaseMgr db;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        recyclerViewBooks = root.findViewById(R.id.recyclerViewBooks);
        searchEditText = root.findViewById(R.id.searchEditText);
        popularTextView = root.findViewById(R.id.popularTextView);

        recyclerViewBooks.setLayoutManager(new GridLayoutManager(getContext(), 2));
        db = new DatabaseMgr(getContext());

        allBooks = db.getBooks();
        bookAdapter = new BookAdapter(allBooks, getContext());
        recyclerViewBooks.setAdapter(bookAdapter);

        searchEditText.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {}

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {}

            @Override
            public void afterTextChanged(Editable s) {
                filterBooks(s.toString());
            }
        });

        return root;
    }

    private void filterBooks(String searchText) {
        if (searchText.isEmpty()) {
            popularTextView.setVisibility(View.VISIBLE);
            allBooks = db.getBooks();
        } else {
            popularTextView.setVisibility(View.GONE);
            allBooks = db.getBooks(searchText);
        }
        bookAdapter.setBooks(allBooks);
    }
}