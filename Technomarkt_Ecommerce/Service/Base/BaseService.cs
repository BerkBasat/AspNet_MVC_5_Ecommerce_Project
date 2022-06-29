using Core;
using Core.Service;
using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public class BaseService<T> : ICoreService<T> where T : EntityRepository
    {
        ApplicationContext db = new ApplicationContext();

        public string Add(T model)
        {
            try
            {
                model.ID = Guid.NewGuid();
                db.Set<T>().Add(model);
                db.SaveChanges();
                return $"Data added successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public bool Any(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Any(exp);
        }

        public string Deactivate(Guid id)
        {
            try
            {
                T deactivated = GetById(id);
                deactivated.Status = Core.Enums.Status.Inactive;
                Update(deactivated);
                return "Data status changed to inactive!";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(T model)
        {
            try
            {
                db.Set<T>().Remove(model);
                db.SaveChanges();
                return "Data deleted successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public T GetById(Guid id)
        {
            return db.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Where(exp).ToList();
        }

        public List<T> GetList()
        {
            return db.Set<T>().ToList();
        }

        public string Update(T model)
        {
            try
            {
                T updated = GetById(model.ID);
                DbEntityEntry entity = db.Entry(updated);
                entity.CurrentValues.SetValues(model);
                db.SaveChanges();
                return "Data updated successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
