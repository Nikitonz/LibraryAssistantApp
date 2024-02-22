package com.nikitonz.libraryviewandroidapp.BookMgr;

public class Book {
        private String title;
        private String author;
        private String genre;
        private String publisher;
        private int year;
        private int pageCount;
        private String language;

        private int cover;
        private boolean available;


        public Book(String title, String author, String genre, String publisher, int year, int pageCount, String language, boolean available, int cover) {
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.publisher = publisher;
            this.year = year;
            this.pageCount = pageCount;
            this.language = language;
            this.available = available;
            this.cover = cover;
        }

        public String getTitle() {
            return title;
        }

        public void setTitle(String title) {
            this.title = title;
        }

        public String getAuthor() {
            return author;
        }

        public void setAuthor(String author) {
            this.author = author;
        }

        public String getGenre() {
            return genre;
        }

        public void setGenre(String genre) {
            this.genre = genre;
        }

        public String getPublisher() {
            return publisher;
        }

        public void setPublisher(String publisher) {
            this.publisher = publisher;
        }

        public int getYear() {
            return year;
        }

        public void setYear(int year) {
            this.year = year;
        }

        public int getPageCount() {
            return pageCount;
        }

        public void setPageCount(int pageCount) {
            this.pageCount = pageCount;
        }

        public String getLanguage() {
            return language;
        }

        public void setLanguage(String language) {
            this.language = language;
        }

        public boolean isAvailable() {
            return available;
        }

        public void setAvailable(boolean available) {
            this.available = available;
        }

        public int getCover() {
            return cover;
        }

        public void setCover(int cover) {
            this.cover = cover;
        }


}