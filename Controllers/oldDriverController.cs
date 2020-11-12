using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MrDriverCore.Data;
using MrDriverCore.Models;

namespace MrDriverCore.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    public class oldDriverController : ControllerBase
    {
        //private readonly MyDBContext _context;
        //private readonly IConfiguration _config;
        //private readonly IDataProtector _protector;

        //public oldDriverController(
        //    MyDBContext context,
        //    IConfiguration config,
        //    IDataProtectionProvider provider)
        //{
        //    _context = context;
        //    _config = config;
        //    _protector = provider.CreateProtector(_config["CryptoKey"]);
        //}

        //[HttpGet("{id}")]
        //public DriverModel GetById(int id)
        //[HttpPost]
        //public async Task<IActionResult> Login(string username, string password)
        //{
        //    if (username == null || password == null)
        //    {
        //        return NotFound();
        //    }

        //    User user = _context.User.FirstOrDefault(u => u.Username == username);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        if (CryptographyService.Verify(password, user.Password))
        //        {

        //            user = await _context.User.Include(u => u.Locations).FirstOrDefaultAsync(u => u.Id == user.Id);

        //            return Ok(user);
        //        }
        //        else
        //        {
        //            return Unauthorized();
        //        }
        //    }
        //}



        //// GET: api/<DriverController>
        //[HttpGet("new")]
        //public DriverModel CreateDriver()
        //{
        //    SqlCommand cmd = new SqlCommand("INSERT INTO [driver] (driver, pin) OUTPUT INSERTED.id VALUES ('Driver', '1234')");
        //    int driverID = sql.ExecuteSQLGetID(cmd);
        //    return new DriverModel(driverID, "Driver", "1234");
        //}

        //// GET: api/<DriverController>
        //[HttpGet("id/{id}")]
        //public DriverModel GetById(int id)
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT * from [driver] WHERE id = @id");
        //    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
        //    DataTable dataTable = sql.SQL2Datatable(cmd);
        //    if (dataTable.Rows.Count > 0) return sql.Datatable2List<DriverModel>(dataTable)[0];
        //    else return null;
        //}

        //// GET: api/<DriverController>
        //[HttpGet("driver/{driver}")]
        //public DriverModel GetByName(string driver)
        //{
        //    return sql.GetDriver(driver);
        //}
        ////// POST api/<DriverController>
        ////[HttpPost]
        ////public void Post([FromBody] DriverModel driverModel)
        ////{
        ////    sql.CreateDriver(driverModel);
        ////}

        //// PUT api/<LocationController>/5
        //[HttpPut("{id}&{name}&{pin}")]
        ////public void Put(int id, [FromBody] DriverModel driver)
        //public int Put(int id, string name, string pin)
        //{
        //    DriverModel driver = new DriverModel(id, name, pin);
        //    return sql.UpdateDriver(driver);
        //}

        //// DELETE api/<LocationController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
