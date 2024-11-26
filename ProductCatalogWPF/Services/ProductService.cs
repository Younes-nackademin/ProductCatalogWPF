using Newtonsoft.Json;
using ProductCatalogWPF.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductCatalogWPF.Services
{
    public class ProductService : IProductService
    {
        private List<Product> _products = new List<Product>();
        // En lista som lagrar alla produkter i minnet.

        public void AddProduct(Product product)
        {
            if (_products.Any(p => p.Name == product.Name))
                throw new System.Exception("Produkten finns redan.");
            // Kontrollera om en produkt med samma namn redan finns i listan.
           //Kastar om
            _products.Add(product);
            // Lägger till den nya produkten i listan.
        }

        public void RemoveProduct(int productId)
        {
            var product = _products.FirstOrDefault(p => p.ID == productId);
            if (product != null)
                _products.Remove(product);
            // Hitta produkten baserat på dess ID och ta bort den från listan.
        }

        public void UpdateProduct(int productId, string name, Category category, decimal price)
        {
            var product = _products.FirstOrDefault(p => p.ID == productId);
            if (product != null)
            {
                product.Name = name;
                product.Category = category;
                product.Price = price;
                // Uppdatera produktens egenskaper om den hittas i listan.
            }
        }

        public List<Product> GetAllProducts() => _products;
        // Returnerar hela listan med produkter.

        public void SaveProductsToFile(string filePath)
        {
            var json = JsonConvert.SerializeObject(_products, Formatting.Indented);
            // Konverterar listan med produkter till JSON-format med läsbar formatering.
            File.WriteAllText(filePath, json);
            // Skriver JSON-data till filen på den angivna sökvägen.
        }

        public List<Product> LoadProductsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                // Läser in JSON-data från filen om den finns.
                _products = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
                // Tillbaka (deserialisering) Ursprunglig JSON-data till en lista med produkter.
                // Om filen är tom returneras en tom lista.
            }
            return _products;
        }
    }
}
