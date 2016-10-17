;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname |Exercise 3|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require 2htdp/image)

;;;
;;; Exercise 3: recursion practice
;;; Place your solutions below.
;;;

(define multiply-list
  (lambda (lst)
    (if (empty? lst)
        1
        (* (car lst) (multiply-list (cdr lst))))))

;(multiply-list '(1 2 3 4 5))


(define multiply-list-iter
 (lambda (lst)
   (local [(define helper (lambda (lst num)
                           (if (empty? lst)
                               num
                               (helper (cdr lst) (* num (car lst))))))]
    (helper lst 1))))


;(multiply-list-iter '(1 2 3 4 5))

(define count
  (lambda (pred lst)
    (if (not (empty? lst))
        (if (pred (car lst))
            (+ 1 (count pred (cdr lst)))
            (+ 0 (count pred (cdr lst))))
        0)))

;(count odd? '(1 2 3 4 5 6 7))

(define count-iter
  (lambda (pred lst)
    (local [(define helper  (lambda (pred lst val) (if (not (empty? lst))
        (if (pred (car lst))
            (helper pred (cdr lst) (+ val 1))
            (helper pred (cdr lst) val))
        val)))]
      (helper pred lst 0))))
    

;(count-iter odd? '(1 2 3 4 5 6 7))

(define iterated-overlay
  (lambda (proc num)
    (if (= num 0)
        empty-image
        (overlay (iterated-overlay proc (- num 1)) (proc (- num 1))))))

;(iterated-overlay (λ (n)(square (* n 10) "solid"(color (* n 50)0 0))) 5)

(define sum-recursive-list
  (lambda (lst)
    (if (number? lst)
        lst
        (if (empty? lst)
            0
            (+ (sum-recursive-list (car lst)) (sum-recursive-list (cdr lst)))))))
  

;(sum-recursive-list (list 1 (list 2 (list 3) 4)))

(define max-recursive-list
  (lambda (lst)
    (if (number? lst)
        lst
        (if (empty? lst)
            -9999999999999999999     ;lowest possible number (kludge, should replace)
            (max (max-recursive-list (car lst)) (max-recursive-list (cdr lst)))))))

;(max-recursive-list (list 1 (list 2 (list 3) 4)))
    
(define (depth lst)
  (if (number? lst)
       0
       (if (empty? lst)
           0
           (+ 1 (max (depth (car lst)) (- (depth (cdr lst)) 1))))))

;(depth (list 1 3 (list (list (list (list 1)))) 5 6 (list 7 7 (list 6 6 )) 7))