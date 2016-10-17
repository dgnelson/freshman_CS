;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname |Tutorial 2|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
(require "shading.rkt")

;;;
;;; Examples
;;;

(define gradient
  (λ (p)
    (point-x p)))

;(render gradient)

;; Note: if you think about it, gradient is actually the same as just point-x
;; so you can also just say (render point-x).

(define sin-grating
  (λ (p)
        (* 255
           (abs (sin (* 0.1
                        (+ (point-x p)
                           (point-y p))))))))
;(render sin-grating)

(define shader (lambda (p) (point-x p)))

(define brighten
  (lambda (shader mult)
    (lambda (p)
      (* mult (shader p)))))

(define (invert shader)
  (lambda (p)
    (- 255 (shader p))))

(define (invert2 shader)
  (lambda (p)
    (abs (- 255 (shader p)))))

;(render (brighten shader 0.3))
(render shader)
(render (invert2 shader))
