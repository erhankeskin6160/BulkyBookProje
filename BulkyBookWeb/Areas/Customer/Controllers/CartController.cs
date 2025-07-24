using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitwork;
        public ShopCartVm ShopCartVm { get; set; }  

        public int OrderTotal { get; set; } 

        public CartController(IUnitOfWork unitwork)
        {
            _unitwork = unitwork;
        }

        public IActionResult Index()
        {
            var claimsIdentity =(ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                ShopCartVm = new ShopCartVm()
                {
                    ListCart =_unitwork.ShoppingCart.GetAll(x=>x.ApplicationUserId == claim.Value, includeProperties: "Product")

                };

            foreach (var cart in ShopCartVm.ListCart) 
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                ShopCartVm.CartTotal += (cart.Price * cart.Count);
            }


                return View(ShopCartVm);
        }


        public IActionResult Summary() 
        {
            return View();
        }

        public  IActionResult Plus(int cartId) 
        {
            var cart = _unitwork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            _unitwork.ShoppingCart.IncrementCount(cart, 1);
            _unitwork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitwork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            if (cart.Count<=1)
            {
                _unitwork.ShoppingCart.Remove(cart);
            }
            else
            {
                _unitwork.ShoppingCart.DecrementCount(cart, 1);
            }
               
            _unitwork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitwork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            _unitwork.ShoppingCart.Remove(cart);
            _unitwork.Save();
            return RedirectToAction(nameof(Index));
        }


        private double GetPriceBasedOnQuantity(double quantity,double price ,double price50, double price100) 
        {
            if (quantity<50)
            {
                return price;
            }
            else 
            {
                if (quantity < 100) 
                {
                    return price50;
                }                    
                return price100;

            }
             
            // Default case if none of the conditions are met
        }
    }
}
