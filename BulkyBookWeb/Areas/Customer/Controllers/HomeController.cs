using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models;
 
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
          
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll("Category,CoverType");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart shoppingCart = new() {Count=1, Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id, "Category,CoverType") };
             return View(shoppingCart);
        }

        public ViewResult aa()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

         
    }
}
