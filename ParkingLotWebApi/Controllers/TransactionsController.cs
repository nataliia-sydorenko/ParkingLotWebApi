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
    public class TransactionsController : ControllerBase
    {
        // GET api/transactions
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return TransactionsService.ShowAllTranactions();
        }

        // GET api/transactions
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Transaction>> Get(int id)
        {
            return Parking.GetInstance().transactions;
        }
    }
}