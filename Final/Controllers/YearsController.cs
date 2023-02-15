using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Final.Hubs;
using Final.Interfaces;
using Final.BusinessObjects;
using System.Threading.Tasks;
using System;
using System.Linq;
using Final.Context;
using Microsoft.AspNetCore.Cors;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class YearsController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly IHubContext<FirstHub, ITypedHubClient> _firstHub;

        public YearsController(ApplicationContext context, IHubContext<FirstHub, ITypedHubClient> firstHub)
        {
            _db = context;
            _firstHub = firstHub;
        }


        [HttpPost]
        [Route("PostYear")]
        public async Task<IActionResult> PostYear(Years model)
        {
            try
            {
                _db.T_Years.Add(model);
                _db.SaveChanges();
                //GetYears();
                var obj = new RealtimeDataHub();
                obj.GetUsers();
                //await _firstHub.Clients.All.SendAsync("LoadProducts");
                return Ok(new { result = "API Running" });
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        public void GetYears()
        {
            var lst = _db.T_Years.ToList();
            _firstHub.Clients.All.BroadcastMessage(lst);
        }
    }
}
