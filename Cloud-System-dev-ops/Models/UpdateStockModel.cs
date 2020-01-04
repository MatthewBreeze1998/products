using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_System_dev_ops.Models
{
    public class UpdateStockModel
    {
        public int ProductId { get; set; }

        public int StockCount { get; set; }

        public String RequestType { get; set; }
    }
}
