using ArandaEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArandaLogic.ProductLogic
{
    public interface IProductsLogic
    {
        bool CreateProduct(ProductToSave product);
        bool UpdateProduct(ProductToUpdate product);
        bool DeleteProduct(int idProduct);
        List<Product> GetProductsByParams(string productName, string description, int? idProductCategory, bool sortAsc);
    }

    public class Product
    {
        public int idProduct { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public int idProductCategory { get; set; }
        public string productImage { get; set; }
        public bool isActive { get; set; }
        public string categoryName { get; set; }
    }

    public class ProductToSave
    {
        public string productName { get; set; }
        public string description { get; set; }
        public int idProductCategory { get; set; }
        public string productImage { get; set; }
    }

    public class ProductToUpdate
    {
        public int idProduct { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public int? idProductCategory { get; set; }
        public string productImage { get; set; }
    }

    public class ProductLogic : IProductsLogic
    {
        IDisconGenericRepository<ArandaEntity.Product> _repository = new DisconGenericRepository<ArandaEntity.Product>(() => new ArandaDBModel());

        public bool CreateProduct(ProductToSave product)
        {
            bool isSaved = false;
            try
            {
                int idProduct = 0;
                ArandaEntity.Product productToSave = new ArandaEntity.Product();
                if (product != null)
                {
                    productToSave.productName = product.productName;
                    productToSave.description = product.description;
                    productToSave.idProductCategory = product.idProductCategory;
                    productToSave.productImage = System.Convert.FromBase64String(product.productImage);
                    productToSave.isActive = true;
                    idProduct = _repository.Add(productToSave);
                    if(idProduct > 0)
                        isSaved = true;
                }
            }
            catch(Exception ex)
            {

            }
            return isSaved;
        }

        public bool DeleteProduct(int idProduct)
        {
            if(idProduct > 0)
            {
                ArandaEntity.Product productToUpdate = _repository.Find(idProduct);
                if(productToUpdate != null)
                {
                    _repository.Remove(productToUpdate);
                    return true;
                }
            }
            return false;
        }

        public List<Product> GetProductsByParams(string productName, string description, int? idProductCategory, bool sortAsc)
        {
            List<Product> lsProducts = new List<Product>();

            using (var context = new ArandaDBModel()) {

                var query = (from a in context.Products
                             join b in context.Categories on a.idProductCategory equals b.idProductCategory
                             where productName.Contains(a.productName) || description.Contains(a.description)
                                || (a.idProductCategory == idProductCategory.Value || !idProductCategory.HasValue)
                             select new
                             {
                                 a.idProduct,
                                 a.productName,
                                 a.description,
                                 a.idProductCategory,
                                 a.productImage,
                                 a.isActive,
                                 b.categoryName
                             }).ToList();

                if (sortAsc)
                    query = query.OrderBy(x => x.productName).ThenBy(x => x.idProductCategory).ToList();
                else
                    query = query.OrderByDescending(x => x.productName).ThenByDescending(x => x.idProductCategory).ToList();

                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        Product product = new Product();
                        product.idProduct = item.idProduct;
                        product.productName = item.productName;
                        product.description = item.description;
                        product.idProductCategory = item.idProductCategory;
                        product.productImage = Convert.ToBase64String(item.productImage, 0, item.productImage.Length);
                        product.isActive = item.isActive;
                        product.categoryName = item.categoryName;
                        lsProducts.Add(product);
                    }
                }

            }

            return lsProducts;
        }

        public bool UpdateProduct(ProductToUpdate product)
        {
            bool isUpdate= false;
            try
            {
                ArandaEntity.Product productToUpdate = _repository.Find(product.idProduct);
                int idProduct = 0;
                if (productToUpdate != null)
                {
                    if (string.IsNullOrEmpty(product.productName))
                        productToUpdate.productName = product.productName;

                    if (string.IsNullOrEmpty(product.description))
                        productToUpdate.description = product.description;

                    if (product.idProductCategory.HasValue)
                        productToUpdate.idProductCategory = product.idProductCategory.Value;

                    if (string.IsNullOrEmpty(product.productImage))
                        productToUpdate.productImage = System.Convert.FromBase64String(product.productImage);

                    idProduct = _repository.Update(productToUpdate);

                    if (idProduct > 0)
                        isUpdate = true;
                }
            }
            catch (Exception ex)
            {

            }
            return isUpdate;
        }
    }
}
