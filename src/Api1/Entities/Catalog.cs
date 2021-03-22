namespace Api1.Entities
{
    public class Catalog
    {
        public Catalog()
        {

        }

        public Catalog(int id, string title, int price)
        {
            Id = id;
            Title = title;
            Price = price;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }
    }
}
