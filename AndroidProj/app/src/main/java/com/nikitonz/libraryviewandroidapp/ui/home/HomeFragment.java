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

import java.util.ArrayList;
import java.util.List;

public class HomeFragment extends Fragment {
    private RecyclerView recyclerViewBooks;
    private EditText searchEditText;
    private BookAdapter bookAdapter;
    private List<Book> allBooks;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        recyclerViewBooks = root.findViewById(R.id.recyclerViewBooks);
        searchEditText = root.findViewById(R.id.searchEditText);

        recyclerViewBooks.setLayoutManager(new GridLayoutManager(getContext(), 2));
        DatabaseMgr db = new DatabaseMgr(getContext());
        allBooks = db.getAllBooks();
        bookAdapter = new BookAdapter(allBooks, getContext());
        recyclerViewBooks.setAdapter(bookAdapter);

        searchEditText.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void afterTextChanged(Editable s) {
                String searchText = s.toString().toLowerCase().trim();
                filterBooks(searchText);

                // Проверяем, пуст ли поисковый запрос
                if (searchText.isEmpty()) {
                    TextView popularTextView = getActivity().findViewById(R.id.populartextView);
                    if (popularTextView != null) {
                        popularTextView.setVisibility(View.VISIBLE);
                    }
                } else {

                    TextView popularTextView = getActivity().findViewById(R.id.populartextView);
                    if (popularTextView != null) {
                        popularTextView.setVisibility(View.GONE);
                    }
                }
            }
        });

        return root;
    }

    private void filterBooks(String searchText) {
        List<Book> filteredBooks = new ArrayList<>();
        for (Book book : allBooks) {
            if (book.getTitle().toLowerCase().contains(searchText.toLowerCase()) ||
                    book.getAuthor().toLowerCase().contains(searchText.toLowerCase()) ||
                    book.getPublisher().toLowerCase().contains(searchText.toLowerCase()) ||
                    String.valueOf(book.getYear()).contains(searchText)) {
                filteredBooks.add(book);
            }
        }
        bookAdapter.filterList(filteredBooks);
    }
}