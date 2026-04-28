using ECommerce.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 100000 },
            new Product { Id = 2, Name = "Mobile", Price = 20000 },
            new Product { Id = 3, Name = "Book", Price = 1500 }
        };

        [HttpGet]
        public List<Product> Getall()
        {
            return products;
        }

        [HttpGet("{id}")]
        public Product GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        [HttpPost]
        public Product Create(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return product;
        }

        [HttpPut("{id}")]
        public Product Update(int id, Product updateProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return null;
            }
            product.Name = updateProduct.Name;
            product.Price = updateProduct.Price;

            return product;
        }

        [HttpPatch("{id}")]
        public Product UpdateIndividual(int id, Product updateProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(updateProduct.Name))
            {
                product.Name = updateProduct.Name;
            }
            if (updateProduct.Price > 0)
            {
                product.Price = updateProduct.Price;
            }
            return product;
        }

        [HttpDelete("{id}")]
        public List<Product> Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return null;
            }
            products.Remove(product);
            return products;
        }
    }
}
