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
        private IRepository<ProductsModel> _ProductsRepo;

        public ProductsController(IRepository<ProductsModel> Products)
        {

            _ProductsRepo = Products;
        }
        [Route("CreateProduct")]//Route
        [Authorize(Policy = "Staffpol")]
        [HttpPost]
        public ActionResult<ProductsModel> CreateProdcut(ProductsModel product)
        {

            if (product == null)// checks if Products is null
            {
                return BadRequest();// Badresult 
            }

            ProductsModel livemodel = _ProductsRepo.CreateObject(product); 

            if(livemodel == null)
            {
                return BadRequest();
            }
                     
            return CreatedAtAction(nameof(getProduct), new { id = product.ProductId }, product); // calls xreate in interface and returns the new product

        }
        [Route("DeleteProduct")]
        [Authorize(Policy = "Manager")]
        [HttpPost]
        public ActionResult<ProductsModel> DeleteProduct(ProductsModel Product)
        {

            if (Product == null)// checks if Products is null
            {
                return BadRequest();// not found if null
            }
            ProductsModel LiveModel = _ProductsRepo.GetObject().FirstOrDefault(x => x.ProductId == Product.ProductId);

            if(LiveModel == null)
            {
                return BadRequest();
            }

            LiveModel = _ProductsRepo.DeleteObject(LiveModel);

            if (LiveModel != null)
            {
                return Conflict();
            }

            return LiveModel;
        }


        [Route("EditProducts")]//Route
        [Authorize(Policy = "Staffpol")]
        [HttpPost]
        public ActionResult<ProductsModel> EditProduct(ProductsModel product)
        {
            if(product == null)// checks if Products is null
            {
                return NotFound();// not found if null
            }

            ProductsModel LiveModel = _ProductsRepo.GetObject().FirstOrDefault(x => x.ProductId == product.ProductId);

            if(LiveModel == null)
            {
                return BadRequest();
            }

            LiveModel.ProductName = product.ProductName;
            LiveModel.Description = product.Description;
            LiveModel.Price = product.Price;
            LiveModel.StockLevel = product.StockLevel;
            LiveModel.SupplierName = product.SupplierName;

            LiveModel = _ProductsRepo.UpdateObject(LiveModel);
            
            if(LiveModel == null)
            {
                return Conflict();
            }
            return LiveModel; // calls edit fuction from interface
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
            
            ProductsModel product = _ProductsRepo.GetObject().FirstOrDefault(x => x.ProductId == id);

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
            return _ProductsRepo.GetObject(); // call interface funcion and retuns all prodcuts as IEnumrable
        }
        
        [Route("UpdateStock")]//Route
        [Authorize(Policy = "Staffpol")]
        [HttpPost]
        public ActionResult<ProductsModel> UpdateStock(UpdateStockModel Stock)
        {
            if (Stock == null)// checks if Products is null
            {
                return BadRequest();// not found if null
            }
            ProductsModel product = _ProductsRepo.GetObject().FirstOrDefault(x => x.ProductId == Stock.ProductId);// product to update

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

            ProductsModel Updatededproduct = _ProductsRepo.UpdateObject(product);

            if(Updatededproduct == null)
            {
                return Conflict();
            }

            return Updatededproduct;// calls edit and updates the index with the new sock level
        }
        
  
      
        
        [Route("SetReSale/{Price}")]//Route
        [Authorize(Policy = "Staffpol")]
        [HttpPost]
        public ActionResult<ProductsModel> SetResale(ProductsModel product, Double Price)
        {
            if(product == null)// checks if Update is null
            {
                return BadRequest();// not found if null
            }

            if(Price <= 0)
            {
                return BadRequest();
            }

            ProductsModel liveModel = _ProductsRepo.GetObject().FirstOrDefault(x => x.ProductId == product.ProductId);
           
            if(liveModel == null)
            {
                return BadRequest();
            }

            liveModel.Price = Price;

            liveModel = _ProductsRepo.UpdateObject(liveModel);

            if(liveModel == null || liveModel.Price != Price)
            {
                return Conflict();
            }

             // changes old price to the resale price
            return liveModel;// calls edit from interface and updates the index  
        }
    }
}