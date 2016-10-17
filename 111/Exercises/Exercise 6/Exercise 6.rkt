;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-advanced-reader.ss" "lang")((modname |Exercise 6|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require 2htdp/image)
(require 2htdp/universe)
(require "struct-inheritance.rkt")

(define-struct bounding-box (x1 y1 x2 y2))

(define-struct shape (posn))

(define-struct (rect shape) (width height)
  #:methods
  (define (move self v)
    (create-rectangle
     (posn+ (shape-posn self) v) (rect-width self) (rect-height self)))
  (define (render self scene)
    (place-image
     (rectangle (rect-width self) (rect-height self) "outline" "black") (posn-x (shape-posn self)) (posn-y (shape-posn self)) scene))
  (define (get-bounding-box self)
    (make-bounding-box
     (- (posn-x (shape-posn self)) (/ (rect-width self) 2))
     (- (posn-y (shape-posn self)) (/ (rect-height self) 2))
     (+ (posn-x (shape-posn self)) (/ (rect-width self) 2))
     (+ (posn-y (shape-posn self)) (/ (rect-height self) 2)))))

(define-struct (circ shape) (radius)
  #:methods
  (define (move self v)
    (create-circle
     (posn+ (shape-posn self) v) (circ-radius self)))
  (define (render self scene)
    (place-image
     (circle (circ-radius self) "outline" "black") (posn-x (shape-posn self)) (posn-y (shape-posn self)) scene))
  (define (get-bounding-box self)
    (make-bounding-box
     (- (posn-x (shape-posn self)) (circ-radius self))
     (- (posn-y (shape-posn self)) (circ-radius self))
     (+ (posn-x (shape-posn self)) (circ-radius self))
     (+ (posn-y (shape-posn self)) (circ-radius self)))))

(define-struct (tri shape) (side-length)
  #:methods
  (define (move self v)
    (create-triangle
     (posn+ (shape-posn self) v) (tri-side-length self)))
  (define (render self scene)
    (place-image
     (triangle (tri-side-length self) "outline" "black") (posn-x (shape-posn self)) (posn-y (shape-posn self)) scene))
  (define (get-bounding-box self)
    (make-bounding-box
     (- (posn-x (shape-posn self)) (* (tri-side-length self) (/ 1 2)))
     (- (posn-y (shape-posn self)) (* (/ 2 3) (tri-side-length self) (cos (/ pi 6))))
     (+ (posn-x (shape-posn self)) (* (tri-side-length self) (/ 1 2)))
     (+ (posn-y (shape-posn self)) (* (/ 1 3) (tri-side-length self) (cos (/ pi 6)))))))

(define (create-circle pos radius)
  (make-circ pos radius))

(define (create-rectangle pos width height)
  (make-rect pos width height))

(define (create-triangle pos side-length)
  (make-tri pos side-length))

;;;;;;;;;; game engine ;;;;;;;;;;;;;;;;;;

;; (make-world (listof wrapped-game-object) (listof wrapped-game-object))
(define-struct world (free collided))

(define-struct wrapped-game-object (inner velocity)
  #:methods
  (define (render self scene)
    (render (wrapped-game-object-inner self) scene))
  (define (move self pos)
    (make-wrapped-game-object
     (move (wrapped-game-object-inner self) pos)
     (wrapped-game-object-velocity self)))
  (define (get-bounding-box self)
    (get-bounding-box (wrapped-game-object-inner self))))

;;;; draw
;; (listof object) -> image
(define (draw world)
  (local
   [(define objects (world-free world))
    (define collided (world-collided world))
    (define basic (foldl render (empty-scene 500 500) objects))
    (define +collided (foldl render+box basic collided))]
   +collided))

(define (render+box obj scene)
  (local
   [(define pos (shape-posn (wrapped-game-object-inner obj)))]
   (place-image
    (render-bb (get-bounding-box obj))
    (posn-x pos)
    (posn-y pos)
    (render obj scene))))

