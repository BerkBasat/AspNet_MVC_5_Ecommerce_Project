using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Brand : EntityRepository
    {
        public string BrandName { get; set; }
        public List<Product> Products { get; set; }
    }
}
