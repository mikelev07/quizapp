using EQuiz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EQuiz.Services
{
    class AnswerService
    {

        const string Url = "https://localhost:44321/api/Answer/";
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

      
        public async Task<IEnumerable<Answer>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Answer>>(result);
        }

 
        public async Task<Answer> Add(Answer friend)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(friend),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Answer>(
                await response.Content.ReadAsStringAsync());
        }
     
        public async Task<Answer> Update(Answer friend)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url + "/" + friend.Id,
                new StringContent(
                    JsonConvert.SerializeObject(friend),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Answer>(
                await response.Content.ReadAsStringAsync());
        }
       
        public async Task<Answer> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Answer>(
               await response.Content.ReadAsStringAsync());
        }

    }
        
     
}

