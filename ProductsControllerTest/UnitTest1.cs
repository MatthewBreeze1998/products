using Could_System_dev_ops.Controllers;
using Could_System_dev_ops.Models;
using Could_System_dev_ops.Repo;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

namespace ProductsControllerTest
{
    public class ProdcutControllerTest
    {
        private HttpClient _client;
        private IProductsRepositry _ProductsRepo;
        private IReSaleRepositry _ReSaleRepo;
        private ProductsController _ProductsController;
        private List<ProductsModel> _ProductsModelsList;
        public ProdcutControllerTest()
        {
            _client = new HttpClient();
        }

        [SetUp]
        public void Setup()
        {
            _ProductsModelsList = new List<ProductsModel>()
            {
                new ProductsModel() {ProductId = 1,ProductName = "levi jeans", Description  =  "blue Jeans", Price = 123.12, StockLevel = 19},
                new ProductsModel() {ProductId = 2,ProductName = "Black desk", Description  =  "Black desk", Price = 11.4 ,StockLevel = 3},
                new ProductsModel() {ProductId = 3,ProductName = "Moniter", Description  =  "24' lg 1080p", Price = 341.41 ,StockLevel = 19}
            };// test data 

            _ProductsController = new ProductsController(_ProductsRepo);
            _ReSaleRepo = new SuccessResaleService();
            _ProductsRepo = new FakeProductsRepo();

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}