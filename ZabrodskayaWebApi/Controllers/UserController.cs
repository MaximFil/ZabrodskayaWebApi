using Microsoft.AspNetCore.Mvc;
using ZabrodskayaWebApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZabrodskayaWebApi.Models;

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
        public ApiResponse LogIn(string login, string password)
        {
            try
            {
                var user = context.Users.FirstOrDefault(u => string.Equals(u.Login, login) && string.Equals(u.Password, password));
                if (user == null)
                {
                    return new ApiResponse(false, "Пользователь с такими данными не существует!");
                }
                else
                {
                    return new ApiResponse(true, "Success", user);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }

        [HttpPost]
        [Route("SignUp")]
        public ApiResponse SignUp([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return new ApiResponse(false, "Невозможно зарегистрироваться!\nПожалуйста, повторите ещё раз!");
                }
                context.Users.Add(user);
                context.SaveChanges();
                return new ApiResponse(true, "Success", user);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsedUserNames")]
        public List<string> GetUsedUserNames()
        {
            try
            {
                return context.Users.Select(u => u.Login).ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("AddStandartToFavorites")]
        public void AddStandartToFavorites([FromBody] XrefUserStandart xrefUserStandart)
        {
            try
            {
                context.XrefUserStandarts.Add(xrefUserStandart);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deletestandartfromfavorite/{standartId}/{userId}")]
        public void DeleteStandartFromFavorite(int userId, int standartId)
        {
            try
            {
                var standart = context.XrefUserStandarts.FirstOrDefault(x => x.StandartId == standartId && x.UserId == userId);
                if(standart == null)
                {
                    return;
                }

                context.XrefUserStandarts.Remove(standart);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
