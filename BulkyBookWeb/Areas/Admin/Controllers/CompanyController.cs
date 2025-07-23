using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IActionResult Index()
        {
            return View();
        }

        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

         

        public IActionResult Upsert(int? id)
        {
            Company company = new();

            

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);

                //update product
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

 
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                    TempData["Succes"] = "Başarıyla Eklendi";
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                    TempData["Succes"] = "Başarıyla Güncellendi";
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            // Eğer valid değilse listeyi tekrar doldur!
            

            return View(company);
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
            var companies = _unitOfWork.Company.GetAll();
            return Json(new { data = companies });
        }


        [HttpDelete("Admin/Product/Delete/{id}")]

        public IActionResult Delete(int? id)
        {

            var companies = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);

            if (companies == null)
            {

                return Json(new { succes = false, message = "Error While Deleting" });
            }


             

            _unitOfWork.Company.Remove(companies);
            _unitOfWork.Save();
            return Json(new { succes = true, message = "Delete SuccesFull" });
        }

        #endregion
    }
}
 