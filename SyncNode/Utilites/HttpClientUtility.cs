using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SyncNode.Utilites
{
 public   class HttpClientUtility
    {
        //Предоставляет класс для отправки HTTP-запросов и получения HTTP-ответов от ресурса, идентифицированного URI.
        private static readonly HttpClient _client = new HttpClient();

        //Представляет сообщение HTTP-ответа, включая код состояния и данные и от кого.
        public static HttpResponseMessage SendJson(string json, string url, string method)
        {
            //    Представляет метод протокола HTTP взависемости от method
            var httpMethod = new HttpMethod(method.ToUpper());
            //Предоставляет HTTP-контент на основе строки. 
            //"application/json" Тип мультимедиа MIME для текста JSON - application/json. Кодировка по умолчанию - UTF-8.
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(httpMethod, url)
            {
                Content = content
            };
            //Отправьте HTTP-запрос как асинхронную операцию.
            var task = _client.SendAsync(request);
            task.Wait();
            return task.Result;
        }

    }
    
}
