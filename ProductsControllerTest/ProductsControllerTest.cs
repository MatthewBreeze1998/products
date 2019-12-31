using Could_System_dev_ops.Controllers;
using Could_System_dev_ops.Models;
using Could_System_dev_ops.Repo;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ProductsControllerTest
{
    public class ProdcutControllerTest
    {
        private HttpClient _client;
        private FakeProductsRepo _ProductsRepo;
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

            _ProductsRepo = new FakeProductsRepo();
            _ProductsController = new ProductsController(_ProductsRepo);
            _ReSaleRepo = new SuccessResaleService();
            
        }
        [Test]
        public void CreateProduct_validProduct_ShouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller
            ProductsModel product = new ProductsModel() { ProductId = 4, ProductName = "levi jeans", Description = "blue Jeans", Price = 123.12, StockLevel = 19 };// valid new model 
            Assert.IsNotNull(product);

            int currentMaxId = _ProductsController.GetAllProducts().Max(x => x.ProductId);// gets max product id 
            Assert.GreaterOrEqual(currentMaxId, 1);// adds 1 to max

            ActionResult<ProductsModel> result = _ProductsController.CreateProdcut(product);// result is return of create controller function
            Assert.IsNotNull(result);// result is nit null

            ActionResult ProductResult = result.Result;// ProductResult is result.Result
            Assert.AreEqual(ProductResult.GetType(), typeof(CreatedAtActionResult));//  ProductResult is of type create at action

            CreatedAtActionResult createdProductResult = (CreatedAtActionResult)ProductResult;// new create at action ProductResult 
            Assert.IsNotNull(createdProductResult);// createdProductResult is not null
            Assert.AreEqual(createdProductResult.Value.GetType(), typeof(ProductsModel));// createdProductResult is of tpye product model

            ProductsModel ProductValue = (ProductsModel)createdProductResult.Value;// ProductValue is createdProductResult.Value
            Assert.IsNotNull(ProductValue);// is not null

            Assert.AreEqual(currentMaxId + 1, ProductValue.ProductId);// are equal 
            Assert.AreEqual(product.ProductName, ProductValue.ProductName);// are equal 
            Assert.AreEqual(product.Description, ProductValue.Description);// are equal 
            Assert.AreEqual(product.Price, ProductValue.Price);// are equal 
            Assert.AreEqual(product.StockLevel, ProductValue.StockLevel);// are equal 
        }

        [Test]
        public void CreateProduct_InvalidProduct_ShouldObject()
        {


            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller
            ProductsModel product = null;// null product model
            Assert.IsNull(product);// is null

            ActionResult<ProductsModel> result = _ProductsController.CreateProdcut(product);// calls create with null model 
            Assert.IsNotNull(result);// result is nit null

            ActionResult ProductResult = result.Result;// ProductResult is result. result
            Assert.AreEqual(ProductResult.GetType(), typeof(BadRequestResult));// ProductResult is of bad request
        }
     
        
        [Test]
        public void DeleteProduct_valid_ShouldObject()
        {

                Assert.IsNotNull(_ProductsRepo);// not null repo
                Assert.IsNotNull(_ProductsController);// not null controller;
                ProductsModel DeleteProduct = new ProductsModel() { ProductId = 2, ProductName = "Black desk", Description = "Black desk", Price = 11.4, StockLevel = 3 };// valid product model
                Assert.IsNotNull(DeleteProduct);// DeleteProduct is not null

                ActionResult<ProductsModel> product = _ProductsController.DeleteProduct(DeleteProduct); // product is the return of DeleteProduct
                Assert.IsNotNull(product);// product is not null 
                Assert.IsNotNull(product.Value);// product. value is not null

                ActionResult<ProductsModel> result = _ProductsController.getProduct(product.Value.ProductId);// result is result of get product
                Assert.IsNotNull(result);// is nit null

                ActionResult StaffResult = result.Result;//StaffResult is result.result
                Assert.AreEqual(StaffResult.GetType(), typeof(NotFoundResult));// StaffResult is of type bad request
        }

        [Test]
        public void DeleteProduct_invalid_ShouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;
            ProductsModel product = null;// prodcut is null product model
            Assert.IsNull(product);// product is null

            ActionResult<ProductsModel> result = _ProductsController.DeleteProduct(product); // result is return if DeleteProduct
            Assert.IsNotNull(result);// result is not null

            ActionResult ProductResult = result.Result;// ProductResult is result.Result
            Assert.AreEqual(ProductResult.GetType(), typeof(BadRequestResult));//ProductResult is type of bad request cause product is null
        }
    }
}