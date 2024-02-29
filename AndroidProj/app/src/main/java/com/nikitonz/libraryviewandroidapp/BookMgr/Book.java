package com.nikitonz.libraryviewandroidapp.BookMgr;

public class Book {
    private String title;
    private String author;
    private String genre;
    private String publisher;
    private int year;
    private int pageCount;
    private String language;

    private byte[] cover;
    private boolean available;

    private String description;
    private int popularity;

    public Book(String title, String author, String genre, String publisher, int year, int pageCount, String language, boolean available, byte[] cover, String description, int popularity) {
        this.title = title;
        this.author = author;
        this.genre = genre;
        this.publisher = publisher;
        this.year = year;
        this.pageCount = pageCount;
        this.language = language;
        this.available = available;
        this.cover = cover;
        this.description = description;
        this.popularity = popularity;
    }

    public String getTitle() {
        return title;
    }



    public String getAuthor() {
        return author;
    }



    public String getGenre() {
        return genre;
    }



    public String getPublisher() {
        return publisher;
    }



    public int getYear() {
        return year;
    }



    public int getPageCount() {
        return pageCount;
    }



    public String getLanguage() {
        return language;
    }



    public boolean isAvailable() {
        return available;
    }



    public byte[] getCover() {
        return cover;
    }




    public String getDescription() {
        return description;
    }


    public int getPopularity() {
        return popularity;
    }


}