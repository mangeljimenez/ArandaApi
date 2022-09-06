using ArandaLogic.ProductLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArandaWebApi.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get(string productName,string descripction, int? category, bool sortAsc = true)
        {
            ProductLogic productLogic = new ProductLogic();
            List<Product> lsProducts = new List<Product>();

            lsProducts = productLogic.GetProductsByParams(productName, descripction, category, sortAsc);

            return Ok(lsProducts);
        }

        //// GET api/<controller>/5
        //[HttpGet]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody] ProductToSave product)
        {
            if (product != null) {
                ProductLogic productLogic = new ProductLogic();
                productLogic.CreateProduct(product);
            }
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public IHttpActionResult Put([FromBody] ProductToUpdate product)
        {
            if (product != null)
            {
                ProductLogic productLogic = new ProductLogic();
                productLogic.UpdateProduct(product);
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete()]
        public IHttpActionResult Delete(int idProduct)
        {
            if (idProduct > 0)
            {
                ProductLogic productLogic = new ProductLogic();
                productLogic.DeleteProduct(idProduct);
            }
            return Ok();
        }
    }
}