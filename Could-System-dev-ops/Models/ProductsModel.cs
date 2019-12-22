using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Could_System_dev_ops.Models
{
    public class ProductsModel
    {  
        public int ProductId { get; set; }
        
        public String ProductName { get; set; }

        public String Description { get; set; }

        public Double Price { get; set;}

       public int StockLevel { get; set; }
    }
}
