using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciQueryableController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public OgrenciQueryableController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public object Get()
        {

            //Burada Web apiye syncfusion grid abilitileri kazandırabilirz.

            //var topluOgrenci = from o in _applicationDbContext.Ogrencis
            //                   join a in _applicationDbContext.Akademisyens on o.DanismanId equals a.Id into akaLeft
            //                   from m in akaLeft.DefaultIfEmpty()
            //                   join 




            //var data = order.AsQueryable();
            //var count = order.Count;
            //var queryString = Request.Query;
            //if (queryString.Keys.Contains("$inlinecount"))
            //{
            //    StringValues Skip;
            //    StringValues Take;
            //    int skip = (queryString.TryGetValue("$skip", out Skip)) ? Convert.ToInt32(Skip[0]) : 0;
            //    int top = (queryString.TryGetValue("$top", out Take)) ? Convert.ToInt32(Take[0]) : data.Count();
            //    return new { Items = data.Skip(skip).Take(top), Count = count };
            //}
            //else
            //{
            //    return data;
            //}
            ////  return new { Items = order.AsQueryable(), Count = order.Count };

            return null;
        }
    }
}
