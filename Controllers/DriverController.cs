using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrDriverCore.Models;

namespace MrDriverCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        SQLController sql = new SQLController();

        // GET: api/<DriverController>
        [HttpGet("new")]
        public User CreateDriver()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [driver] (driver, pin) OUTPUT INSERTED.id VALUES ('Driver', '1234')");
            int driverID = sql.ExecuteSQLGetID(cmd);
            return new User(driverID, "Driver", "1234");
        }

        // GET: api/<DriverController>
        [HttpGet("id/{id}")]
        public User GetById(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * from [User] WHERE id = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            DataTable dataTable = sql.SQL2Datatable(cmd);
            if (dataTable.Rows.Count > 0) return sql.Datatable2List<User>(dataTable)[0];
            else return null;
        }

        // GET: api/<DriverController>
        [HttpGet("driver/{driver}")]
        public User GetByName(string driver)
        {
            return sql.GetDriver(driver);
        }
        //// POST api/<DriverController>
        //[HttpPost]
        //public void Post([FromBody] DriverModel driverModel)
        //{
        //    sql.CreateDriver(driverModel);
        //}

        // PUT api/<LocationController>/5
        [HttpPut("{id}&{name}&{pin}")]
        //public void Put(int id, [FromBody] DriverModel driver)
        public int Put(int id, string username, string password)
        {
            User driver = new User(id, username, password);
            return sql.UpdateDriver(driver);
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
