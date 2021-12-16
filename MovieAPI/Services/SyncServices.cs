using MovieAPI.Models;
using MovieAPI.Utilites;
using Microsoft.AspNetCore.Http;
using MovieAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class SyncServices<T> : ISyncServices<T> where T : MongoDocument
    {
        private readonly ISyncServiceSettings _settings;
        private readonly string _origin;


        public SyncServices(ISyncServiceSettings settings, IHttpContextAccessor httpContext)
        {
            _settings = settings;
            //получаю данные с запроса а именно Host того кто отправил
            _origin = httpContext.HttpContext.Request.Host.ToString();
        }
        public HttpResponseMessage Delete(T record)
        {
            //тип синхронизации
            var syncType = _settings.DeleteHttpMethod;
            var json = ToSyncEntityJson(record, syncType);

            var response = HttpClientUtility.SendJson(json, _settings.Host, "POST");
            return response;
        }

        public HttpResponseMessage Upsert(T record)
        {
            var syncType = _settings.UpsertHttpMethod;
            var json = ToSyncEntityJson(record, syncType);
            //post запрос на SyncNode
            var response = HttpClientUtility.SendJson(json, _settings.Host, "POST");
            return response; 
        }


        private string ToSyncEntityJson(T record, string syncType)
        {
            var objectType = typeof(T);

            var syncEntity = new SyncEntity()
            {
                JsonData = JsonSerializer.Serialize(record),
                //тип синхронизации DELETE PUT
                SyncType = syncType,
               
                ObjectType = objectType.Name, //api/movie
                Id = record.Id,
                LastChangeAt = record.LastChangedAt,
                Origin = _origin
            };
           // Преобразует значение указанного типа в строку JSON.
            var json = JsonSerializer.Serialize(syncEntity);
            return json;
        }
    }
}
