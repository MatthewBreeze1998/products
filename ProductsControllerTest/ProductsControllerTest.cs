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

            _ProductsController = new ProductsController(_ProductsRepo);
            _ReSaleRepo = new SuccessResaleService();
            _ProductsRepo = new FakeProductsRepo();
        }
        [Test]
        public void CreateProduct_validProduct_ShouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);
            Assert.IsNotNull(_ProductsController);
            ProductsModel product = new ProductsModel() { ProductId = 4, ProductName = "levi jeans", Description = "blue Jeans", Price = 123.12, StockLevel = 19 };
            Assert.IsNotNull(product);

         

            ActionResult<ProductsModel> result = _ProductsController.CreateProdcut(product);
            Assert.IsNotNull(result);

            ActionResult ProductResult = result.Result;
            Assert.AreEqual(ProductResult.GetType(), typeof(CreatedAtActionResult));

            CreatedAtActionResult createdProductResult = (CreatedAtActionResult)ProductResult;
            Assert.IsNotNull(createdProductResult);
            Assert.AreEqual(createdProductResult.Value.GetType(), typeof(ProductsModel));

            ProductsModel ProductValue = (ProductsModel)createdProductResult.Value;
            Assert.IsNotNull(ProductValue);

            Assert.AreEqual( product.ProductId, ProductValue.ProductId);
            Assert.AreEqual(product.ProductName, ProductValue.ProductName);
            Assert.AreEqual(product.Description, ProductValue.Description);
            Assert.AreEqual(product.Price, ProductValue.Price);
            Assert.AreEqual(product.StockLevel, ProductValue.StockLevel);
        }

        [Test]
        public void CreateProduct_InvalidProduct_ShouldObject()
        {
           
      
            Assert.IsNotNull(_ProductsRepo);
            Assert.IsNotNull(_ProductsController);
            ProductsModel product = null;
            Assert.IsNull(product);

            ActionResult<ProductsModel> result = _ProductsController.CreateProdcut(product);
            Assert.IsNotNull(result);

            ActionResult ProductResult = result.Result;
            Assert.AreEqual(ProductResult.GetType(), typeof(BadRequestResult));
        }
     
        
        [Test]
        public void DeleteProduct_valid_ShouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);
            Assert.IsNotNull(_ProductsController);
            ProductsModel product = new ProductsModel() { ProductId = 3, ProductName = "Moniter", Description = "24' lg 1080p", Price = 341.41, StockLevel = 19 };
            Assert.IsNotNull(product);

            _ProductsController.DeleteProdcut(product);

            ActionResult<ProductsModel> result = _ProductsController.getProduct(null);
            Assert.IsNotNull(result);

            ActionResult ProductResult = result.Result;
            Assert.AreEqual(ProductResult.GetType(), typeof(BadRequestResult));



        }

        [Test]
        public void DeleteProduct_invalid_ShouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);
            Assert.IsNotNull(_ProductsController);
            ProductsModel product = null;
            Assert.IsNull(product);

            ActionResult<ProductsModel> result = _ProductsController.DeleteProdcut(product); 
            Assert.IsNotNull(result);

            ActionResult ProductResult = result.Result;
            Assert.AreEqual(ProductResult.GetType(), typeof(BadRequestResult));
        }
    }
}