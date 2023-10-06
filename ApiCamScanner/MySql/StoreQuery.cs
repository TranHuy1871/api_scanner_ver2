namespace ApiCamScanner.MySql
{
    public class StoreQuery
    {
        //user
        public const string InsertUserQuery       = "INSERT INTO users (username, password, email, phoneNumber) VALUES (@username, @password, @email, @phoneNumber); SELECT LAST_INSERT_ID()";
        public const string CheckUserExists       = "SELECT COUNT(*) FROM users WHERE username = @username OR email = @email";
        public const string CheckAuthenticateUser = "SELECT * FROM users WHERE username = @username AND password = @password";
        public const string UpdatePasswordQuery   = "UPDATE users SET password = @newPassword WHERE username = @username AND password = @currentPassword";

        //document types
        public const string GetAllDocumentTypesQuery = "SELECT * FROM documenttypes";
        public const string InsertDocumentTypeQuery  = "INSERT INTO documenttypes (typeName, userId) VALUES (@TypeName, @UserId)";
        public const string DeleteDocumentTypeQuery  = "DELETE FROM documenttypes WHERE documentTypeId = @DocumentTypeId";
        public const string UpdateDocumentTypeQuery  = "UPDATE documenttypes SET typeName = @TypeName, userId = @UserId WHERE documentTypeId = @DocumentTypeId";

        //document
        public const string GetAllDocumentsQuery = "SELECT * FROM documents";
        public const string InsertDocumentQuery = "INSERT INTO documents (documentName, documentTypeId) VALUES (@DocumentName, @DocumentTypeId)";
        public const string DeleteDocumentQuery = "DELETE FROM documents WHERE documentId = @DocumentId";
        public const string UpdateDocumentQuery = "UPDATE documents SET documentName = @DocumentName, documentTypeId = @DocumentTypeId WHERE documentId = @DocumentId";

        //data types
        public const string GetAllDataTypesQuery = "SELECT * FROM datatypes";
        public const string InsertDataTypeQuery  = "INSERT INTO datatypes (datatypeName, documentId) VALUES (@DataTypeName, @DocumentId)";
        public const string DeleteDataTypeQuery  = "DELETE FROM datatypes WHERE datatypeId = @DataTypeId";
        public const string UpdateDataTypeQuery  = "UPDATE datatypes SET datatypeName = @DataTypeName, documentId = @DocumentId WHERE datatypeId = @DataTypeId";

        //data
        public const string GetAllData = "SELECT * FROM data";
        public const string InsertData = "INSERT INTO data (dataValue, datatypeId) VALUES (@DataValue, @DataTypeId)";
        public const string DeleteData = "DELETE FROM data WHERE dataId = @DataId";
        public const string UpdateData = "UPDATE data SET dataValue = @DataValue, datatypeId = @DataTypeId WHERE dataId = @DataId";
    }

}
