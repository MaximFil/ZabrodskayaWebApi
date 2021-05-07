using Microsoft.AspNetCore.Mvc;
using ZabrodskayaWebApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZabrodskayaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly ApplicationContext context;

        public UserController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("LogIn/{login}/{password}")]
        public JsonResult LogIn(string login, string password)
        {
            try
            {
                var user = context.Users.FirstOrDefault(u => string.Equals(u.Login, login) && string.Equals(u.Password, password));
                if(user == null)
                {
                    return new JsonResult(new { IsSuccess = false, Message = "Пользователь с такими данными не существует!" });
                }
                else
                {
                    return new JsonResult(new { IsSuccess = true, Message = "Success", Data = user });
                }
            }
            catch(Exception ex)
            {
                return new JsonResult(new { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("SignUp")]
        public JsonResult SignUp([FromBody]User user)
        {
            try
            {
                if(user == null)
                {
                    return new JsonResult(new { IsSuccess = false, Message = "Невозможно зарегистрироваться!\nПожалуйста, повторите ещё раз!" });
                }
                context.Users.Add(user);
                context.SaveChanges();
                return new JsonResult(new { IsSuccess = true, Message = "Success", Data = user });
            }
            catch(Exception ex)
            {
                return new JsonResult(new { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("AddStandartToFavorites")]
        public JsonResult AddStandartToFavorites([FromBody]XrefUserStandart xrefUserStandart)
        {
            try
            {
                context.XrefUserStandarts.Add(xrefUserStandart);
                context.SaveChanges();
                return new JsonResult(new { IsSuccess = true, Message = "Success" });
            }
            catch(Exception ex)
            {
                return new JsonResult(new { IsSuccess = false, Message = ex.Message });
            }
        }

    }
}
