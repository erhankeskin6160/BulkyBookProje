

using BulkyBook.DataAcces.Data;
using BulkyBook.DataAcces.Repository;
using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }


        public IActionResult Edit(int id)
        {
            if (id==null || id ==0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Eror2354", "Kategori ismiyle Displayorder Aynı Olamaz");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["Succes"] = "Başarılıya Güncellendi";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        public IActionResult Create() 
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {

            if (category.Name==category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Eror2354", "Kategori ismiyle Displayorder Aynı Olamaz");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["Succes"] = "Başarılıya Oluşturuldu";

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        public IActionResult Delete(int? id) 
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetFirstOrDefault(x=>x.Id==id);
             

            return View(category);
        }



      [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {



            _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
            TempData["Succes"] = "Başarılıya Silindi";

            return RedirectToAction(nameof(Index));
            
        }
    }
}
