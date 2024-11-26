using ProductCatalogWPF.Models;
using System.Collections.Generic;

namespace ProductCatalogWPF.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void RemoveProduct(int productId);
        void UpdateProduct(int productId, string name, Category category, decimal price);
        List<Product> GetAllProducts();
        void SaveProductsToFile(string filePath);
        List<Product> LoadProductsFromFile(string filePath);
    }
}
