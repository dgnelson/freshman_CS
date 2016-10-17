#lang racket

;;;
;;; Path and filesystem operations for using in EECS-111
;;; These aren't included in the student languages, so we have
;;; to explicitly provide them.
;;;

;; Export standard Racket stuff
(provide path? path-string? path->string string->path build-path path-has-extension?
         file-exists? file-or-directory-modify-seconds file-size copy-file rename-file-or-directory
         delete-file delete-directory
         directory-exists? make-directory
         directory-files directory-subdirectories)

;; Special stuff defined here
(provide path-directory path-filename)


(define (path-directory path)
  (call-with-values (λ () (split-path path))
                    (λ (directory filename must-be-dir?)
                      directory)))

(define (path-filename path)
  (call-with-values (λ () (split-path path))
                    (λ (directory filename must-be-dir?)
                      (path->string filename))))

(define (directory-subdirectories path)
  (filter directory-exists?
          (directory-list path #:build? #t)))

(define (directory-files path)
  (filter file-exists?
          (directory-list path #:build? #t)))

(define (path-has-extension? path extension)
  (local [(define string (if (string? path)
                                 path
                                 (path->string path)))
          (define l (string-length string))
          (define ext-length (string-length extension))
          (define maybe-extension
            (if (< ext-length l)
                (substring string (- l ext-length) l)
                ""))]
    (string=? extension maybe-extension)))

;(path-has-extension? (current-directory) ".jpg")
;(path-has-extension? (build-path (current-directory)
;                                 "foo.jpg")
;                     ".jpg")
