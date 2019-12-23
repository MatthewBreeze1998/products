using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Could_System_dev_ops.Models;
using Could_System_dev_ops.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Could_System_dev_ops.Controllers
{
    [Route("api/Products")]
    [ApiController]
    [Authorize]
    public class ProductsController : Controller
    {
        private IProductsRepositry _ProductsRepo;
        public ProductsController(IProductsRepositry Products )
        {
            _ProductsRepo = Products;
        }
        [Route("CreatProduct")]//Route
        [HttpPost]
        public ActionResult<ProductsModel> CreateProdcut(ProductsModel product)
        {

            if(product == null)// checks if Products is null
            {
                return BadRequest();// Badresult 
            }
            if (product.ProductId <= 0)// checks valid id
            {
         
                return BadRequest();
            }
            _ProductsRepo.CreateProduct(product);
            return CreatedAtAction(nameof(getProduct), new { id = product.ProductId },product); // calls xreate in interface and returns the new product

        }

        public ActionResult<ProductsModel> DeleteProdcut(ProductsModel product)
        {

            if (product == null)// checks if Products is null
            {
                return BadRequest();// not found if null
            }
            if (product.ProductId <= 0)// checks valid id
            {

                return BadRequest();
            }
            ProductsModel deletedProduct = _ProductsRepo.DeleteProduct(product);

            return deletedProduct; // calls Delete in interface and returns the new product

        }
        [Route("EditProducts")]//Route
        [HttpPost]
        public ActionResult<ProductsModel> EditProduct(ProductsModel product)
        {
            if(product == null)// checks if Products is null
            {
                return NotFound();// not found if null
            }
            if(product.ProductId <= 0)// checks valid id
            {

                return NotFound();
            }
            
            return _ProductsRepo.EditProduct(product); // calls edit fuction from interface
            // retruns edited data
        }


        [Route("GetProduct/{id}")]//Route
        [HttpGet]
        public ActionResult<ProductsModel> getProduct(int? id)
        {
            if(id == null)// checks if id is valid
            {
                return BadRequest();// not found if null
            }
            ProductsModel product = _ProductsRepo.GetProduct(id);
            return product; // gets prodcut by id returns product
        }
        
        [Route("GetAllProducts")]//Route
        [HttpGet]
        public IEnumerable<ProductsModel> GetAllProducts()
        {
            return _ProductsRepo.GetAllProduct(); // call interface funcion and retuns all prodcuts as IEnumrable
        }

        [Route("UpdateStock/{id,NewStock}")]//Route
        [HttpPost]
        public ActionResult<ProductsModel> UpdateStock(ProductsModel product, int NewStock)
        {     
            if(product == null)// checks if Products is null
            {
                return NotFound();// not found if null
            }
            if(NewStock < 1)// checks theres a new stock add
            {
                return NotFound();// not found if less than one
            }
            product.StockLevel = product.StockLevel + NewStock; // gets stocklevel gets new stock and adds them
            return _ProductsRepo.EditProduct(product);// calls edit and updates the index with the new sock level
        }

      
        
        [Route("SetReSale/{id}")]//Route
        [HttpPost]
        public ActionResult<ProductsModel> SetResale(ProductsReSaleUpdate Update)
        {
            if(Update == null)// checks if Update is null
            {
                return NotFound();// not found if null
            }

            ProductsModel products = Update.product; // gives products a products model
            ReSaleMetaData Resale = Update.ReSale;// gives resale resale metadata
            products.Price = Resale.NewPrice; // changes old price to the resale price
            return _ProductsRepo.EditProduct(products);// calls edit from interface and updates the index 
        }


    }
}