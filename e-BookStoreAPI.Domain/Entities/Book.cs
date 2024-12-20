using eBookStoreAPI.Domain.Entities;

namespace eBookStoreAPI.Domain.Entities;

public class Book: EntityBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public int PublishedYear { get; set; }

        public Book() { }

        public Book(string title, string genre, string isbn, string authorName, int publishedYear)
        {
            Title = title;
            Genre = genre;
            ISBN = isbn;
            AuthorName = authorName;
            PublishedYear = publishedYear;
        }
    }

