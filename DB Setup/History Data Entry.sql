USE library

--adam patrons_books
INSERT INTO patrons_books (patron_id, book_id) VALUES (1, 4)
INSERT INTO patrons_books (patron_id, book_id) VALUES (1, 8)
INSERT INTO patrons_books (patron_id, book_id) VALUES (1, 7)
INSERT INTO patrons_books (patron_id, book_id) VALUES (1, 5)
INSERT INTO patrons_books (patron_id, book_id) VALUES (1, 2)

--Jim
INSERT INTO patrons_books (patron_id, book_id) VALUES (2, 6)
INSERT INTO patrons_books (patron_id, book_id) VALUES (2, 5)
INSERT INTO patrons_books (patron_id, book_id) VALUES (2, 1)
INSERT INTO patrons_books (patron_id, book_id) VALUES (2, 4)
INSERT INTO patrons_books (patron_id, book_id) VALUES (2, 3)



SELECT * FROM patrons_books
