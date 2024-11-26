namespace ProductCatalogWPF.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;
        // Standardvärdet är en tom sträng om inget namn anges.

        public decimal Price { get; set; }

        public Category Category { get; set; } = new Category { CategoryName = "Ingen" };
        // Om ingen kategori anges, används en standardkategori med namnet "Ingen".

        public override string ToString()
        {
            return $"ID: {ID}, Namn: {Name}, Pris: {Price:C}, Kategori: {Category.CategoryName}";
            // Formar produktens information som en sträng, inklusive pris med valutatecken (C).
        }
    }
}
