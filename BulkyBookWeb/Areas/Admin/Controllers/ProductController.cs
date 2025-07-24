 using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product>products= _unitOfWork.Product.GetAll();
            return View(products);

        }

        public IActionResult Upsert(int? id)
        {
            ProductVm productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ConverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);

                //update product
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVm productVm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\Product");
                    var extension = Path.GetExtension(file.FileName);

                    if (productVm.Product.ImageUrl!=null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    productVm.Product.ImageUrl = @"\image\Product\" + fileName + extension;
                }

                if (productVm.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVm.Product);
                    TempData["Succes"] = "Başarıyla Eklendi";
                }
                else
                {       
                    _unitOfWork.Product.Update(productVm.Product);
                    TempData["Succes"] = "Başarıyla Güncellendi";
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            // Eğer valid değilse listeyi tekrar doldur!
            productVm.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            productVm.ConverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(productVm);
        }






        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);


        //    return View(product);
        //}



        //[HttpDelete]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(Product product)
        //{



        //    _unitOfWork.Product.Remove(product);
        //    _unitOfWork.Save();
        //    TempData["Succes"] = "Başarılıya Silindi";

        //    return RedirectToAction(nameof(Index));

        //}
        #region API CAlls

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _unitOfWork.Product.GetAll(null,"Category,CoverType");
            return Json(new { data = product });
         }


        [HttpDelete("Admin/Product/Delete/{id}")]

        public IActionResult Delete(int? id)
        {
           
            var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

            if (product==null)
            {

                return Json(new {succes=false,message="Error While Deleting"});
            }

             
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }


            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new { succes = true, message = "Delete SuccesFull" });
        }

        #endregion
    }
}
