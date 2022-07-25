using DAL.Context;
using DAL.Entity;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class SupplierOrderService:BaseService<SupplierOrder>
    {
        ApplicationContext db = new ApplicationContext();

        public Order Add(Order model)
        {
            model.ID = Guid.NewGuid();
            var result = db.Set<Order>().Add(model);
            db.SaveChanges();

            return result;
        }
    }
}
