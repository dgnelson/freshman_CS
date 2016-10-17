;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname |Tutorial 3|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require "remove-duplicates.rkt")

(define friends-db
  (list (list "ben" "jerry")
        (list "martha" "paul")
        (list "hillary" "bernie")
        (list "lizbeth" "mikael")
        (list "edward" "bernie")
        (list "steve" "hillary")
        (list "larry" "edward")
        (list "sheryl" "hillary")
        (list "sheryl" "martha")
        (list "lizbeth" "edward")
        (list "elliot" "lizbeth")
        (list "edward" "elliot")
        (list "lizbeth" "berkoff")
        (list "berkoff" "nikita")))

'( 1 2 4 "I am the walrus")

(define mylist '( 1 2 3 4 5 6 7 8 9))

(define (sum lst)
  (foldl + 0 lst))

(lambda (lst) (apply + lst))

(lambda (lst) (foldl + 0 lst))

(lambda (lst) (map (lambda (n) (* n 3)) lst))

(lambda (lst) (filter (lambda (n) (and (> n 5) (< n 20))) lst))

(define friends?
  (lambda (f1 f2)
    (apply (and (contains? f1) (contains? f2)) friends-db)))
      
