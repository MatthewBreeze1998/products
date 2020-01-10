using Cloud_System_dev_ops.Controllers;
using Cloud_System_dev_ops.Models;
using Cloud_System_dev_ops.Repo;
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
        private IRepository<ProductsModel> _ProductsRepo;
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
                new ProductsModel() {ProductId = 1,ProductName = "levi jeans", Description  =  "blue Jeans", Price = 123.12, StockLevel = 19, SuppilerName = ""},
                new ProductsModel() {ProductId = 2,ProductName = "Black desk", Description  =  "Black desk", Price = 11.4 ,StockLevel = 3, SuppilerName = ""},
                new ProductsModel() {ProductId = 3,ProductName = "Moniter", Description  =  "24' lg 1080p", Price = 341.41 ,StockLevel = 19, SuppilerName = ""}
            };// test data 

            _ProductsRepo = new FakeProductsRepo();
            _ProductsController = new ProductsController(_ProductsRepo);


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
            ProductsModel DeleteProduct = new ProductsModel() { ProductId = 1, ProductName = "levi jeans", Description = "blue Jeans", Price = 123.12, StockLevel = 19, SuppilerName = "" };
            Assert.IsNotNull(DeleteProduct);// DeleteProduct is not null

            ActionResult<ProductsModel> getproduct = _ProductsController.getProduct(DeleteProduct.ProductId);
            Assert.IsNotNull(getproduct);// is nit null

            ActionResult<ProductsModel> product = _ProductsController.DeleteProduct(DeleteProduct); // product is the return of DeleteProduct
            Assert.IsNotNull(product);// product is not null 

            ActionResult<ProductsModel> result = _ProductsController.getProduct(DeleteProduct.ProductId);// result is result of get product
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
        [Test]
        public void GetProduct_valid_shouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;
            ProductsModel product = new ProductsModel() { ProductId = 1, ProductName = "levi jeans", Description = "blue Jeans", Price = 123.12, StockLevel = 19, SuppilerName = "" };// users is a valid user 
            Assert.IsNotNull(product);// user is not null

            ActionResult<ProductsModel> result = _ProductsController.getProduct(product.ProductId).Value;// result is the value of the get user controller function 
            Assert.IsNotNull(result);// result is not null
            Assert.IsNotNull(result.Value);// result value is not null

            ProductsModel productResult = result.Value;//  usersResult resut.value 
            Assert.IsNotNull(productResult);// checks not null

            Assert.AreEqual(product.ProductId, productResult.ProductId);//checks if it matches
            Assert.AreEqual(product.ProductName, productResult.ProductName);//checks if it matches
            Assert.AreEqual(product.Price, productResult.Price);//checks if it matches
            Assert.AreEqual(product.StockLevel, productResult.StockLevel);//checks if it matches
            Assert.AreEqual(product.SuppilerName, productResult.SuppilerName);//checks if it matches
            Assert.AreEqual(product.Description, productResult.Description);//checks if it matches
        }

        [Test]
        public void GetProduct_invalid_shouldObject()
        {

            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;
            ProductsModel product = null;// user is null
            Assert.IsNull(product);// checks user is null

            ActionResult<ProductsModel> result = _ProductsController.getProduct(null);// sets result to getuser function
            Assert.IsNotNull(result);// checks not null

            ActionResult usersResult = result.Result;// sets UserResult to the result of result
            Assert.AreEqual(usersResult.GetType(), typeof(NotFoundResult));// checks the resut is of type badrequest
        }
        [Test]
        public void EditProduct_valid_ShouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;
            ProductsModel product = new ProductsModel() { ProductId = 1, ProductName = "levi jeans", Description = "blue Jeans", Price = 123.12, StockLevel = 19, SuppilerName = "" };// users is a valid user 
            Assert.IsNotNull(product);// user is not null

            product.ProductName = "jack and jone";// change lastname

            ActionResult<ProductsModel> result = _ProductsController.EditProduct(product); //calls edit user and set to result 
            Assert.IsNotNull(result);// checks not null

            ProductsModel updatedProduct = result.Value;// sets updatedUser to result.Value
            Assert.IsNotNull(updatedProduct);   // checks updatedUser not null 
           
            
            Assert.AreEqual(product.ProductName, updatedProduct.ProductName);// checks the name has changed 

        }

        [Test]
        public void EditProduct_invalid_ShouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;
            ProductsModel product = null;// Updateduser is null
            Assert.IsNull(product);// check Updateduser is null


            ActionResult<ProductsModel> result = _ProductsController.EditProduct(product);// sets result to the edit user action
            Assert.IsNotNull(result);// checks its not null

            ActionResult usersResult = result.Result;// sets usersResult to the result.Result
            Assert.AreEqual(usersResult.GetType(), typeof(NotFoundResult));// checks  usersResult is of type bad request 
        }
        [Test]
        public void UpdateStock_valid_shouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;

            UpdateStockModel product = new UpdateStockModel() { ProductId = 1, StockCount = 2 , RequestType = "order" };// users is a valid user 
            Assert.IsNotNull(product);// user is not null

            ActionResult<ProductsModel> getProduct = _ProductsController.getProduct(product.ProductId);// sets result to the edit user action
            Assert.IsNotNull(getProduct);// checks its not null
            Assert.IsNotNull(getProduct.Value);

            ProductsModel getProductresult = getProduct.Value;// sets updatedUser to result.Value
            Assert.IsNotNull(getProductresult);
           
            
            ActionResult<ProductsModel> result = _ProductsController.UpdateStock(product);// sets result to the edit user action
            Assert.IsNotNull(result);// checks its not null

            ProductsModel updatedProduct = result.Value;// sets updatedUser to result.Value
            Assert.IsNotNull(updatedProduct);

            Assert.AreEqual(updatedProduct.StockLevel, getProductresult.StockLevel);

        }
        [Test]
        public void UpdateStock_Invalid_shouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;

            UpdateStockModel product = null;// users is a valid user 
            Assert.IsNull(product);// user is not nul

            ActionResult<ProductsModel> result = _ProductsController.UpdateStock(product);// sets result to the edit user action
            Assert.IsNotNull(result);// checks its not null

            ActionResult productsResult = result.Result;// sets usersResult to the result.Result
            Assert.AreEqual(productsResult.GetType(), typeof(BadRequestResult));// checks  us

        }
        [Test]
        public void SetReSale_valid_shouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;
            ProductsModel product = new ProductsModel() { ProductId = 1, ProductName = "levi jeans", Description = "blue Jeans", Price = 123.12, StockLevel = 19, SuppilerName = "" };// users is a valid user 
            Assert.IsNotNull(product);// user is not null

            double price = 100.00;

            ActionResult<ProductsModel> result= _ProductsController.SetResale(product, price);// sets result to the edit user action
            Assert.IsNotNull(result);// checks its not null
            Assert.IsNotNull(result.Value);

            ProductsModel resaleresult = result.Value;// sets updatedUser to result.Value
            Assert.IsNotNull(resaleresult);

            Assert.AreEqual(resaleresult.Price, price);

        }
        [Test]
        public void SetReSale_Inalid_shouldObject()
        {
            Assert.IsNotNull(_ProductsRepo);// not null repo
            Assert.IsNotNull(_ProductsController);// not null controller;
            ProductsModel product = null;
            Assert.IsNull(product);// user is not null

            double price = 100.00;

            ActionResult<ProductsModel> result = _ProductsController.SetResale(product, price);// sets result to the edit user action
            Assert.IsNotNull(result);// checks its not null

            ActionResult productsResult = result.Result;// sets usersResult to the result.Result
            Assert.AreEqual(productsResult.GetType(), typeof(BadRequestResult));// checks  us

        }

    }
}