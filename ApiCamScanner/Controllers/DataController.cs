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
        private readonly ExecuteSql     _execute;

        public DataController(IConfiguration config)
        {
            _config  = config;
            _execute = new ExecuteSql(_config.GetConnectionString("MyConnection"));
        }

        [HttpGet("GetAllData")]
        public IActionResult GetAllData()
        {
            try
            {
                var data = _execute.Query<Data>(StoreQuery.GetAllData);

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
                var parameters = new { DataValue = data.DataValue, DataTypeId = data.DataTypeId };
                _execute.Execute(StoreQuery.InsertData, parameters);

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
                var parameters = new { DataId = dataId };
                bool isDeleted = _execute.Execute(StoreQuery.DeleteData, parameters);

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
                var parameters = new { DataValue = data.DataValue, DataTypeI = data.DataTypeId, DataId = dataId };
                bool isUpdated = _execute.Execute(StoreQuery.UpdateData, parameters);

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
