 

using System.Collections.Generic;

namespace YesSql.Indexes
{
    public class IdentityKeyMap
    {
        private readonly Dictionary<string, object> _documentIds = new Dictionary<string, object>();
        private readonly Dictionary<object, int> _entities = new Dictionary<object, int>();
        private readonly Dictionary<string, Document> _documents = new Dictionary<string, Document>();

        public bool TryGetDocumentId(object item, out int id)
        {
            return _entities.TryGetValue(item, out id);
        }

        public bool TryGetEntityById(string idKey, out object document)
        {
            return _documentIds.TryGetValue(idKey, out document);
        }

        public bool HasEntity(object entity)
        {
            return _entities.ContainsKey(entity);
        }

        public void AddEntity(string idKey, object entity)
        {
            var id = int.Parse(idKey.Split(':')[0]);
            _entities.Add(entity, id);
            _documentIds.Add(idKey, entity);
        }

        public void AddDocument(Document doc,string collection =null)
        {
            _documents[GetIdKey(doc.Id,collection)] = doc;
        } 
        public string GetIdKey(int id, string collection)
        {
            return $"{id}:{collection ?? ""}";
        }
        public bool TryGetDocument(string idKey, out Document doc)
        {
            return _documents.TryGetValue(idKey, out doc);
        }

        public void Remove(string idKey, object entity)
        {
            _entities.Remove(entity);
            _documentIds.Remove(idKey);
            _documents.Remove(idKey);
        }

        public IEnumerable<object> GetAll()
        {
            return _entities.Keys;
        }

        public void Clear()
        {
            _entities.Clear();
            _documentIds.Clear();
            _documents.Clear();
        }
    }
}
