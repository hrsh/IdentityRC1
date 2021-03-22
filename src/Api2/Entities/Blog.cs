namespace Api2.Entities
{
    public class Blog
    {
        public Blog()
        {

        }

        public Blog(int id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = body;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
