using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
  public  interface ISyncServices<T> where T:MongoDocument
    {
       // Представляет сообщение HTTP-ответа, включая код состояния и данные.
       //отправка http запроса друким репликациям
        HttpResponseMessage Upsert(T record);
        HttpResponseMessage Delete(T record);

    }
}
