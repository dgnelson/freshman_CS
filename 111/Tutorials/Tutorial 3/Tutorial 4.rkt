;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname |Tutorial 4|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(define (factorial n)
        (if (= n 0)
            1
            (* n (factorial (- n 1)))))

;(factorial 5)

(define (factorial-iter n num)
  (if (= n 0)
      num
      (factorial-iter (- n 1) (* n num))))

;(factorial-iter 5 1)

(define (reverse-list lst)
  (local [(define (do-this remaining reversed)
            (if (empty? remaining)
                
(reverse-list (list 1 2 3 4 5))
