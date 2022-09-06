using ArandaEntity;
using ArandaLogic.General;
using ArandaLogic.ProductLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArandaLogic.ProductLogic
{   
    public interface IProductCategoryLogic
    {
        GenericResponses<int> CreateProductCategory(CategoryToSave category);
        GenericResponses<int> UpdateProductCategory(Category category);
        GenericResponses<int> DeleteProductCategory(int idProductCategory);
        GenericResponses<List<Category>> GetProductCategoriesByParams(string categoryName, bool sortAsc);
    }

    public class CategoryToSave
    {
        public string categoryName { get; set; }
    }
    public class Category
    {
        public int idProductCategory { get; set; }
        public string categoryName { get; set; }
        public bool isActive { get; set; }
    }

    public class ProductCategoryLogic : IProductCategoryLogic
    {
        IDisconGenericRepository<ArandaEntity.Category> _repository = new DisconGenericRepository<ArandaEntity.Category>(() => new ArandaDBModel());

        public GenericResponses<int> CreateProductCategory(CategoryToSave category)
        {
            GenericResponses<int> genericResponses = new GenericResponses<int>();
            try
            {
                ArandaEntity.Category categoryToSave = new ArandaEntity.Category();
                if (category != null)
                {
                    categoryToSave.categoryName = category.categoryName;
                    categoryToSave.isActive = true;
                    genericResponses.Data = _repository.Add(categoryToSave);
                }
            }
            catch (Exception ex)
            {
                genericResponses.Message = "Ha ocurrido un error al momento de crear la categoria " + ex.Message;
                genericResponses.HasError = true;
            }
            return genericResponses;
        }

        public GenericResponses<int> DeleteProductCategory(int isProductCategory)
        {
            GenericResponses<int> genericResponses = new GenericResponses<int>();
            try
            {
                if (isProductCategory > 0)
                {
                    ArandaEntity.Category categoryToUpdate = _repository.Find(isProductCategory);
                    if (categoryToUpdate != null)
                    {
                        genericResponses.Data = _repository.Remove(categoryToUpdate);
                    }
                }
            }
            catch (Exception ex)
            {
                genericResponses.Message = "Ha ocurrido un error al momento de eliminar el categoria " + ex.Message;
                genericResponses.HasError = true;
            }
            return genericResponses;
        }

        public GenericResponses<List<Category>> GetProductCategoriesByParams(string categoryName, bool sortAsc)
        {
            GenericResponses<List<Category>> genericResponses = new GenericResponses<List<Category>>();

            List<Category> lsCategories = new List<Category>();
            try
            {
                using (var context = new ArandaDBModel())
                {

                    var query = (from a in context.Categories
                                 where (a.categoryName.Contains(categoryName) || string.IsNullOrEmpty(categoryName))
                                 select new
                                 {
                                     a.idProductCategory,
                                     a.isActive,
                                     a.categoryName
                                 }).ToList();

                    if (sortAsc)
                        query = query.OrderBy(x => x.categoryName).ToList();
                    else
                        query = query.OrderByDescending(x => x.categoryName).ToList();

                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            Category category = new Category();
                            category.idProductCategory = item.idProductCategory;
                            category.isActive = item.isActive;
                            category.categoryName = item.categoryName;
                            lsCategories.Add(category);
                        }
                        genericResponses.Data = lsCategories;
                    }
                }
            }
            catch (Exception ex)
            {
                genericResponses.Message = "Ha ocurrido un error al momento de consultar los datos " + ex.Message;
                genericResponses.HasError = true;
            }
            return genericResponses;
        }

        public GenericResponses<int> UpdateProductCategory(Category category)
        {
            GenericResponses<int> genericResponses = new GenericResponses<int>();
            try
            {
                ArandaEntity.Category categoryToUpdate = _repository.Find(category.idProductCategory);
                if (categoryToUpdate != null)
                {
                    if (string.IsNullOrEmpty(category.categoryName))
                        categoryToUpdate.categoryName = category.categoryName;

                    genericResponses.Data = _repository.Update(categoryToUpdate);
                }
            }
            catch (Exception ex)
            {
                genericResponses.Message = "Ha ocurrido un error al momento de actualizar los datos " + ex.Message;
                genericResponses.HasError = true;
            }
            return genericResponses;
        }
    }
}
