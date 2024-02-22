package com.nikitonz.libraryviewandroidapp.ui.about;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class AboutViewModel extends ViewModel {

    private final MutableLiveData<String> mText = new MutableLiveData<>();;

    public AboutViewModel() {


    }

    public LiveData<String> getText() {
        return mText;
    }
}