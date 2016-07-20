-- Library Method SQL library


-- LIBRARIAN  VIEW BOOKS PAGE   /librarian/books
-- Book.GetTitles(int authorId)
SELECT books.title  FROM
authors JOIN books_authors  ON (authors.id = books_authors.author_id)
	JOIN books ON ( books_authors.book_id = books.id )
WHERE authors.id = 10

-- books.GetAuthors(int book_id)
SELECT authors.name FROM
books JOIN books_authors ON (books.id = books_authors.book_id)
	JOIN authors ON (books_authors.author_id = authors.id)
WHERE books.id = 4

-- copy.GetCopies(int book_id)
-- method called by book.GetCountCopies()
SELECT * FROM copies WHERE book_id = 1


-- LIBRARIAN HOME PAGE
--Public List<book> SearchByAuthor(string author_name)  /librarian

SELECT books.* FROM
authors JOIN books_authors ON (authors.id = books_authors.author_id)
	JOIN books ON (books_authors.book_id = books.id)
WHERE authors.name = 'Hunt, Andrew'

-- public List<Book> SearchByTitle (string bookTitle)
Select books.title FROM books


SELECT books.* FROM


WHERE copies.due_date < '2016-07-10'

SELECT * FROM books_authors

-- public string GetTitle(int copy_id)
SELECT books.title FROM
copies JOIN books ON (copies.book_id = books.id)
WHERE copies.id = 3

-- public string GetPatronName(int copy_id)
SELECT patrons.name FROM
copies JOIN checkouts ON (copies.id = checkouts.copy_id)
	JOIN patrons ON (checkouts.patron_id = patrons.id)

WHERE copies.id = 1

-- PATRON BOOK RESULTS   /patron/books
-- public List<Copy> GetAvailableCopies(int copy_id)
SELECT *  FROM copies WHERE copies.id = 2 AND copies.due_date = '1900-01-01'

-- public List<Copy> GetCheckedOutCopies(int copyId)
-- Use this method to get all check out items, then in C#, sort list and return the first date in an ascending list
SELECT *  FROM copies WHERE copies.id = 1 AND copies.due_date > '1900-01-01'


-- public void CheckOutCopy(int copyId)
UPDATE copies SET due_date = '2016-07-31' WHERE id = 2