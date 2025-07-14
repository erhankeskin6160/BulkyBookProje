using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType>coverTypes= _unitOfWork.CoverType.GetAll();
            return View(coverTypes);

        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var covrtypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (covrtypeFromDb == null)
            {
                return NotFound();
            }

            return View(covrtypeFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {

            

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(coverType);
                _unitOfWork.Save();
                TempData["Succes"] = "Başarılıya Güncellendi";
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {

            

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["Succes"] = "Başarılıya Oluşturuldu";

                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);


            return View(category);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType coverType)
        {



            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();
            TempData["Succes"] = "Başarılıya Silindi";

            return RedirectToAction(nameof(Index));

        }
    }
}
