using LibManager.Models;

namespace LibManager.Data;

public class BadDataStorage
{
    public static readonly List<Author> Authors;
    public static readonly List<Book> Books;

    static BadDataStorage()
    {
        Authors = new List<Author>();
        Books = new List<Book>();
    }
}