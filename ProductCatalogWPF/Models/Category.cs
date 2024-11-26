namespace ProductCatalogWPF.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        // Standardvärdet är en tom sträng om inget namn anges.

        public override string ToString()
        {
            return CategoryName;
        }
        // Returnerar endast kategorinamnet när objektet används som text.
    }
}
