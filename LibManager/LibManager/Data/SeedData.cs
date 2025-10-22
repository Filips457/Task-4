using LibManager.Models;

namespace LibManager.Data;

public static class SeedData
{
    public static void SeedSomeData(LibraryContext context)
    {
        if (context.Authors.Any()) return;

        var author = new Author
        {
            Name = "Лев Толстой",
            DateOfBirth = new DateTime(1828, 9, 9),
            Books = new List<Book>
                {
                    new Book { Title = "Война и мир", PublishedYear = 1869 },
                    new Book { Title = "Анна Каренина", PublishedYear = 1877 }
                }
        };

        context.Authors.Add(author);
        context.SaveChanges();
    }
}