﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Could_System_dev_ops.Models;
using Could_System_dev_ops.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Could_System_dev_ops.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private ProductsRepo _ProductsRepo;
        private ReSaleService _ReSaleList;
        public ProductsController(ProductsRepo Products, ReSaleService reSale )
        {
            _ProductsRepo = Products;
            _ReSaleList = reSale;
        }
        [Route("CreatProduct/{product}")]
        [HttpPost]
        public ActionResult<ProductsModel> CreateProdcuts(ProductsModel products)
        {

            if(products == null)
            {
                return NotFound();
            }
            _ProductsRepo.CreateProduct(products);
            return products;

        }
        [Route("EditProducts")]
        [HttpPost]
        public ActionResult<ProductsModel> EditProduct(ProductsModel product)
        {
            if(product == null)
            {
                return NotFound();
            }
            _ProductsRepo.EditProducts(product);
            return product;
        }


        [Route("GetProduct/{id}")]
        [HttpGet]
        public ActionResult<ProductsModel> getProducts(int id)
        {
            ProductsModel createProdcuts = _ProductsRepo.GetProduct(id);

            if(createProdcuts == null)
            {
                return NotFound();
            }

            return createProdcuts;
        }
        
        [Route("GetAllProducts")]
        [HttpGet]
        public IEnumerable<ProductsModel> GetAllProducts()
        {
            IEnumerable<ProductsModel> All = _ProductsRepo.GetAllProduct();
            return All; 
        }

        [Route("UpdateStock/{id,NewStock}")]
        [HttpPost]
        public ActionResult<ProductsModel> UpdateStock(ProductsModel product, int NewStock)
        {     
            if(product == null)
            {
                return NotFound();
            }
            if(NewStock < 1)
            {
                return NotFound();
            }
            product.StockLevel = product.StockLevel + NewStock;
            return _ProductsRepo.EditProducts(product);
        }

      
        
        [Route("SetReSale/{id}")]
        [HttpPost]
        public ActionResult<ProductsModel> SetResale(ProductsReSaleUpdate Update)
        {
            if(Update == null)
            {
                return NotFound();
            }

            ProductsModel products = Update.product;
            ReSaleMetaData Resale = Update.ReSale;
            products.Price = Resale.NewPrice;
            return _ProductsRepo.EditProducts(products);
        }


    }
}