﻿using DAL.Entity;
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
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AppUserAndRole> AppUserAndRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserAndComment> UserAndComments { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<SupplierOrderDetail> SupplierOrderDetails { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new SubCategoryMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new OrderMap());

            modelBuilder.Entity<AppUserAndRole>().HasKey(x => new
            {
                x.AppUserId,
                x.AppUserRoleId
            });

            modelBuilder.Entity<UserAndComment>().HasKey(x => new
            {
                x.AppUserId,
                x.UserCommentId,
                x.ProductId
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
