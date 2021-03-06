;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname |Exercise 2|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
;;;;
;;;; Exercise 2: A simple database
;;;; EECS-111 Fall 2015
;;;;

(require "remove-duplicates.rkt")

;; This defines the basic album datatype.
;; It provides you with make-album, album-title,
;; album-artist, and album-genre
(define-struct album (title artist genre))

;;;
;;; Enter a list of albums below
;;; They need not be the actual albums you own.
;;; But you should include enough variety to adequately
;;; test your code.
;;;
;;; Here's what we mean.  One of the questions involves
;;; writing a procedure that finds all the albums of a
;;; given genre.  If all the albums in the database are
;;; in the rock genre, then there's only one genre and
;;; when you ask for all the rock albums and it gives
;;; back all the albums, you don't know whether that's
;;; because the code really works, or because it's
;;; not even paying attention to the genre.  So you want
;;; to make sure there are multiple artists and genres,
;;; some artists with only one album or genre, others
;;; with multiple artists or genres, etc.
;;;

;(define database
  ;; Fill in the info below
  ;(list (make-album "After the Disco" "Broken Bells" "Alternative")
   ;     (make-album "An Awesome Wave" "Alt-J" "Alternative")
    ;    (make-album "Broken Bells" "Broken Bells" "Alternative")
     ;   (make-album "Franz Ferdinand" "Franz Ferdinand" "Punk")
      ;  (make-album "Chase This Light" "Jimmy Eat World" "Punk")
       ; (make-album "Blled American" "Jimmy Eat World" "Alternative")
        ;(make-album "Neighborhoods" "Blink-182" "Punk")
;        (make-album "Tonight" "Franz Ferdinand" "Punk")
 ;       (make-album "Contra" "Vampire Weekend" "Indie")
  ;      (make-album "Costello Music" "The Fratellis" "Indie")
   ;     (make-album "Is This It" "The Strokes" "Indie")
    ;    (make-album "Angles" "The Strokes" "Alternative")
     ;   (make-album "Room On Fire" "The Strokes" "Alternative")
      ;  (make-album "Vampire Weekend" "Vampire Weekend" "Indie")
       ; ))

;;;
;;; Add the procedures you write (e.g. all-genres, versatile-artists)
;;; below.  Be sure to test your procedures to make sure they work.
;;; We are not providing test cases this time, so it's up to you
;;; to make sure your code works.  We will use our own test cases
;;; when grading and assign you a grade based on the number of
;;; test cases that passed.
;;;

(define all-titles
  (lambda (db)
    (foldl (lambda (album titles) (cons (album-title album) titles)) '() db)))

(define all-artists
 (lambda (db)
  (remove-duplicates
     (foldl (lambda (album artists) (cons (album-artist album) artists)) '() db))))

(define all-genres
 (lambda (db)
  (remove-duplicates
     (foldl (lambda (album genres) (cons (album-genre album) genres)) '() db))))

(define artist-albums
  (lambda (artist db)
    (all-titles
     (filter (lambda (album) (equal? artist (album-artist album))) db))))

(define artist-genres
 (lambda (artist db)
   (all-genres
     (filter (lambda (album) (equal? artist (album-artist album))) db))))

(define artist-is-versatile?
  (lambda (artist db)
    (> (length (artist-genres artist db)) 1)))

(define versatile-artists
  (lambda (db)
    (all-artists
     (filter (lambda (album) (artist-is-versatile? (album-artist album) db)) db))))

(define artist-album-counts
  (lambda (db)
    (map
     (lambda (artist)
       (list artist (length (artist-albums artist db)))) (all-artists db))))

(define genre-albums
  (lambda (genre db)
    (all-titles
     (filter (lambda (album) (equal? genre (album-genre album))) db))))

(define genre-album-counts
  (lambda (db)
    (map
     (lambda (genre)
       (list genre (length (genre-albums genre db)))) (all-genres db))))

(define most-prolific-artist
  (lambda (db)
    (foldl
          (lambda (artist best)
            (if (equal? best "empty")
                artist
            (if (> (length (artist-albums artist db)) (length (artist-albums best db)))
                 artist
                 best)))
            "empty"
            (all-artists db))))

(define in:test-db
  (list (make-album "Bangerz"
                    "Miley Cyrus"
                    "Pop")
        (make-album "Hannah Montana"
                    "Miley Cyrus"
                    "Teen")
        (make-album "Believe"
                    "Justin Bieber"
                    "Pop")
        (make-album "First"
                    "Dr. Frump"
                    "Pop")
        (make-album "Second"
                    "Dr. Frump"
                    "Indie Rock")
        (make-album "Pamyu Pamyu Revolution"
                    "Kyary Pamyu Pamyu"
                    "J-Pop")
        (make-album "Z"
                    "My Morning Jacket"
                    "Indie Rock")
        (make-album "Hello"
                    "My Morning Jacket"
                    "Indie Rock")
        (make-album "Goodbye"
                    "My Morning Jacket"
                    "Indie Rock")))

;; tests
(check-expect (all-titles in:test-db)
              '("Bangerz" "Hannah Montana" "Believe" "First" "Second"
                          "Pamyu Pamyu Revolution" "Z" "Hello" "Goodbye"))

(check-expect (all-artists in:test-db)
              '("Miley Cyrus" "Justin Bieber" "Dr. Frump" "Kyary Pamyu Pamyu" "My Morning Jacket"))

(check-expect (all-genres in:test-db)
              '("Pop" "Teen" "Indie Rock" "J-Pop"))

(check-expect (map album-title (artist-albums "Miley Cyrus" in:test-db))
              '("Bangerz" "Hannah Montana"))

(check-expect (artist-genres "Miley Cyrus" in:test-db)
              '("Pop" "Teen"))

(check-expect (artist-is-versatile? "Miley Cyrus" in:test-db)
              #t)

(check-expect (artist-is-versatile? "My Morning Jacket" in:test-db)
              #f)

(check-expect (versatile-artists in:test-db)
              '("Miley Cyrus" "Dr. Frump"))

(check-expect (artist-album-counts in:test-db)
              '(("Miley Cyrus" 2) ("Justin Bieber" 1) ("Dr. Frump" 2)
                                  ("Kyary Pamyu Pamyu" 1) ("My Morning Jacket" 3)))

(check-expect (genre-album-counts in:test-db)
              '(("Pop" 3) ("Teen" 1) ("Indie Rock" 4) ("J-Pop" 1)))

;(when (advanced?) ;; this test runs only if youre in the advance class
 ; (check-expect (most-prolific-artist in:test-db)
  ;              "My Morning Jacket"))