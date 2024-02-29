package com.nikitonz.libraryviewandroidapp.ui.about;

import android.content.ActivityNotFoundException;
import android.content.Intent;
import android.media.MediaPlayer;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Toast;
import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import com.nikitonz.libraryviewandroidapp.R;
import java.io.IOException;

public class AboutFragment extends Fragment {

    private MediaPlayer mediaPlayer;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_about, container, false);

        Button buttonReturn = root.findViewById(R.id.buttonReturn);
        buttonReturn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                getActivity().finish();
            }
        });

        ImageView imageViewGmailTo = root.findViewById(R.id.imageViewEmail);
        imageViewGmailTo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String email = "nikitoniy2468@gmail.com";
                String subject = "MYDYPLOMA Application";
                String body = "";

                Intent intent = new Intent(Intent.ACTION_SENDTO);
                intent.setData(Uri.parse("mailto:" + email));
                intent.putExtra(Intent.EXTRA_SUBJECT, subject);
                intent.putExtra(Intent.EXTRA_TEXT, body);

                try {
                    startActivity(intent);
                } catch (ActivityNotFoundException e) {
                    Intent emailIntent = new Intent(Intent.ACTION_VIEW);
                    emailIntent.setData(Uri.parse("https://mail.google.com/"));

                    try {
                        startActivity(emailIntent);
                    } catch (ActivityNotFoundException ex) {
                        Toast.makeText(getContext(), "Невозможно отправить письмо", Toast.LENGTH_SHORT).show();
                    }
                }
            }
        });

        ImageView imageViewGithubGOTO = root.findViewById(R.id.imageViewGitHub);
        imageViewGithubGOTO.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String githubUrl = "https://github.com/Nikitonz/LibraryAssistantApp";

                Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse(githubUrl));

                try {
                    startActivity(intent);
                } catch (ActivityNotFoundException e) {
                    Toast.makeText(getContext(), "Невозможно открыть GitHub", Toast.LENGTH_SHORT).show();
                }
            }
        });

        final ImageView lolBanImageView = root.findViewById(R.id.lolBan);

        final long audioDuration = getAudioDuration(R.raw.cave2);

        new Handler(Looper.getMainLooper()).postDelayed(new Runnable() {
            @Override
            public void run() {
                final AlphaAnimation fadeIn = new AlphaAnimation(0f, 1f);
                fadeIn.setDuration(audioDuration);

                fadeIn.setAnimationListener(new Animation.AnimationListener() {
                    @Override
                    public void onAnimationStart(Animation animation) {
                        playSound();
                    }

                    @Override
                    public void onAnimationEnd(Animation animation) {
                        lolBanImageView.setVisibility(View.VISIBLE);
                    }

                    @Override
                    public void onAnimationRepeat(Animation animation) {

                    }
                });

                lolBanImageView.startAnimation(fadeIn);
            }
        }, 5000);

        return root;
    }

    private void playSound() {
        mediaPlayer = MediaPlayer.create(getContext(), R.raw.notcave);
        mediaPlayer.start();
    }

    private long getAudioDuration(int audioResId) {
        MediaPlayer mediaPlayer = MediaPlayer.create(getContext(), audioResId);
        long duration = mediaPlayer.getDuration();
        mediaPlayer.release();
        return duration;
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        if (mediaPlayer != null) {
            mediaPlayer.release();
        }
    }
}