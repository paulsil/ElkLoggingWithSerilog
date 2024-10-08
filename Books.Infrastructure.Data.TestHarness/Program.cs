﻿using System;
using System.Text;
using Books.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Infrastructure.Data.TestHarness
{
    class Program
    {
        public static ServiceCollection ServiceCollection { get; private set; }
        public static ServiceProvider ServiceProvider { get; private set; }

        static void Main(string[] args)
        {
            ServiceCollection = new ServiceCollection();
            IoC.ConfigureServices(ServiceCollection);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
            using var context = ServiceProvider.GetService<LibraryContext>();
            //InsertData(context);
            PrintData(context);
        }

        private static void InsertData(LibraryContext context)
        {
              
            // Creates the database if not exists
            context.Database.EnsureCreated();

            // Adds a publisher
            var publisher = new Publisher
            {
                Name = "Mariner Books"
            };
            context.Publisher.Add(publisher);

            // Adds some books
            context.Book.Add(new Book
            {
                ISBN = "978-0544003415",
                Title = "The Lord of the Rings",
                Author = "J.R.R. Tolkien",
                Language = "English",
                Pages = 1216,
                Publisher = publisher
            });
            context.Book.Add(new Book
            {
                ISBN = "978-0547247762",
                Title = "The Sealed Letter",
                Author = "Emma Donoghue",
                Language = "English",
                Pages = 416,
                Publisher = publisher
            });

            // Saves changes
            context.SaveChanges();

            
        }

        private static void PrintData(LibraryContext context)
        {
            // Gets and prints all books in database
            var books = context.Book
                .Include(p => p.Publisher);
            foreach (var book in books)
            {
                var data = new StringBuilder();
                data.AppendLine($"ISBN: {book.ISBN}");
                data.AppendLine($"Title: {book.Title}");
                data.AppendLine($"Publisher: {book.Publisher.Name}");
                Console.WriteLine(data.ToString());
            }
        }
    }
}
