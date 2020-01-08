using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud_System_dev_ops.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud_System_dev_ops.Repo
{
   public class EntityFrameWorkProductsRepositry : IRepository<ProductsModel>
    {
        private readonly IServiceScope _scope;
        private readonly ProductsDataBaseContext _context;

        public EntityFrameWorkProductsRepositry(IServiceProvider service)
        {
            _scope = service.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<ProductsDataBaseContext>();

        }
        public ProductsModel CreateObject(ProductsModel Object)
        {
            _context.Products.Add(Object);
            _context.SaveChanges();

            return Object;
        }
        public ProductsModel DeleteObject(ProductsModel Object)
        {
            try
            {
                _context.Products.Remove(Object);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Object;
            }

            return null;
        }

        public IEnumerable<ProductsModel> GetObject()
        {
            return _context.Products;
        }
        public ProductsModel UpdateObject(ProductsModel Object)
        {
            try
            {
                _context.Products.Update(Object);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Object;
        }
    }
}
