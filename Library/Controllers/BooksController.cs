using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System;

namespace Library.Controllers
{
    public class BooksController : Controller
    {

        [HttpGet("/books")]
        public ActionResult Index()
        {
            List<Book> allBooks = Book.GetAll();
            return View(allBooks);
        }


        [HttpGet("/books/new")]
        public ActionResult New()
        {
            return View();
        }


        // [HttpPost("/books")]
        // public ActionResult Create(string bookName)
        // {
        //     Book newBook = new Book(bookName);
        //     newBook.Save();
            
        //     List<Book> allBooks = Book.GetAll();
        //     return View("Index", allBooks);
        // }

        //create new book, author and title both
        [HttpPost("/books")]
        public ActionResult Create(string bookName, string bookAuthor)
        {

            //check for duplicates, add book title:
            bool isSameName = false;
            string tempName = "";
            List<Book> allBooks = Book.GetAll();
            foreach(Book book in allBooks)
            {
                tempName = book.GetName();
                if (tempName == bookName)
                {
                    isSameName = true; 
                }
            }
            if (isSameName == false)
            {
                Book newBook = new Book(bookName);
                newBook.Save();
            
            
            //check for duplicates, add book author:
            bool isSameAuthor = false;
            string tempAuthor = "";
            List<Author> allAuthors = Author.GetAll();
            foreach(Author author in allAuthors)
            {
                tempAuthor = author.GetName();
                if (tempAuthor == bookAuthor)
                {
                    isSameAuthor = true; 
                }
            }
            if (isSameAuthor == false)
            {
                Author newAuthor = new Author(bookAuthor);
                newAuthor.Save();
                newBook.AddAuthor(newAuthor);
            }
            }
            Copy newCopy = new Copy(bookName, bookAuthor, 2);
            newCopy.Save();


            return RedirectToAction("Index");
        }

        //show indidividual books 
        [HttpGet("/books/{bookId}")]
        public ActionResult Show(int bookId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Book selectedBook = Book.Find(bookId);
            List<Author> bookAuthors = Book.GetAuthors(bookId);
            List<Author> allAuthors = Author.GetAll();
            model.Add("bookAuthors", bookAuthors);
            model.Add("allAuthors", allAuthors);
            model.Add("book", selectedBook);

            return View("Show", model);
        }


        //add author to books_authors join table
        [HttpPost("/books/{bookId}/addAuthor")]
        public ActionResult AddAuthor(int bookId, int authorAdded)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Book selectedBook = Book.Find(bookId);
            Author author = Author.Find(authorAdded);
            selectedBook.AddAuthor(author);
            List<Author> bookAuthors = Book.GetAuthors(bookId);
            List<Author> allAuthors = Author.GetAll();
            model.Add("bookAuthors", bookAuthors);
            model.Add("allAuthors", allAuthors);
            model.Add("book", selectedBook);
            return View("Show", model);
        }

    }
}