using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace ProductManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        [HttpGet]

        //GET : api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id) => repository.GetProductByid(id);

        [HttpPost]
        public IActionResult PostProduct(Product p)
        {
            repository.SaveProduct(p); 
            return NoContent();
        }
        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductByid(id);
            if (p == null)
                return NotFound();
            repository.DeleteProduct(p);
            return NoContent();

        }
        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, Product p)
        {
            var Ptmp = repository.GetProductByid(id);
            if (Ptmp == null)
                return NotFound();
            repository.UpdateProduct(p);
            return NoContent();
        }




    }
}
