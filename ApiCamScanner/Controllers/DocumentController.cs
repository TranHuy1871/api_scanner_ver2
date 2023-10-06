using ApiCamScanner.DTOs;
using ApiCamScanner.MySql;
using Microsoft.AspNetCore.Mvc;

namespace ApiCamScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ExecuteSql _execute;

        public DocumentsController(IConfiguration config)
        {
            _config = config;
            _execute = new ExecuteSql(_config.GetConnectionString("MyConnection"));
        }

        [HttpGet("GetAllDocuments")]
        public IActionResult GetAllDocuments()
        {
            try
            {
                var documents = _execute.Query<Document>(StoreQuery.GetAllDocumentsQuery);

                return Ok(documents);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpPost("CreateDocument")]
        public IActionResult CreateDocument([FromBody] Document document)
        {
            try
            {
                var parameters = new { DocumentName = document.DocumentName, DocumentTypeId = document.DocumentTypeId };
                _execute.Execute(StoreQuery.InsertDocumentQuery, parameters);

                return Ok("Document created successfully");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpDelete("Delete{documentId}")]
        public IActionResult DeleteDocument(int documentId)
        {
            try
            {
                var parameters = new { DocumentId = documentId };
                bool isDeleted = _execute.Execute(StoreQuery.DeleteDocumentQuery, parameters);

                if (isDeleted)
                    return Ok("Document deleted successfully");
                else
                    return NotFound("Document not found");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpPut("Update{documentId}")]
        public IActionResult UpdateDocument(int documentId, [FromBody] Document document)
        {
            try
            {
                var parameters = new { DocumentName = document.DocumentName, DocumentTypeId = document.DocumentTypeId, DocumentId = documentId };
                bool isUpdated = _execute.Execute(StoreQuery.UpdateDocumentQuery, parameters);

                if (isUpdated)
                    return Ok("Document updated successfully");
                else
                    return NotFound("Document not found");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }
    }
}
