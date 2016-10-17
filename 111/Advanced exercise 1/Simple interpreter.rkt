;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-intermediate-lambda-reader.ss" "lang")((modname |Simple interpreter|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #f #t none #f () #f)))
;;;
;;; EECS-111 Advanced Section Exercise 1
;;; A simple interpreter
;;;

(define (eval exp environment)
  (cond [(symbol? exp)
         (lookup exp
                 environment)]
        [(list? exp)
         (eval-complex-expression exp environment)]
        [else
         exp]))

(define (eval-complex-expression exp environment)
  (local [(define spec-exp (first exp))
          (define rest-spec-exp (rest exp))]
    (cond [(eq? spec-exp 'if)
           (eval-if rest-spec-exp environment)]
          [(eq? spec-exp 'cond)
           (eval-cond rest-spec-exp environment)]
          [(eq? spec-exp 'local)
           (eval-local rest-spec-exp environment)]
          [else
           (eval-procedure-call (map (λ (subexp)
                                       (eval subexp
                                             environment))
                                     exp))])))

(define (eval-if exp environment)
  (if (eval (first exp) environment)
      (eval (first (rest exp)) environment)
      (eval (first (rest (rest exp))) environment)))

(define (eval-cond exp environment)
  (local [(define first-exp (first exp))]
    (cond [(eq? (first first-exp) 'else)
           (eval (first (rest first-exp)) environment)]
          [(eval (first (first exp)) environment)
           (eval (first (rest first-exp)) environment)]
          [else
           (eval-cond (rest exp) environment)])))

(define (eval-local exp environment)
  (local [(define defs (first exp))
          (define new-envir (make-environment-from-definitions defs environment))]
    (eval (first (rest exp)) new-envir)))

(define (make-environment-from-definitions defs environ)
  (append (map definition->binding defs) environ))
    
(define (eval-procedure-call proc-and-args)
  (apply (first proc-and-args)
         (rest proc-and-args)))

(define (lookup variable-name
                environment)
  (local [(define binding
            (assoc variable-name environment))]
    (if (list? binding)
        (second binding)
        (error "Undefined variable"
               variable-name))))

(define default-environment
  (list (list '+ +)
        (list '- -)
        (list '* *)
        (list '/ /)
        (list 'odd? odd?)
        (list 'even? even?)
        (list 'positive? positive?)
        (list 'negative? negative?)))

(define (definition->binding definition)
  (if (not (eq?
            (first definition)
            'define))
      (error "Invalid definition"
             definition)
      (list (second definition)
            (eval
             (third definition)
             default-environment))))

(define (program definitions)
  (append (map definition->binding
               definitions)
          default-environment))


;TEST CASES

;(eval '(+ n (* a b))
 ;     (program '((define n
  ;                 7)
   ;              (define a 4)
    ;             (define b 3))))

;(eval '(if (even? (+ 1 4)) 5 6) default-environment)

;(eval '(cond [#f 1][else (+ 1 2)]) default-environment)

;(eval '(local [(define m 1) (define s 1)] (+ 1 m s)) default-environment)

;(first (first (rest '(cond (else 2)))))

;'(cond [#f 1][else 5])