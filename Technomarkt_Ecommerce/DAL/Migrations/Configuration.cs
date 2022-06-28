﻿namespace DAL.Migrations
{
    using DAL.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Context.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DAL.Context.ApplicationContext context)
        {
            //Categories
            List<Category> categories = new List<Category>()
            {
                new Category{ID = Guid.NewGuid(), CategoryName="Computers"},
                new Category{ID = Guid.NewGuid(), CategoryName="Electronics"},
                new Category{ID = Guid.NewGuid(), CategoryName="Gaming Consoles"}
            };

            if (!context.Categories.Any())
            {
                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
            }

            //SubCategories
            List<SubCategory> subCategories = new List<SubCategory>()
            {
                new SubCategory{ID = Guid.NewGuid(),
                    SubCategoryName= "Desktop",
                    CategoryId = categories.FirstOrDefault(x => x.CategoryName =="Computers").ID
                },

                new SubCategory{ID = Guid.NewGuid(),
                    SubCategoryName = "Laptop",
                    CategoryId = categories.FirstOrDefault(x => x.CategoryName == "Computers").ID
                },

                new SubCategory{ID = Guid.NewGuid(),
                    SubCategoryName = "Tablets",
                    CategoryId = categories.FirstOrDefault(x => x.CategoryName == "Computers").ID
                },

                new SubCategory{ID = Guid.NewGuid(),
                    SubCategoryName = "Smart Phone",
                    CategoryId = categories.FirstOrDefault(x => x.CategoryName == "Electronics").ID
                },

                new SubCategory{ID = Guid.NewGuid(),
                    SubCategoryName = "Smart Watch",
                    CategoryId = categories.FirstOrDefault(x => x.CategoryName == "Electronics").ID
                },
                new SubCategory{ID = Guid.NewGuid(),
                    SubCategoryName = "Playstation",
                    CategoryId = categories.FirstOrDefault(x => x.CategoryName == "Gaming Consoles").ID
                },
                new SubCategory{ID = Guid.NewGuid(),
                    SubCategoryName = "XBOX",
                    CategoryId = categories.FirstOrDefault(x => x.CategoryName == "Gaming Consoles").ID
                },
            };

            if (!context.SubCategories.Any())
            {
                foreach (var subCategory in subCategories)
                {
                    context.SubCategories.Add(subCategory);
                    context.SaveChanges();
                }
            }

            //Suppliers
            List<Supplier> suppliers = new List<Supplier>()
            {
                new Supplier{ID = Guid.NewGuid(),
                    CompanyName = "YY İthalat Ltd. Şti.",
                    PhoneNumber = "0 212 671 52 50",
                    Address = "İstanbul",
                    Email = "yy_ithalat@gmail.com"},

                new Supplier{ID = Guid.NewGuid(),
                    CompanyName = "Arena Bilgisayar Sanayi ve Ticaret A.Ş.",
                    PhoneNumber = "0 212 364 64 64",
                    Address = "İstanbul",
                    Email = "arenabilgisayar@gmail.com"},

                new Supplier{ID = Guid.NewGuid(),
                    CompanyName = "Metro Elektronik",
                    PhoneNumber = "0 216 528 10 00",
                    Address = "İzmir",
                    Email = "metro_elektronik@gmail.com"},

            };

            if (!context.Suppliers.Any())
            {
                foreach (var supplier in suppliers)
                {
                    context.Suppliers.Add(supplier);
                    context.SaveChanges();
                }
            }

            //Brands
            List<Brand> brands = new List<Brand>()
            {
                new Brand{ID = Guid.NewGuid(), BrandName = "Apple" },
                new Brand{ID = Guid.NewGuid(), BrandName = "Samsung"},
                new Brand{ID = Guid.NewGuid(), BrandName = "Playstation"},
                new Brand{ID = Guid.NewGuid(), BrandName = "Xbox"},
                new Brand{ID = Guid.NewGuid(), BrandName = "Asus"},
            };


            if (!context.Brands.Any())
            {
                foreach (var brand in brands)
                {
                    context.Brands.Add(brand);
                    context.SaveChanges();
                }
            }


            //Products

            List<Product> products = new List<Product>()
            {
                new Product{ID = Guid.NewGuid(), ProductName = "Macbook Pro 14", SupplyCost = 890, UnitPrice = 1999, UnitsInStock = 500, ProductImagePath = "C:\\Users\\Huawei\\Desktop\\macbook-pro.jpg", Description = "Test", SubCategoryId = subCategories.FirstOrDefault(x => x.SubCategoryName == "Laptop").ID, SupplierId = suppliers.FirstOrDefault(x => x.CompanyName == "Arena Bilgisayar Sanayi ve Ticaret A.Ş.").ID, BrandId = brands.FirstOrDefault(x => x.BrandName == "Apple").ID},
                new Product{ID = Guid.NewGuid(), ProductName = "Asus Zenbook", SupplyCost = 420, UnitPrice = 1040, UnitsInStock = 600, ProductImagePath = "C:\\Users\\Huawei\\Desktop\\asus-zenbook.jpg", Description = "Test", SubCategoryId = subCategories.FirstOrDefault(x => x.SubCategoryName == "Laptop").ID, SupplierId = suppliers.FirstOrDefault(x => x.CompanyName == "Metro Elektronik").ID, BrandId = brands.FirstOrDefault(x => x.BrandName == "Asus").ID},
            };

            if (!context.Products.Any())
            {
                foreach (var product in products)
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }

            //AppUser
            List<AppUser> userList = new List<AppUser>()
            {
                new AppUser{ID = Guid.NewGuid(), Username = "Admin", Password = "1234", Email = "admin@gmail.com", PhoneNumber = "5302551166"}
            };
        }
    }
}
