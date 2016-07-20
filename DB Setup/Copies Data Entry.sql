USE library

--copies
--checked in
INSERT INTO copies (book_id, due_date) VALUES (1, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (2, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (3, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (4, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (5, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (6, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (7, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (8, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (8, '1900-01-01')
INSERT INTO copies (book_id, due_date) VALUES (10, '1900-01-01')

--checked out -- NOT overdue
INSERT INTO copies (book_id, due_date) VALUES (1, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (2, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (2, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (2, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (5, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (6, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (6, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (8, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (7, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (9, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (9, '2016-07-31')
INSERT INTO copies (book_id, due_date) VALUES (9, '2016-07-31')

--checked out -- OVERDUE
INSERT INTO copies (book_id, due_date) VALUES (1, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (1, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (3, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (1, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (5, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (6, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (8, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (8, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (8, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (7, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (7, '2016-07-07')
INSERT INTO copies (book_id, due_date) VALUES (10, '2016-07-07')





SELECT * FROM copies


--UPDATE copies SET due_date = '2016-07-07' WHERE id = 1