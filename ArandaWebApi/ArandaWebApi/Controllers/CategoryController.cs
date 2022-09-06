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
    public class CategoryController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult Get(string categoryName, bool sortAsc = true)
        {
            ProductCategoryLogic categoryLogic = new ProductCategoryLogic();
            var message = new Message<List<Category>>();

            var response = categoryLogic.GetProductCategoriesByParams(categoryName, sortAsc);
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

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody] CategoryToSave category)
        {
            var message = new Message<int>();
            if (category != null)
            {
                ProductCategoryLogic categoryLogic = new ProductCategoryLogic();
                var response = categoryLogic.CreateProductCategory(category);

                if (!response.HasError)
                {
                    message.IsSuccess = true;
                    message.ReturnMessage = "Categoria guardada de forma correcta";
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
        public IHttpActionResult Put([FromBody] Category category)
        {
            var message = new Message<int>();
            if (category != null)
            {
                ProductCategoryLogic categoryLogic = new ProductCategoryLogic();
                var response = categoryLogic.UpdateProductCategory(category);

                if (!response.HasError)
                {
                    message.IsSuccess = true;
                    message.ReturnMessage = "Categoria actualizada de forma correcta";
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
        public IHttpActionResult Delete(int idProductCategory)
        {
            var message = new Message<int>();
            if (idProductCategory > 0)
            {
                ProductCategoryLogic categoryLogic = new ProductCategoryLogic();
                var response = categoryLogic.DeleteProductCategory(idProductCategory);

                if (!response.HasError)
                {
                    message.IsSuccess = true;
                    message.ReturnMessage = "Categoria eliminada de forma correcta";
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