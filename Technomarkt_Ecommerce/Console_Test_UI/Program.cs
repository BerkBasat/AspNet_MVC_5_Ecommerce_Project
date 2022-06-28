using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Test_UI
{
    class Program
    {
        static void Main(string[] args)
        {
            CategoryService categoryService = new CategoryService();

            foreach (var category in categoryService.GetList())
            {
                Console.WriteLine(category.CategoryName);
            }

            var updated = categoryService.GetById(Guid.Parse("BF75E1B8-97A8-46EA-8889-250E28330685"));
            updated.CategoryName = "update test";

            categoryService.Delete(updated);

            Console.Read();
        }
    }
}
