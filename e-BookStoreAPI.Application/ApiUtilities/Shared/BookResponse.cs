
    public class BookResponse
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public int PublishedYear { get; set; }
        public DateTime CreatedOn { get; set; } 

}

