using MovieAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T: MongoDocument
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IMongoDbSettings dbSettings)
        {
            _db = new MongoClient(dbSettings.ConnectionString).GetDatabase(dbSettings.DatabaseName);
            string tableName = typeof(T).Name.ToLower();
            _collection = _db.GetCollection<T>(tableName);
        }
        //удаление элемента из базы данных 
        public void DeleteRecord(Guid id)
        {
            _collection.DeleteOne(doc => doc.Id == id);
        }

        //Получение всех записей из базы данных 
        public List<T> GetAllRecords()
        {
            // BsonDocument, который устанавливает параметры фильтрации. Пустой BsonDocument позволяет выбрать все документы. 
            var records = _collection.Find(new BsonDocument()).ToList();
            return records;
        }
        //возращает один элемент из база по ID
        public T GetRecordById(Guid id)
        {
            var record = _collection.Find(doc => doc.Id == id).FirstOrDefault();
            return record;
        }

        //Вставляет документ в коллекцию.
        public T InsertRecord(T record)
        {
            _collection.InsertOne(record);
            return record;
        }
        //замена существующей записи 
        public void UpsertRecord(T record)
        {
            _collection.ReplaceOne(doc => doc.Id==record.Id, record,
                new ReplaceOptions() { IsUpsert = true });
        }
    }
}