(define (render-bb bb)
  (rectangle (- (bounding-box-x2 bb) (bounding-box-x1 bb))
             (- (bounding-box-y2 bb) (bounding-box-y1 bb))
             "outline" "black"))

;;;; tick
(define-struct two-values (left right))

;; world -> world
(define (tick world)
  (local
   [(define tv (delete-collisions (move-objects (world-free world))))]
   (make-world
    (two-values-left tv)
    (append (two-values-right tv) (world-collided world)))))

;; (listof object) -> void
(define (move-objects objects)
  (map move-object objects))

(define (move-object o)
  (local
   [(define o* (wrapped-game-object-inner o))
    (define v (wrapped-game-object-velocity o))
    (define v* (update-velocity v))]
   (make-wrapped-game-object (move o* v*) v*)))

(define gravity (make-posn 0 (* 2 0.0098)))
(define terminal-velocity 1)
(define (update-velocity v)
  (if (>= (posn-y v) terminal-velocity)
      v
      (posn+ v gravity)))

;;;; collisions

;; (listof object) -> (listof object)
(define (delete-collisions objects)
  (filter/split (lambda (o) (not (collides? o objects)))
                objects))

(define (filter/split f l)
  (local
   [(define (loop lst left right)
      (cond [(empty? lst)
             (make-two-values left right)]
            [else
             (if (f (first lst))
                 (loop (rest lst) (cons (first lst) left) right)
                 (loop (rest lst) left (cons (first lst) right)))]))]
   (loop l empty empty)))

;; object (listof object) -> boolean
(define (collides? o objects)
  (ormap (lambda (o2)
           (and (not (eq? o o2))
                (intersects? o o2)))
         objects))

;; object object -> boolean
(define (intersects? o o2)
  (local
   [(define 1-b (get-bounding-box o))
    (define 2-b (get-bounding-box o2))]
   (or
    (bb-intersects? 1-b 2-b)
    (bb-intersects? 2-b 1-b) )))

;; bounding-box bounding-box -> boolean
(define (bb-intersects? 1-b 2-b)
  (and (or (within? (bounding-box-x1 1-b) (bounding-box-x1 2-b) (bounding-box-x2 2-b))
           (within? (bounding-box-x2 1-b) (bounding-box-x1 2-b) (bounding-box-x2 2-b)))
       (or (within? (bounding-box-y1 1-b) (bounding-box-y1 2-b) (bounding-box-y2 2-b))
           (within? (bounding-box-y2 1-b) (bounding-box-y1 2-b) (bounding-box-y2 2-b)))))

(check-expect
 (bb-intersects?
  (make-bounding-box 7 7 12 12)
  (make-bounding-box 5 5 15 15))
 #t)

;; number number number -> boolean
(define (within? a b c)
  (<= b a c))

;;;; mouse
(define (click world x y evt)
  (cond [(string=? "button-down" evt)
         (local
          [(define v (random-velocity))
           (define type (random))
           (define pos (make-posn x y))]
          (make-world
           (cons
            (make-wrapped-game-object
             (cond
               [(< type .3)
                (create-circle pos (r3))]
               [(> type .6)
                (create-rectangle pos (r3) (r3))]
               [else (create-triangle pos (r3))])
             v)
            (world-free world))
           (world-collided world)))]
        [else world]))

(define (random-velocity)
  (make-posn (r2) (r2)))

(define (r3)
  (+ 10 (random 40)))
(define (r2)
  (- (* (random) 2) 0.5))

;;;; game loop

(define (start)
  (big-bang (make-world empty empty)
            (on-tick tick)
            (on-draw draw)
            (on-mouse click)))

;;;; aux

(define (posn+ a b)
  (make-posn (+ (posn-x a)
                (posn-x b))
             (+ (posn-y a)
                (posn-y b))))

(define (posn* p s)
  (make-posn (* s (posn-x p))
             (* s (posn-y p))))

(define (posn- a b)
  (posn+ a (posn* b -1)))

(check-expect
 (bb-intersects?
  (make-bounding-box 5 5 15 15)
  (make-bounding-box 5 1 15 11))
 #t)

;(start)