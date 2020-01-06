using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud_System_dev_ops.Models;
using Cloud_System_dev_ops.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cloud_System_dev_ops.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private IProductsRepositry _ProductsRepo;
        private IReSaleRepositry _ReSaleRepo;
        public ProductsController(IProductsRepositry Products)
        {

            _ProductsRepo = Products;
        }
        [Route("CreatProduct/")]//Route
        [Authorize(Policy = "Staffpol")]
        [HttpPost]
        public ActionResult<ProductsModel> CreateProdcut(ProductsModel product)
        {

            if (product == null)// checks if Products is null
            {
                return BadRequest();// Badresult 
            }
            if (product.ProductId <= 0)// checks valid id
            {

                return BadRequest();
            }

            int newId = _ProductsRepo.GetAllProduct().Max(x => x.ProductId + 1);// gats max id and adds one
            product.ProductId = newId; // sets new id

            _ProductsRepo.CreateProduct(product);
            return CreatedAtAction(nameof(getProduct), new { id = product.ProductId }, product); // calls xreate in interface and returns the new product

        }
        [Route("DeleteProduct/")]
        [Authorize(Policy = "Manager")]
        public ActionResult<ProductsModel> DeleteProduct(ProductsModel Product)
        {

            if (Product == null)// checks if Products is null
            {
                return BadRequest();// not found if null
            }
            if (Product.ProductId <= 0)// checks valid id
            {

                return BadRequest();
            }
            return _ProductsRepo.DeleteProduct(Product); ; // calls Delete in interface and returns the new product
        }


        [Route("EditProducts/")]//Route
        [Authorize(Policy = "Staffpol")]
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
                return NotFound();// not found if null
            }
            ProductsModel product = _ProductsRepo.GetProduct(id);

            if(product == null)
            {
                return NotFound();
            }
            return product; // gets prodcut by id returns product
        }
        
        [Route("GetAllProducts")]//Route
        [HttpGet]
        public IEnumerable<ProductsModel> GetAllProducts()
        {
            return _ProductsRepo.GetAllProduct(); // call interface funcion and retuns all prodcuts as IEnumrable
        }
        
        [Route("UpdateStock/")]//Route
        [Authorize(Policy = "Staffpol")]
        [HttpPost]
        public ActionResult<ProductsModel> UpdateStock(UpdateStockModel Stock)
        {
            if (Stock == null)// checks if Products is null
            {
                return BadRequest();// not found if null
            }
            ProductsModel product = _ProductsRepo.GetProduct(Stock.ProductId);// product to update

            if (Stock.RequestType.ToLower() == "order")// checks if its to reduce stock
            {
                product.StockLevel = product.StockLevel - Stock.StockCount;// reduces stocks
            }
            else if (Stock.RequestType.ToLower() == "purchaserequest")// checks  purchase request
            {
                product.StockLevel = product.StockLevel + Stock.StockCount; // adds stock
            }
            else
            {
                return BadRequest();
            }  // else bad request
            return _ProductsRepo.EditProduct(product);// calls edit and updates the index with the new sock level
        }
        
        public async Task<ReSaleMetaData> GetResaleInfo(ReSaleMetaData reSale)
        {
            return await _ReSaleRepo.SetReSale(reSale); 
        }
      
        
        [Route("SetReSale/{Price}")]//Route
        [Authorize(Policy = "Staffpol")]
        [HttpPost]
        public ActionResult<ProductsModel> SetResale(ProductsModel product, Double Price)// get pro
        {
            if(product == null)// checks if Update is null
            {
                return NotFound();// not found if null
            }
            ReSaleMetaData reSale = null;
            reSale.ProductId = product.ProductId;
            reSale.CurrentPrice = Price;
            reSale.NewPrice = Price;
            product.Price = Price; // changes old price to the resale price
            return _ProductsRepo.EditProduct(product);// calls edit from interface and updates the index  
        }
    }
}