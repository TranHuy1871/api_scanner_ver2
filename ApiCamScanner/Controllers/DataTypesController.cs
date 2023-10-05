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
                string getAllDataTypesQuery = "SELECT * FROM datatypes";
                var dataTypes = _execute.Query<DataTypes>(getAllDataTypesQuery);

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
                string insertDataTypeQuery = "INSERT INTO datatypes (datatypeName, documentId) VALUES (@DataTypeName, @DocumentId)";
                var parameters = new { DataTypeName = dataType.DataTypeName, DocumentId = dataType.DocumentId };
                _execute.Execute(insertDataTypeQuery, parameters);

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
                string deleteDataTypeQuery = "DELETE FROM datatypes WHERE datatypeId = @DataTypeId";
                var parameters = new { DataTypeId = dataTypeId };
                bool isDeleted = _execute.Execute(deleteDataTypeQuery, parameters);

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
                string updateDataTypeQuery = "UPDATE datatypes SET datatypeName = @DataTypeName, documentId = @DocumentId WHERE datatypeId = @DataTypeId";
                var parameters = new { DataTypeName = dataType.DataTypeName, DocumentId = dataType.DocumentId, DataTypeId = dataTypeId };
                bool isUpdated = _execute.Execute(updateDataTypeQuery, parameters);

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
