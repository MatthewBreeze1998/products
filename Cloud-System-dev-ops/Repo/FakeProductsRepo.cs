using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud_System_dev_ops.Models;

namespace Cloud_System_dev_ops.Repo
{
    public class FakeProductsRepo : IRepository<ProductsModel>
    
    {

        public List<ProductsModel> _ProductsModelsList;
        public FakeProductsRepo()
        {
            _ProductsModelsList = new List<ProductsModel>()

            {
                new ProductsModel() {ProductId = 1,ProductName = "levi jeans", Description  =  "blue Jeans", Price = 123.12, StockLevel = 19, SupplierName = ""},
                new ProductsModel() {ProductId = 2,ProductName = "Black desk", Description  =  "Black desk", Price = 11.4 ,StockLevel = 3, SupplierName = ""},
                new ProductsModel() {ProductId = 3,ProductName = "Moniter", Description  =  "24' lg 1080p", Price = 341.41 ,StockLevel = 19, SupplierName = ""}
            };// test data 
        }
        

        public ProductsModel CreateObject(ProductsModel product)
        {

            product.ProductId = GetNextId();
            _ProductsModelsList.Add(product);// products to test data
            return product;// returrms the the new product
        }
        public ProductsModel DeleteObject(ProductsModel Product)
        {
            _ProductsModelsList.Remove(_ProductsModelsList.FirstOrDefault(x =>   x.ProductId == Product.ProductId)); // finds first staff with given id then removes them form the fake data
            return Product;
        }

        public IEnumerable<ProductsModel> GetObject()
        {
            return _ProductsModelsList.AsEnumerable<ProductsModel>(); // retuns all prodcuts and returns them as an IEnumerable
        }

        public ProductsModel UpdateObject(ProductsModel product)
        {
            ProductsModel inMemory = _ProductsModelsList.FirstOrDefault(x => x.ProductId == product.ProductId);

            if(inMemory == null )
            {
                return null;
            }
            try
            {
                int index = _ProductsModelsList.IndexOf(inMemory);
                _ProductsModelsList[index] = product;
                return product;

            }
            catch(Exception ex)
            {
                return null;
            }
        }
        private int GetNextId()
        {
            return (_ProductsModelsList == null || _ProductsModelsList.Count() == 0) ? 1 : _ProductsModelsList.Max(x => x.ProductId) + 1;
        }
    }
}
