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
        [Route("getstandarttypes")]
        public List<StandartType> GetStandartTypes()
        {
            try
            {
                return context.StandartTypes.ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getstandartsbytype/{typeId}")]
        public List<Standart> GetStandartsByType(int typeId)
        {
            try
            {
                if(typeId < 1)
                {
                    throw new Exception("Такого типа не существует!");
                }
                
                return context.Standarts.Include(s => s.StandartType)
                        .Where(s => s.StandartType.Id == typeId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetFavoriteStandarts/{userId}")]
        public List<FavoriteStandart> GetFavoriteStandarts(int userId)
        {
            try
            {
                if(userId < 1)
                {
                    throw new Exception("Такого пользователя не существует!");
                }

                var standarts = context.XrefUserStandarts.Include(x => x.Standart)
                    .ThenInclude(x => x.StandartType)
                    .Where(x => x.UserId == userId)
                    .Select(x => new FavoriteStandart
                    {
                        StandartId = x.StandartId,
                        StandartTypeName = x.Standart.StandartType.StandartTypeName,
                        StandartHeader = x.Standart.Header,
                        StandartDetails = x.Standart.Details
                    }).ToList();

                return standarts;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllStandarts")]
        public List<Standart> GetStandarts()
        {
            try
            {
                return context.Standarts.ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
