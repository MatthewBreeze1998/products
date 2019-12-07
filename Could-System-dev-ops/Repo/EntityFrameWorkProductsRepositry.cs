using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Could_System_dev_ops.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Could_System_dev_ops.Repo
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
    public IEnumerable<ProductsModel> GetObject()
        {
            return _context.Products;
        }
    public bool UpdateObject(ProductsModel Object)
        {
            try
            {
                _context.Products.Update(Object);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
        
    }
}
