using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Could_System_dev_ops.Models;

namespace Could_System_dev_ops.Repo
{
    public class FakeProductsRepo : IProductsRepositry
    
    {

        public List<ProductsModel> _ProductsModelsList;
        public List<ProductsModel> _productTests;
        public FakeProductsRepo()
        {
            _ProductsModelsList = new List<ProductsModel>()

            {
                new ProductsModel() {ProductId = 1,ProductName = "levi jeans", Description  =  "blue Jeans", Price = 123.12, StockLevel = 19},
                new ProductsModel() {ProductId = 2,ProductName = "Black desk", Description  =  "Black desk", Price = 11.4 ,StockLevel = 3},
                new ProductsModel() {ProductId = 3,ProductName = "Moniter", Description  =  "24' lg 1080p", Price = 341.41 ,StockLevel = 19}
            };// test data 
        }
        

        public ProductsModel CreateProduct(ProductsModel products)
        {
            _ProductsModelsList.Add(products);// products to test data
            return products;// returrms the the new product
        }
        public ProductsModel DeleteProduct(ProductsModel Product)
        {
            _ProductsModelsList.Remove(_ProductsModelsList.FirstOrDefault(x => Product.ProductId == x.ProductId)); // finds first staff with given id then removes them form the fake data
            return Product;
        }
        public ProductsModel GetProduct(int? id)
        {
           
            return _ProductsModelsList.FirstOrDefault(x => id == x.ProductId);// uses id to find product
            // uses first or default to find the first product with that id
            // retruns the product
        }

        public IEnumerable<ProductsModel> GetAllProduct()
        {
            return _ProductsModelsList.AsEnumerable<ProductsModel>(); // retuns all prodcuts and returns them as an IEnumerable
        }

        public ProductsModel EditProduct(ProductsModel products)
        {
            
            return _ProductsModelsList[_ProductsModelsList.IndexOf(_ProductsModelsList.FirstOrDefault(x => x.ProductId == products.ProductId))] = products;
            // finds the product with the id then replaces the index with the new product model thats passed through
        }

   
    }
}
