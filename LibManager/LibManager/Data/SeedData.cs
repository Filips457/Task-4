using LibManager.Models;

namespace LibManager.Data;

public static class SeedData
{
    public static void SeedSomeData(LibraryContext context)
    {
        if (context.Authors.Any()) return;

        var authors = new List<Author>
        {
            new Author
            {
                Name = "Лев Толстой",
                DateOfBirth = new DateTime(1828, 9, 9),
                Books = new List<Book>
                {
                    new Book { Title = "Война и мир", PublishedYear = 1869 },
                    new Book { Title = "Анна Каренина", PublishedYear = 1877 }
                }
            },
            new Author
            {
                Name = "Джордж Оруэлл",
                DateOfBirth = new DateTime(1903, 6, 25),
                Books = new List<Book>
                {
                    new Book { Title = "1984", PublishedYear = 1949 },
                    new Book { Title = "Скотный двор", PublishedYear = 1945 }
                }
            },
            new Author
            {
                Name = "Харуки Мураками",
                DateOfBirth = new DateTime(1949, 1, 12),
                Books = new List<Book>
                {
                    new Book { Title = "1Q84", PublishedYear = 2009 },
                    new Book { Title = "Убийство Командора", PublishedYear = 2017 }
                }
            },
            new Author
            {
                Name = "Салли Руни",
                DateOfBirth = new DateTime(1991, 2, 20),
                Books = new List<Book>
                {
                    new Book { Title = "Нормальные люди", PublishedYear = 2018 },
                    new Book { Title = "Разговоры с друзьями", PublishedYear = 2017 }
                }
            },
            new Author
            {
                Name = "Артём Ковалев",
                DateOfBirth = new DateTime(1995, 5, 5),
                Books = new List<Book>
                {
                    new Book { Title = "Архитектура .NET приложений", PublishedYear = 2023 },
                    new Book { Title = "SQL: от теории к практике", PublishedYear = 2024 }
                }
            }
        };

        context.Authors.AddRange(authors);
        context.SaveChanges();
    }
}