using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Could_System_dev_ops.Models
{
    public class GetValueDto : DbContext
    {

        public String Message { get; set; }

        public int Number { get; set; }

    }
}
