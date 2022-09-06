using ArandaLogic.ProductLogic;
using ArandaWebApi.Models;
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
            var message = new Message<List<Product>>();

            var response = productLogic.GetProductsByParams(productName, descripction, category, sortAsc);
            if (!response.HasError)
            {
                message.IsSuccess = true;
                message.ReturnMessage = "Datos consultados de forma correcta";
                message.Data = response.Data;
                return Ok(message);
            }
            else
            {
                message.ReturnMessage = response.Message;
                message.IsSuccess = false;
                return Content(HttpStatusCode.BadRequest, message);
            }
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
            var message = new Message<int>();
            if (product != null) {

                ProductLogic productLogic = new ProductLogic();
                var response = productLogic.CreateProduct(product);

                if (!response.HasError)
                {
                    message.IsSuccess = true;
                    message.ReturnMessage = "Producto guardado de forma correcta";
                    message.Data = response.Data;
                    return Ok(message);
                }
                else
                {
                    message.ReturnMessage = response.Message;
                    message.IsSuccess = false;
                    return Content(HttpStatusCode.BadRequest, message);
                }

            }
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public IHttpActionResult Put([FromBody] ProductToUpdate product)
        {
            var message = new Message<int>();
            if (product != null)
            {
                ProductLogic productLogic = new ProductLogic();
                var response = productLogic.UpdateProduct(product);

                if (!response.HasError)
                {
                    message.IsSuccess = true;
                    message.ReturnMessage = "Producto actualizado de forma correcta";
                    message.Data = response.Data;
                    return Ok(message);
                }
                else
                {
                    message.ReturnMessage = response.Message;
                    message.IsSuccess = false;
                    return Content(HttpStatusCode.BadRequest, message);
                }
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete()]
        public IHttpActionResult Delete(int idProduct)
        {
            var message = new Message<int>();
            if (idProduct > 0)
            {
                ProductLogic productLogic = new ProductLogic();
                var response = productLogic.DeleteProduct(idProduct);

                if (!response.HasError)
                {
                    message.IsSuccess = true;
                    message.ReturnMessage = "Producto eliminado de forma correcta";
                    message.Data = response.Data;
                    return Ok(message);
                }
                else
                {
                    message.ReturnMessage = response.Message;
                    message.IsSuccess = false;
                    return Content(HttpStatusCode.BadRequest, message);
                }
            }
            return Ok();
        }
    }
}