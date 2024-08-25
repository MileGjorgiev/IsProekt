using EShop.Domain.Domain;
using EShop.Domain.DTO;
using EShop.Domain.Identity;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie_App.Service.Interface;

namespace EShop.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<EShopApplicationUser> userManager;
        private readonly IConcertService _concertService;


        public AdminController(IOrderService orderService, UserManager<EShopApplicationUser> userManager, IConcertService concertService)
        {
            _orderService = orderService;
            this.userManager = userManager;
            _concertService = concertService;
        }
        [HttpGet("[action]")]
        public List<Order> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return _orderService.GetDetailsForOrder(model);
        }
        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDto> model)
        {
            bool status = true;
            foreach (var user in model)
            {
                var userCheck = userManager.FindByEmailAsync(user.Email).Result;
                if (userCheck == null)
                {
                    var neww = new EShopApplicationUser
                    {
                        Email = user.Email,
                        UserName = user.Email,
                        FirstName=user.FirstName,
                        LastName=user.LastName,
                        Address = user.Address,
                        NormalizedEmail = user.Email,
                        UserCart = new ShoppingCart()
                    };
                    var result = userManager.CreateAsync(neww, user.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }
        [HttpPost("[action]")]
        public bool ImportAllConcerts(List<ConcertDto> model)
        {
            bool status = true;
            foreach (var user in model)
            {
             
                    var neww = new Concert
                    {
                        ConcertName = user.ConcertName,
                        ConcertDescription = user.ConcertDescription,
                        ConcertImage = user.ConcertImage,
                        Rating = user.Rating
                    };

                _concertService.CreateNewConcert(neww);
                    
            }
            return status;
        }
    }
}
