using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product>products= _unitOfWork.Product.GetAll();
            return View(products);

        }

        public IActionResult Upsert(int id)
        {
            Product product = new Product();
            IEnumerable<SelectListItem> CategoryList=_unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            IEnumerable<SelectListItem> ConverTypeList=_unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            if (id == null || id == 0)
            {
                return View(product);
             }
            var ProductFromDB = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

            if (ProductFromDB == null)
            {
                return NotFound();
            }

            return View(ProductFromDB);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {

            

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["Succes"] = "Başarılıya Güncellendi";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);


            return View(product);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {



            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["Succes"] = "Başarılıya Silindi";

            return RedirectToAction(nameof(Index));

        }
    }
}
