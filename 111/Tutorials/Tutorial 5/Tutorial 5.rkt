;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-advanced-reader.ss" "lang")((modname |Tutorial 5|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #t #t none #f () #f)))
;;;
;;; Tutorial 5
;;;

(require 2htdp/image)
(require 2htdp/universe)

;; Part 1

(define balance 100)

(define (deposit! x)
  (begin (set! balance (+ balance x))))

(define (withdraw! x)
  (if (< (- balance x) 0)
      (error "Insufficient Balance")
      (begin (set! balance (- balance x)))))

;; Part 2


(define (imperative-foldl proc val lst)
  (local [(define num val)]
    (begin
      (for-each (lambda (element)
                  (set! num (proc element num))) lst)
      num)))

;(imperative-foldl + 0 (list 1 2 3 4 5 6))

(define (reverse-list lst1)
  (local [(define lst2 empty)]
    (begin
      (for-each (lambda (element)
                  (set! lst2 (cons element lst2)))
                lst1) lst2)))

;(reverse-list (list 1 2 3 4 5))
                
;; Part 3



;; Part 4
(define (key-pressed key)
  ;; Fill this in
  null)




;;; Don't modify the code below.

(define the-text "")
(define quit? false)
(define (edit-text)
  ;; My apologies to the authors of big-bang for taking their nice functional
  ;; simulator framework and using it in a completely imperative manner.
  (begin (set! quit? false)
         (big-bang null
                   (stop-when (λ (ignore) quit?))
                   (on-key (λ (ignore key)
                             (begin (key-pressed key)
                                    "")))
                   (on-draw (λ (state)
                              (overlay (text the-text 24 "green")
                                       (rectangle 300 50 "solid" "black")))))
         the-text))