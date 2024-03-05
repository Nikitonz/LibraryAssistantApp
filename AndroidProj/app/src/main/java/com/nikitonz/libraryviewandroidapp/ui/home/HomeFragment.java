package com.nikitonz.libraryviewandroidapp.ui.home;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ProgressBar;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.localbroadcastmanager.content.LocalBroadcastManager;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.nikitonz.libraryviewandroidapp.BookMgr.Book;
import com.nikitonz.libraryviewandroidapp.BookMgr.BookAdapter;
import com.nikitonz.libraryviewandroidapp.BookMgr.DatabaseMgr;
import com.nikitonz.libraryviewandroidapp.BookMgr.OnConnectionEstablishedListener;
import com.nikitonz.libraryviewandroidapp.R;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

public class HomeFragment extends Fragment{
    private TextView popularTextView;
    private RecyclerView recyclerViewBooks;
    private BookAdapter bookAdapter;
    private List<Book> allBooks = new ArrayList<>();
    private DatabaseMgr db;

    private ProgressBar progressBar;
    private final BroadcastReceiver updateReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            if (intent != null && intent.getAction() != null && intent.getAction().equals("UPDATE_DATABASE")) {

                updateData();
            }
        }
    };

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        recyclerViewBooks = root.findViewById(R.id.recyclerViewBooks);
        popularTextView = root.findViewById(R.id.popularTextView);
        EditText searchEditText = root.findViewById(R.id.searchEditText);

        recyclerViewBooks.setLayoutManager(new GridLayoutManager(getContext(), 2));
        ImageButton bStartSearch = root.findViewById(R.id.bStartSearch);
        bStartSearch.setOnClickListener(v -> {
            String searchText = searchEditText.getText().toString();
            filterBooks(searchText);
        });
        searchEditText.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {}

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {}

            @Override
            public void afterTextChanged(Editable s) {
                if (s.toString().isEmpty()) {
                    popularTextView.setVisibility(View.VISIBLE);
                    filterBooks("");
                } else {
                    popularTextView.setVisibility(View.GONE);
                }
            }
        });
        progressBar = root.findViewById(R.id.progressBar);
        db = new DatabaseMgr(getContext(), progressBar);
        bookAdapter = new BookAdapter(allBooks, getContext());
        recyclerViewBooks.setAdapter(bookAdapter);
        updateData();
        registerUpdateReceiver();


        return root;
    }
    @Override
    public void onDestroyView() {
        super.onDestroyView();
        unregisterUpdateReceiver();
    }


    private void registerUpdateReceiver() {
        IntentFilter intentFilter = new IntentFilter("UPDATE_DATABASE");
        LocalBroadcastManager.getInstance(requireContext()).registerReceiver(updateReceiver, intentFilter);
    }


    private void unregisterUpdateReceiver() {
        LocalBroadcastManager.getInstance(requireContext()).unregisterReceiver(updateReceiver);
    }
    private void loadBooks() {
        db.getBooks(bookList -> {
            allBooks = bookList;
            Objects.requireNonNull(getActivity()).runOnUiThread(() -> bookAdapter.setBooks(allBooks));
        });
    }

    private void filterBooks(String searchText) {
        if (searchText.isEmpty()) {
            db.getBooks(bookList -> {

                allBooks = bookList;
                Objects.requireNonNull(getActivity()).runOnUiThread(() -> bookAdapter.setBooks(allBooks));
            });
        } else {
            db.getBooks(searchText, bookList -> {

                allBooks = bookList;
                Objects.requireNonNull(getActivity()).runOnUiThread(() -> bookAdapter.setBooks(allBooks));
            });
        }
    }



    public void updateData() {


        db.connectToRemoteDatabase();

        db.setOnConnectionEstablishedListener(new OnConnectionEstablishedListener() {
            @Override
            public void onConnectionEstablished() {
                loadBooks();
            }

            @Override
            public void onConnectionNOTEstablished() {

                allBooks.clear();
                if (bookAdapter != null) {
                    Objects.requireNonNull(getActivity()).runOnUiThread(() -> bookAdapter.setBooks(allBooks));
                }
            }
        });


    }

}