using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Transaction.BusinessLogic;
using Transaction.Models;

namespace Transaction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

  
        public ProductsController(ProductService service)
        {
            _service = service;
        }

    
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            var products = _service.GetProducts();
            return Ok(products); 
        }

        [HttpPost]
        public ActionResult Create([FromBody] Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Item) || product.PurchasePrice <= 0 || product.SellingPrice <= 0)
            {
                return BadRequest("Invalid product details. Check names and pricing values."); 
            }

            _service.AddProduct(product);
            return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product); 
        }

        [HttpPut]
        public ActionResult Update([FromBody] Product product)
        {
            var products = _service.GetProducts();
            var exists = products.Exists(p => p.Id == product.Id);

            if (!exists)
            {
                return NotFound($"Product with ID {product.Id} not found."); 
            }

            _service.UpdateProduct(product);
            return Ok("Product updated successfully.");
        }

       
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var products = _service.GetProducts();
            var exists = products.Exists(p => p.Id == id);

            if (!exists)
            {
                return NotFound($"Product with ID {id} not found."); 
          }

            _service.DeleteProduct(id);
            return NoContent(); 
        }
    }
}