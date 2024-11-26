using ProductCatalogWPF.Models;
using ProductCatalogWPF.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProductCatalogWPF
{
    public partial class MainWindow : Window
    {
        private ProductService _productService = new ProductService();
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        // En ObservableCollection (automatiskt ska meddela ändringar) produkter hålls i gränssnitt.
        // Eventuella ändringar granskas gränssnittet.

        public MainWindow()
        {
            InitializeComponent();
            ProductGrid.ItemsSource = _products;
            // Visar produkterna i en tabell.
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var priceInput = PriceBox.Text.Replace('.', ',');
                if (!decimal.TryParse(priceInput, out var price))
                {
                    throw new FormatException("Felaktigt prisformat. Ange ett giltigt pris.");
                }
                // Hanterar decimalformat genom att byta ut punkt till komma och försöker konvertera text till ett decimalvärde.

                var product = new Product
                {
                    Name = NameBox.Text,
                    Price = price,
                    Category = new Category { CategoryName = CategoryBox.Text }
                };
                // Skapar ett nytt produktobjekt baserat på användarens inmatning.

                _productService.AddProduct(product);
                _products.Add(product);
                // Lägger till produkten i både tjänstens lista och "_products".

                ClearInputFields();
                // Rensar textfälten efter att produkten har lagts till.
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                // Visar felmeddelande om något blir fel
            }
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Product)ProductGrid.SelectedItem;
            if (selectedProduct != null)
            {
                _productService.RemoveProduct(selectedProduct.ID);
                _products.Remove(selectedProduct);
                // Tar bort den valda produkten från både tjänstens lista och _products.
            }
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Product)ProductGrid.SelectedItem;
            if (selectedProduct != null)
            {
                try
                {
                    var priceInput = PriceBox.Text.Replace('.', ',');
                    if (!decimal.TryParse(priceInput, out var price))
                    {
                        throw new FormatException("Felaktigt prisformat. Ange ett giltigt pris.");
                    }
                    // Hanterar decimalformat

                    _productService.UpdateProduct(selectedProduct.ID, NameBox.Text, new Category { CategoryName = CategoryBox.Text }, price);
                    selectedProduct.Name = NameBox.Text;
                    selectedProduct.Category = new Category { CategoryName = CategoryBox.Text };
                    selectedProduct.Price = price;
                    ProductGrid.Items.Refresh();
                    // Uppdaterar den valda produkten i tjänstens lista och uppdaterar också tabellen (ProductGrid).

                    ClearInputFields();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SaveProducts_Click(object sender, RoutedEventArgs e)
        {
            _productService.SaveProductsToFile("Resources/products.json");
            // Sparar alla produkter i tjänstens lista till filen "products.json" i Resources-mappen.
            MessageBox.Show("Produkter sparade framgångsrikt.");
        }

        private void LoadProducts_Click(object sender, RoutedEventArgs e)
        {
            var loadedProducts = _productService.LoadProductsFromFile("Resources/products.json");
            _products.Clear();
            foreach (var product in loadedProducts)
            {
                _products.Add(product);
                // Laddar produkter från filen och uppdaterar "_products" så att de visas i gränssnittet.
            }
            MessageBox.Show("Produkter inlästa framgångsrikt.");
        }

        private void ClearInputFields()
        {
            NameBox.Text = string.Empty;
            CategoryBox.Text = string.Empty;
            PriceBox.Text = string.Empty;
            // Rensar textfälten för just namn, kategori och pris.
        }
    }
}
