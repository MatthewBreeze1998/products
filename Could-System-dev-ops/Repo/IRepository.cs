using Cloud_System_dev_ops_System_dev_ops.Models;
using System.Collections.Generic;

namespace Cloud_System_dev_ops_System_dev_ops.Repo
{
    public interface IRepository<Product>
    {
        bool UpdateObject(ProductsModel Object);

        ProductsModel CreateObject(ProductsModel Object);

        IEnumerable<Models.ProductsModel> GetObject();
    }
}