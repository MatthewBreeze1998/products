using Could_System_dev_ops.Controllers;
using Could_System_dev_ops.Models;
using Could_System_dev_ops.Repo;
using Microsoft.AspNetCore.Mvc;
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

        public void CreateProduct_validUser_ShouldObject()
        {


            Assert.IsNotNull(_ProductsRepo);
            Assert.IsNotNull(_ProductsController);
            ProductsModel product = new ProductsModel() { ProductId = 4, ProductName = "levi jeans", Description = "blue Jeans", Price = 123.12, StockLevel = 19 };
            Assert.IsNotNull(user);

            int currentMaxId = _ProductsController.getProducts().Value.Max();
            Assert.GreaterOrEqual(currentMaxId, 1);

            ActionResult<ProductsModel> result = _ProductsController.CreateProdcuts(product);
            Assert.IsNotNull(result);

            ActionResult usersResult = result.Result;
            Assert.AreEqual(usersResult.GetType(), typeof(CreatedAtActionResult));

            CreatedAtActionResult createdUserResult = (CreatedAtActionResult)usersResult;
            Assert.IsNotNull(createdUserResult);
            Assert.AreEqual(createdUserResult.Value.GetType(), typeof(ProductsModel));

            ProductsModel UserValue = (ProductsModel)createdUserResult.Value;
            Assert.IsNotNull(UserValue);

            Assert.AreEqual(currentMaxId + 1, UserValue.UserId);
            Assert.AreEqual(user.FirstName, UserValue.FirstName);
            Assert.AreEqual(user.LastName, UserValue.LastName);
            Assert.AreEqual(user.Email, UserValue.Email);
            Assert.AreEqual(user.isActive, UserValue.isActive);
            Assert.AreEqual(user.PurchaseAbility, user.PurchaseAbility);
        }

        [Test]
        public void CreateProduct_InvalidUser_ShouldObject()
        {
            _userRepo = new FakeUserRepo(null);

            Assert.IsNotNull(_userRepo);
            Assert.IsNotNull(_userController);
            UsersModel user = new UsersModel() { UserId = 0, FirstName = "", LastName = "", Email = "", isActive = false, PurchaseAbility = false };
            Assert.IsNotNull(user);

            int currentMaxId = _userController.GetAllUsers().;
            Assert.GreaterOrEqual(currentMaxId, 1);

            ActionResult<UsersModel> result = _userController.CreateUser(user);
            Assert.IsNotNull(result);

            ActionResult usersResult = result.Result;
            Assert.AreEqual(usersResult.GetType(), typeof(CreatedAtActionResult));

            CreatedAtActionResult createdUserResult = (CreatedAtActionResult)usersResult;
            Assert.IsNotNull(createdUserResult);
            Assert.AreEqual(createdUserResult.Value.GetType(), typeof(UsersModel));

            UsersModel UserValue = (UsersModel)createdUserResult.Value;
            Assert.IsNotNull(UserValue);

            Assert.AreEqual(currentMaxId + 1, UserValue.UserId);
            Assert.AreEqual(user.FirstName, UserValue.FirstName);
            Assert.AreEqual(user.LastName, UserValue.LastName);
            Assert.AreEqual(user.Email, UserValue.Email);
            Assert.AreEqual(user.isActive, UserValue.isActive);
            Assert.AreEqual(user.PurchaseAbility, user.PurchaseAbility);
        }
    }
}