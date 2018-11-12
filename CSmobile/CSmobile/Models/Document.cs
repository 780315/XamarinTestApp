using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{

    public class ExtendedProperties
    {
    }

    public class ObjectsAddedToCollectionProperties
    {
    }

    public class ObjectsRemovedFromCollectionProperties
    {
    }

    public class OriginalValues
    {
    }

    public class ChangeTracker
    {
        public string id { get; set; }
        public ExtendedProperties extendedProperties { get; set; }
        public ObjectsAddedToCollectionProperties objectsAddedToCollectionProperties { get; set; }
        public ObjectsRemovedFromCollectionProperties objectsRemovedFromCollectionProperties { get; set; }
        public OriginalValues originalValues { get; set; }
        public int state { get; set; }
    }

    public class DocumentUserGroup
    {
        public string id { get; set; }
        public ChangeTracker changeTracker { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public object document { get; set; }
        public int documentID { get; set; }
        public int Id { get; set; }
        public object userGroup { get; set; }
        public int userGroupID { get; set; }
    }

    public class Document
    {
        public Document(string Document, Array Files, int id, string Type)// finish construtor, where to bind Files?
        {
            this.documentName = Document;
            this.entityID = id;
            this.entityType = Type;
        }

        public string id { get; set; }        
        public IList<object> cashBookItems { get; set; }
        public ChangeTracker changeTracker { get; set; }
        public object changedBy { get; set; }
        public object changedOn { get; set; }
        public string checkSum { get; set; }
        public object connectedDocumentId { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public object data { get; set; }
        public object description { get; set; }
        public IList<object> document1 { get; set; }
        public object document2 { get; set; }
        public int documentChangeVersion { get; set; }
        public object documentChangedBy { get; set; }
        public object documentChangedOn { get; set; }
        public IList<object> documentEntityRelations { get; set; }
        public string documentName { get; set; }
        public string documentNumber { get; set; }
        public IList<object> documentSources { get; set; }
        public int documentVersion { get; set; }
        public IList<DocumentUserGroup> document_UserGroup { get; set; }
        public int entityID { get; set; }
        public string entityType { get; set; }
        public string filePathGuid { get; set; }
        public int Id { get; set; }
        public int Lock { get; set; }
        public object parentID { get; set; }
        public object postBook { get; set; }
        public object refferedDocument { get; set; }
        public object relativePath { get; set; }
        public bool savedAsTemp { get; set; }
        public int size { get; set; }
        public int type { get; set; }
        public int version { get; set; }
        public object _category { get; set; }
        public object _state { get; set; }

    }

    public class Example
    {
        public IList<Document> documents { get; set; }        
        public int total { get; set; }
    }
    

}

