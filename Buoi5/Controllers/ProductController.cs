using Buoi5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Buoi5.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        // GET: ProductController
        public ActionResult Index(string? searchString, string? sortOrder, int page = 1)
        {
            // 1. Lấy dữ liệu gốc
            var data = from p in Data.SeedData.Products
                       join c in Data.SeedData.Categories on p.CategoryId equals c.CategoryId
                       select new ProductViewModel
                       {
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           ProductPrice = p.ProductPrice,
                           ProductDescriptiion = p.ProductDescription,
                           CategoryName = c.CategoryName
                       };

            // 2. Xử lý Tìm kiếm (nếu có)
            if (!string.IsNullOrEmpty(searchString))
            {
                string keyWord = searchString.ToLower();
                data = data.Where(p => (p.ProductName != null && p.ProductName.ToLower().Contains(keyWord))
                                    || (p.CategoryName != null && p.CategoryName.ToLower().Contains(keyWord)));
            }

            // 3. Xử lý Sắp xếp theo ID và Name
            // Lưu trạng thái đảo chiều sắp xếp vào ViewBag để bấm lần 1 tăng dần, bấm lần 2 giảm dần
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";

            switch (sortOrder)
            {
                case "id_desc":
                    data = data.OrderByDescending(p => p.ProductId);
                    break;
                case "name_asc":
                    data = data.OrderBy(p => p.ProductName);
                    break;
                case "name_desc":
                    data = data.OrderByDescending(p => p.ProductName);
                    break;
                default: // Mặc định sắp xếp theo ID tăng dần
                    data = data.OrderBy(p => p.ProductId);
                    break;
            }

            // 4. Xử lý Phân trang
            int pageSize = 2; // Số lượng sản phẩm trên 1 trang (bạn đang có 3 sản phẩm nên để 2 để thấy rõ 2 trang)
            int totalItems = data.Count(); // Tổng số sản phẩm sau khi lọc
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize); // Tính tổng số trang

            // Cắt lấy đúng số sản phẩm của trang hiện tại bằng Skip và Take
            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // 5. Truyền các thông số sang View để giữ trạng thái khi chuyển trang
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedData);
        }
        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            var category = Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product?.CategoryId);
            var productViewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescriptiion = product.ProductDescription,
                CategoryName = category?.CategoryName

            };

            return View(productViewModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                newProduct.ProductId = Data.SeedData.Products.Max(P => P.ProductId + 1);
                Data.SeedData.Products.Add(newProduct);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName");
            return View(newProduct);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName",product.CategoryId);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product editProduct)
        {
            if (ModelState.IsValid)
            {
                var data = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == editProduct.ProductId);
                if(data == null)
                {
                    return NotFound();
                }
                data.ProductName = editProduct.ProductName;
                data.ProductPrice = editProduct.ProductPrice;
                data.ProductDescription = editProduct.ProductDescription;
                data.CategoryId = editProduct.CategoryId;
                return RedirectToAction(nameof(Index));
                    
            }
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", editProduct.CategoryId);
            return View(editProduct);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }
            var category = Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            ViewBag.CategoryName = category?.CategoryName;
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var deleteProduct = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if(deleteProduct == null){
                return NotFound();
            }
            Data.SeedData.Products.Remove(deleteProduct);
            return RedirectToAction(nameof(Index));
        }
    }
}
