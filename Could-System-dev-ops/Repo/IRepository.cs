using Could_System_dev_ops.Models;
using System.Collections.Generic;

namespace Could_System_dev_ops.Repo
{
    public interface IRepository<Product>
    {
        bool UpdateObject(ProductsModel Object);

        ProductsModel CreateObject(ProductsModel Object);

        IEnumerable<Models.ProductsModel> GetObject();
    }
}