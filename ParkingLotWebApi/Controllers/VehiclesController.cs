using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> Get()
        {
            return Parking.GetInstance().vehicles;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Vehicle> Get(string id)
        {
            return Parking.GetInstance().vehicles.Find(v => v.Id == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Vehicle vehicle)
        {
            new VehicleCreator().AddVehicle(vehicle);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] double balance)
        {
            Parking.GetInstance().vehicles.Find(v => v.Id == id).Balance = balance;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            VehicleRemover.TakeVehicle(Parking.GetInstance().vehicles.Find(v => v.Id == id));
        }
    }
}