--USE library

CREATE TABLE patrons
(
	name VARCHAR(100),
	id INT IDENTITY(1,1)
	)

CREATE TABLE authors
(
	name VARCHAR(100),
	id INT IDENTITY(1,1)
	)

CREATE TABLE books
(
	title VARCHAR(100),
	id INT IDENTITY(1,1)
	)

CREATE TABLE copies
(
	book_id INT,
	due_date datetime,
	id INT IDENTITY(1,1)
	)

CREATE TABLE checkouts
(
	patron_id INT,
	copy_id INT,
	id INT IDENTITY(1,1)
	)

CREATE TABLE books_authors
(
	book_id INT,
	author_id INT,
	id INT IDENTITY(1,1)
	)