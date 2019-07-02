using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        // GET api/balance
        [HttpGet]
        public ActionResult<double> Get()
        {
            return Parking.GetInstance().Balance;
        }
    }
}