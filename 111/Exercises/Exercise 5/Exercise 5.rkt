;; The first three lines of this file were inserted by DrRacket. They record metadata
;; about the language level of this file in a form that our tools can easily process.
#reader(lib "htdp-advanced-reader.ss" "lang")((modname |Exercise 5|) (read-case-sensitive #t) (teachpacks ()) (htdp-settings #(#t constructor repeating-decimal #t #t none #f () #f)))
(require "file-operations.rkt")

(define (copy-tree from to) 
  (begin (unless (directory-exists? to)
           (make-directory to))
         (for-each (λ (file)
                     (begin (printf "Copying file ~A to ~A~n" file to)
                            (copy-file file
                                       (build-path to
                                                   (path-filename file))
                                       true)))
                   (directory-files from))
         (for-each (λ (subdir)
                     (begin (printf "Copying directory ~A to ~A~n" subdir to)
                            (copy-tree subdir
                                       (build-path to
                                                   (path-filename subdir)))))
                   (directory-subdirectories from))))


;(copy-tree "test" "output")

(define (backup from to) 
  (begin (unless (directory-exists? to)
           (make-directory to))
         (for-each (λ (file)
                     (local [(define new-path (build-path to (path-filename file)))]
                     (begin (unless (and (file-exists? new-path)
                                         (< (file-or-directory-modify-seconds file) (file-or-directory-modify-seconds new-path)))                                                      
                              (printf "Copying file ~A to ~A~n" file to))
                            (unless (and (file-exists? new-path)
                                         (< (file-or-directory-modify-seconds file) (file-or-directory-modify-seconds new-path)))
                              (copy-file file
                                         new-path
                                         true)))))
                   (directory-files from))
         (for-each (λ (subdir)
                     (begin (printf "Copying directory ~A to ~A~n" subdir to)
                            (backup subdir
                                       (build-path to
                                                   (path-filename subdir)))))
                   (directory-subdirectories from))))

;(backup "test" "output")

(define (for-each-recur proc lst)
  (begin
    (cond
      [(empty? lst) empty]
      [else (cons (proc (first lst)) (for-each-recur proc (rest lst)))])))

;(for-each-recur abs (list -1 -2 -3))

(define (backup-without-for-each from to) 
  (begin (unless (directory-exists? to)
           (make-directory to))
         (for-each-recur (λ (file)
                     (local [(define new-path (build-path to (path-filename file)))]
                     (begin (unless (and (file-exists? new-path)
                                         (< (file-or-directory-modify-seconds file) (file-or-directory-modify-seconds new-path)))                                                      
                              (printf "Copying file ~A to ~A~n" file to))
                            (unless (and (file-exists? new-path)
                                         (< (file-or-directory-modify-seconds file) (file-or-directory-modify-seconds new-path)))
                              (copy-file file
                                         new-path
                                         true)))))
                   (directory-files from))
         (for-each-recur (λ (subdir)
                     (begin (printf "Copying directory ~A to ~A~n" subdir to)
                            (backup-without-for-each subdir
                                       (build-path to
                                                   (path-filename subdir)))))
                   (directory-subdirectories from))))

;(backup-without-for-each "test" "output")

(define (count-files pth)
  (+ (length (directory-files pth))
     (foldl + 0 (map count-files (directory-subdirectories pth)))))

;(count-files "output")

(define (directory-size pth)
  (+ (foldl + 0 (map file-size (directory-files pth)))
     (foldl + 0 (map directory-size (directory-subdirectories pth)))))

;(directory-size "test")

(define (search-directory str pth)
  (filter (lambda (file) (string-contains? str (path-filename file))) (append (directory-files pth)
          (foldl append empty (map (lambda (dir) (search-directory str dir)) (directory-subdirectories pth))))))

;(search-directory "blah" "output")

(define (filter-directory pred pth)
  (filter (lambda (file) (pred file)) (append (directory-files pth)
          (foldl append empty (map (lambda (dir) (filter-directory pred dir)) (directory-subdirectories pth))))))

;(filter-directory (lambda (file) (string-contains? "blah" (path-filename file))) "output")

(define (find-file-type exten dir)
  (filter-directory (lambda (pth) (path-has-extension? pth exten)) dir))

;(find-file-type ".txt" "output")

(define (file-type-disk-usage exten dir)
  (foldl + 0 (map file-size (find-file-type exten dir))))

;(file-type-disk-usage ".txt" "output")