using Microsoft.AspNetCore.Mvc;
using ZabrodskayaWebApi.DAL;
using ZabrodskayaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ZabrodskayaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StandartController : Controller
    {

        private readonly ApplicationContext context;

        public StandartController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getstandartsbytype/{typeId}")]
        public JsonResult GetStandartsByType(int typeId)
        {
            try
            {
                if(typeId < 1)
                {
                    return Json(new { IsSuccess = false, Message = "Такого типа не существует!" });
                }
                var standarts = context.Standarts.Include(s => s.StandartType)
                        .Where(s => s.StandartType.Id == typeId).ToList();

                return new JsonResult(new { IsSuccess = true, Message = "Success", Data = standarts });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetFavoriteStandarts/{userId}")]
        public JsonResult GetFavoriteStandarts(int userId)
        {
            try
            {
                if(userId < 1)
                {
                    return new JsonResult(new { IsSuccess = false, Message = "Такого пользователя не существует!" });
                }

                var standarts = context.XrefUserStandarts.Include(x => x.Standart)
                    .ThenInclude(x => x.StandartType)
                    .Where(x => x.UserId == userId)
                    .Select(x => new FavoriteStandart
                    {
                        StandartTypeName = x.Standart.StandartType.StandartTypeName,
                        StandartHeader = x.Standart.Header,
                        StandartDetails = x.Standart.Details
                    }).ToList();

                return new JsonResult(new { IsSuccess = true, Message = "Success", Data = standarts });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
