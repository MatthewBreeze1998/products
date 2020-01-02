using Cloud_System_dev_ops_System_dev_ops.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_System_dev_ops_System_dev_ops.Repo
{
    public interface IProductsRepositry

    {
        ProductsModel CreateProduct(Models.ProductsModel products);

        ProductsModel GetProduct(int? id);

        IEnumerable<Models.ProductsModel> GetAllProduct();

        ProductsModel EditProduct(ProductsModel products);

        ProductsModel DeleteProduct(ProductsModel Product);
        
    }
}
