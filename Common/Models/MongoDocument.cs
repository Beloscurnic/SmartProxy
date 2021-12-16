using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    /// <summary>
    /// Класс MongoDocument 
    /// Данные о запросе Id и Дата
    /// </summary>
 
    public abstract class MongoDocument
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime LastChangedAt { get; set; }
    }
}
