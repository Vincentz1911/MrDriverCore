using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MrDriverCore.Models;

namespace MrDriverCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        SQLController sql = new SQLController();

        // GET: api/<LocationController>
        [HttpGet("{id}")]
        public List<Location> Get(int id)
        {
            List<Location> lm = sql.GetLocationList(id);

            //List<LocationDTO> ldto = new List<LocationDTO>();
            //foreach (var location in lm)
            //{
            //    ldto.Add(new LocationDTO()
            //    {
            //        Name = location.Name,
            //        Street = location.Street,
            //        City = location.City,
            //        LatLng = new LatLng(location.Latitude, location.Longitude),
            //        Saved = location.Saved
            //    });
            //}
            return lm;
        }

        //// GET api/<LocationController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<LocationController>
        [HttpPost]
        public void Post([FromBody] Location location)
        {
            sql.CreateLocation(location);
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            sql.DeleteLocation(id);
        }
    }
}
