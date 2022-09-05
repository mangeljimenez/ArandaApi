using ArandaEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArandaLogic.ProductLogic
{
    public interface IProductsLogic
    {
        List<Product> GetSingleProducts(int idProduct);
    }

    public class Product
    {
        public int idProduct { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public int idProductCategory { get; set; }
        public byte[] productImage { get; set; }
        public bool isActive { get; set; }
        public string categoryName { get; set; }
    }

    public class ProductLogic : IProductsLogic
    {
        IDisconGenericRepository<ArandaEntity.Product> _repository = new DisconGenericRepository<ArandaEntity.Product>(() => new ArandaDBModel());
        public List<Product> GetSingleProducts(int idProduct)
        {
            List<Product> pList = new List<Product>();
            List<ArandaEntity.Product> products = _repository.All().ToList<ArandaEntity.Product>();
            if(products.Count > 0)
            {
                foreach(var prod in products)
                {
                    Product pToAdd = new Product();

                    pToAdd.idProduct = prod.idProduct;
                    pToAdd.productName = prod.productName;
                    pToAdd.description = prod.description;
                    pToAdd.idProductCategory = prod.idProductCategory;
                    pToAdd.productImage = prod.productImage;
                    pToAdd.isActive = prod.isActive;
                    pToAdd.categoryName = prod.Category.categoryName;

                    pList.Add(pToAdd);
                }
            }
            return pList;
        }
    }
}
