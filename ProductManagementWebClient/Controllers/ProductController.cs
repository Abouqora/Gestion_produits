using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
       private readonly HttpClient _httpClient=null;
       private string productApiUrl = "";
        public ProductController()
        {
            _httpClient = new HttpClient();
            var contentType= new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "https://localhost:7273/api/product";
        }

     
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage content = await _httpClient.GetAsync(productApiUrl);
            string strData= await content.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> listProduits = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(listProduits);


        }
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage content = await _httpClient.GetAsync(productApiUrl+"/"+id);
            string strData = await content.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product Produit = JsonSerializer.Deserialize<Product>(strData, options);
            return View(Produit);


        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> create(Product p)
        {
            
                //HTTP POST
                var postTask = _httpClient.PostAsJsonAsync<Product>(productApiUrl, p);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(p);
        }
        public ActionResult Edit(int id)
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var data = 
            Product p = null;
            var product = new StringContent(JsonConvert.SerializeObject(, Formatting.Indented));
            var postTask = _httpClient.PutAsync<Product>(productApiUrl, p);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(p);
        }
        public async Task<ActionResult>  Delete(int id)
        {
            

                //HTTP DELETE
                var deleteTask = await  _httpClient.DeleteAsync(productApiUrl);
               
                if (deleteTask.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            
           return RedirectToAction("Index");

        }
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    return View();
        //}

    }
}
