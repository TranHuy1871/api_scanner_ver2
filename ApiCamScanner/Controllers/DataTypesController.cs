using ApiCamScanner.DTOs;
using ApiCamScanner.MySql;
using Microsoft.AspNetCore.Mvc;

namespace ApiCamScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTypesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ExecuteSql _execute;

        public DataTypesController(IConfiguration config)
        {
            _config = config;
            _execute = new ExecuteSql(_config.GetConnectionString("MyConnection"));
        }

        [HttpGet("GetAllDataTypes")]
        public IActionResult GetAllDataTypes()
        {
            try
            {
                var dataTypes = _execute.Query<DataTypes>(StoreQuery.GetAllDataTypesQuery);

                return Ok(dataTypes);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpPost("CreateDataType")]
        public IActionResult CreateDataType([FromBody] DataTypes dataType)
        {
            try
            {
                var parameters = new { DataTypeName = dataType.DataTypeName, DocumentId = dataType.DocumentId };
                _execute.Execute(StoreQuery.InsertDataTypeQuery, parameters);

                return Ok("Data type created successfully");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpDelete("Delete{dataTypeId}")]
        public IActionResult DeleteDataType(int dataTypeId)
        {
            try
            {
                var parameters = new { DataTypeId = dataTypeId };
                bool isDeleted = _execute.Execute(StoreQuery.DeleteDataTypeQuery, parameters);

                if (isDeleted)
                    return Ok("Data type deleted successfully");
                else
                    return NotFound("Data type not found");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpPut("Update{dataTypeId}")]
        public IActionResult UpdateDataType(int dataTypeId, [FromBody] DataTypes dataType)
        {
            try
            {
                var parameters = new { DataTypeName = dataType.DataTypeName, DocumentId = dataType.DocumentId, DataTypeId = dataTypeId };
                bool isUpdated = _execute.Execute(StoreQuery.UpdateDataTypeQuery, parameters);

                if (isUpdated)
                    return Ok("Data type updated successfully");
                else
                    return NotFound("Data type not found");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }
    }
}
