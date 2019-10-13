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

        const string Url = "http://gasprom.somee.com/api/answer";
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
            string result = "";
            try
            {
                 result = await client.GetStringAsync(Url);
            }
            catch (Exception e) // I would appreciate if i get more specific exception but, 
            {
                //deal with it 
            }
            return JsonConvert.DeserializeObject<IEnumerable<Answer>>(result);
        }

 
        public async Task<UserTest> Add(UserTest friend)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(friend),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<UserTest>(
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

    class UsetTestService
    {

        const string Url = "http://gasprom.somee.com/api/usertestsapi";
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public async Task<IEnumerable<UserTest>> Get()
        {
            HttpClient client = GetClient();
            string result = "";
            try
            {
                result = await client.GetStringAsync(Url);
            }
            catch (Exception e) // I would appreciate if i get more specific exception but, 
            {
                //deal with it 
            }
            return JsonConvert.DeserializeObject<IEnumerable<UserTest>>(result);
        }


        public async Task<UserTest> Add(UserTest friend)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(friend),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<UserTest>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Answer> Update(UserTest friend)
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

        public async Task<UserTest> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<UserTest>(
               await response.Content.ReadAsStringAsync());
        }

    }


}

