using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using ProductComponent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductComponent.Data.Tests
{
    [TestClass()]
    public class ProductRepositoryTests: IDisposable
    {
        private readonly IProductRepository _repository;
        private readonly ProductComponentContext _context;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProductComponentContext>()
                .UseInMemoryDatabase(databaseName: "Test123")
                .Options;

            _context = new ProductComponentContext(options);
            this.SeedData(_context);
            _repository = new ProductRepository(_context);
        }

        public void SeedData(ProductComponentContext context)
        {
            context.Product.Add(new Product
            {
                Id = 1,
                ProductName = "Super awesome software",
                ProductSlug = "super-awesome-software",
                Description = "this is a test",
                Price = 12.00
            });

            context.SaveChanges();
        }
        public new void Dispose()
        {
            //Delete database after each test.
            this._context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public async Task GetAllProduct()
        {
            ICollection<Product> products = (ICollection<Product>)await this._repository.GetProducts();

            Assert.IsTrue(1 == products.Count);
        }

        [TestMethod()]
        public async Task GetProductBySlugAsync()
        {
            Product product = await this._repository.GetProductBySlug("super-awesome-software");

            Assert.IsTrue("super-awesome-software" == product.ProductSlug);
        }

        [TestMethod()]
        public async Task CreateProductAsync()
        {
            Product newProduct = new Product
            {
                ProductName = "Super Bad Software",
                Description = "This is a test",
                ProductSlug = "super-bad-software",
                Price = 12.00
            };

            await this._repository.CreateProduct(newProduct);

            Assert.IsTrue(2 == newProduct.Id);
        }

        [TestMethod()]
        public async Task UpdateProductAsync()
        {
            Product product = await this._repository.GetProductBySlug("super-awesome-software");

            product.Price = 20.00;
            await this._repository.EditProduct(product);

            Assert.IsTrue(20.00 == product.Price);
        }

        [TestMethod()]
        public async Task DeleteProductAsync()
        {
            Product product = await this._repository.GetProductBySlug("super-awesome-software");

            await this._repository.DeleteProduct(product.Id);

            ICollection<Product> products = (ICollection<Product>)await this._repository.GetProducts();

            Assert.IsTrue(0 == products.Count);
        }
    }
}