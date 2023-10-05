using ApiCamScanner.DTOs;
using ApiCamScanner.MySql;
using Microsoft.AspNetCore.Mvc;

namespace ApiCamScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ExecuteSql _execute;

        public DataController(IConfiguration config)
        {
            _config = config;
            _execute = new ExecuteSql(_config.GetConnectionString("MyConnection"));
        }

        [HttpGet("GetAllData")]
        public IActionResult GetAllData()
        {
            try
            {
                string getAllDataQuery = "SELECT * FROM data";
                var data = _execute.Query<Data>(getAllDataQuery);

                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpPost("CreateData")]
        public IActionResult CreateData([FromBody] Data data)
        {
            try
            {
                string insertDataQuery = "INSERT INTO data (dataValue, datatypeId) VALUES (@DataValue, @DataTypeId)";
                var parameters = new { DataValue = data.DataValue, DataTypeId = data.DataTypeId };
                _execute.Execute(insertDataQuery, parameters);

                return Ok("Data created successfully");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpDelete("Delete{dataId}")]
        public IActionResult DeleteData(int dataId)
        {
            try
            {
                string deleteDataQuery = "DELETE FROM data WHERE dataId = @DataId";
                var parameters = new { DataId = dataId };
                bool isDeleted = _execute.Execute(deleteDataQuery, parameters);

                if (isDeleted)
                    return Ok("Data deleted successfully");
                else
                    return NotFound("Data not found");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpPut("Update{dataId}")]
        public IActionResult UpdateData(int dataId, [FromBody] Data data)
        {
            try
            {
                string updateDataQuery = "UPDATE data SET dataValue = @DataValue, datatypeId = @DataTypeId WHERE dataId = @DataId";
                var parameters = new { DataValue = data.DataValue, DataTypeI = data.DataTypeId, DataId = dataId };
                bool isUpdated = _execute.Execute(updateDataQuery, parameters);

                if (isUpdated)
                    return Ok("Data updated successfully");
                else
                    return NotFound("Data not found");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }
    }
}
