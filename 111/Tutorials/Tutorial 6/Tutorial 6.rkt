;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-advanced-reader.ss" "lang")((modname |Tutorial 6|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #t #t none #f () #f)))
;;;
;;; EECS-111 Tutorial 6
;;;

;;
;; Part 1
;;

(define (min-max-recursive-list x)
  (local [(define found-one? false)
          (define max 0)
          (define min 0)
          (define (recurse x)
            (if (number? x)
                (if found-one?
                    (begin (when (> x max)
                             (set! max x))
                           (when (< x min)
                             (set! min x)))
                    (begin (set! found-one? true)
                           (set! max x)
                           (set! min x)))
                (for-each recurse x)))]
    (begin (recurse x)
           (list min max))))

(check-expect (min-max-recursive-list '(1 3 1 (4 1 2 (5))))
              (list 1 5))

(check-expect (min-max-recursive-list '(1 3 1 (4 1 -2 (5))))
              (list -2 5))

;;
;; Part 2
;;

(define (sum-list-imper lst)
  (local [(define num 0)]
    (begin
      (for-each (lambda (element)
                  (set! num (+ num element))) lst) num)))

(check-expect (sum-list-imper (list 1 2 3 4)) 10)

(define (fold-and-map folder start mapper lst)
  (local [(define temp-lst empty)
          (define num start)]
    (begin
      (for-each (lambda (element)
                  (set! temp-lst (cons (mapper element) temp-lst))) lst)
      (set! temp-lst (reverse temp-lst))
      (for-each (lambda (element)
                  (set! num (folder element num))) temp-lst)
      num)))

;(fold-and-map + 0 abs (list 1 -2 3 -4))

(define (flat-no-dup-list lst)
  (local [(define temp-lst empty)]
    (begin
      (for-each (lambda (element)
                  (if (not (list? element))
                      (set! temp-lst (cons element temp-lst))
                      (set! temp-lst (append temp-lst (flat-no-dup-list element))))) lst)
      (
      temp-lst)))

(flat-no-dup-list (list 1 1 2 2 3 (list 5 5 6 7 8) 3 6 8))

;;
;; Part 3
;;

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


