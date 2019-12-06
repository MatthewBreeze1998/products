using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Could_System_dev_ops.Models;

namespace Could_System_dev_ops.Repo
{
    public class FakeProductsRepo : ProductsRepo

    {

        private List<ProductsModel> _ProductsModelsList;


        public FakeProductsRepo()
        {
            _ProductsModelsList = new List<ProductsModel>()
            {
                new ProductsModel() {ProductId = 1,ProductName = "levi jeans", Description  =  "blue Jeans", Price = 123.12, StockLevel = 19},
                new ProductsModel() {ProductId = 1,ProductName = "Black desk", Description  =  "Black desk", Price = 11.4 ,StockLevel = 3},
                new ProductsModel() {ProductId = 1,ProductName = "Moniter", Description  =  "24' lg 1080p", Price = 341.41 ,StockLevel = 19}
            };
        }


        
        public ProductsModel CreateProduct(ProductsModel products)
        {
            
            _ProductsModelsList.Add(products);
            return products;
        }

        public ProductsModel GetProduct(int id)
        {
            return _ProductsModelsList.FirstOrDefault(x => id == x.ProductId);
        }

        public IEnumerable<ProductsModel> GetAllProduct()
        {
            return _ProductsModelsList.AsEnumerable<ProductsModel>();
        }

        public ProductsModel SetResale(int id)
        {
            ReSaleModel ReSalePrice = _ReSaleList.FirstOrDefault(x => x.ProductId == id);
            ProductsModel price = _ProductsModelsList.FirstOrDefault(b => b.ProductId == id);

            price.Price = ReSalePrice.NewPrice;
            _ProductsModelsList.Insert(_ProductsModelsList.IndexOf(_ProductsModelsList.FirstOrDefault(x => id == x.ProductId)), price);

            return price;
        }

        public ProductsModel UpdateStock(int id, int increase)
        {
           ProductsModel Update =  _ProductsModelsList.FirstOrDefault(x => id == x.ProductId);
           Update.StockLevel = Update.StockLevel + increase;
           _ProductsModelsList.Insert(_ProductsModelsList.IndexOf(_ProductsModelsList.FirstOrDefault(x => id == x.ProductId)), Update);
           return Update;
        }
    }
}
