using ApiCamScanner.DTOs;
using ApiCamScanner.MySql;
using Microsoft.AspNetCore.Mvc;

namespace ApiCamScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ExecuteSql _execute;

        public DocumentTypesController(IConfiguration config)
        {
            _config = config;
            _execute = new ExecuteSql(_config.GetConnectionString("MyConnection"));
        }

        [HttpGet("GetAllDocumentTypes")]
        public IActionResult GetAllDocumentTypes()
        {
            try
            {
                string getAllDocumentTypesQuery = "SELECT * FROM documenttypes";
                var documentTypes = _execute.Query<DocumentTypes>(getAllDocumentTypesQuery);

                return Ok(documentTypes);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpPost("CreateDocumentType")]
        public IActionResult CreateDocumentType([FromBody] DocumentTypes documentType)
        {
            try
            {
                string insertDocumentTypeQuery = "INSERT INTO documenttypes (typeName, userId) VALUES (@TypeName, @UserId)";
                var parameters = new { TypeName = documentType.TypeName, UserId = documentType.UserId };
                _execute.Execute(insertDocumentTypeQuery, parameters);

                return Ok("Document type created successfully");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpDelete("Delete{documentTypeId}")]
        public IActionResult DeleteDocumentType(int documentTypeId)
        {
            try
            {
                string deleteDocumentTypeQuery = "DELETE FROM documenttypes WHERE documentTypeId = @DocumentTypeId";
                var parameters = new { DocumentTypeId = documentTypeId };
                bool isDeleted = _execute.Execute(deleteDocumentTypeQuery, parameters);

                if (isDeleted)
                    return Ok("Document type deleted successfully");
                else
                    return NotFound("Document type not found");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpPut("Update{documentTypeId}")]
        public IActionResult UpdateDocumentType(int documentTypeId, [FromBody] DocumentTypes documentType)
        {
            try
            {
                string updateDocumentTypeQuery = "UPDATE documenttypes SET typeName = @TypeName, userId = @UserId WHERE documentTypeId = @DocumentTypeId";
                var parameters = new { TypeName = documentType.TypeName, UserId = documentType.UserId, DocumentTypeId = documentTypeId };
                bool isUpdated = _execute.Execute(updateDocumentTypeQuery, parameters);

                if (isUpdated)
                    return Ok("Document type updated successfully");
                else
                    return NotFound("Document type not found");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }
    }
}