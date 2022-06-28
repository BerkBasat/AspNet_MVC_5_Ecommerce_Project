using DAL.Entity;
using DAL.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class ApplicationContext:DbContext
    {
        //todo 1: Order, Order Details, Supplier Orders(tedarikçiden temin edilen ürünler) daha sonra eklenecek!
        //todo 2: Sipariş takibi için Order Status enum oluşturulacak!

        public ApplicationContext()
        {
            Database.Connection.ConnectionString = "Server=LAPTOP-55FON9IT;Database=TechnomarktDB;Trusted_Connection=True;";
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new SubCategoryMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new BrandMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
