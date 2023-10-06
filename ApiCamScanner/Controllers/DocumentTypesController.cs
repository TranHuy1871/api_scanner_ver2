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
                var documentTypes = _execute.Query<DocumentTypes>(StoreQuery.GetAllDocumentTypesQuery);

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
                var parameters = new { TypeName = documentType.TypeName, UserId = documentType.UserId };
                _execute.Execute(StoreQuery.InsertDocumentTypeQuery, parameters);

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
                var parameters = new { DocumentTypeId = documentTypeId };
                bool isDeleted = _execute.Execute(StoreQuery.DeleteDocumentTypeQuery, parameters);

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
                var parameters = new { TypeName = documentType.TypeName, UserId = documentType.UserId, DocumentTypeId = documentTypeId };
                bool isUpdated = _execute.Execute(StoreQuery.UpdateDocumentTypeQuery, parameters);

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