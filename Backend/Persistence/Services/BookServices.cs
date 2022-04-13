using Persistence.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Persistence.Models;
using System.Net.Http;

namespace Persistence.Services
{
    public class BookServices<T> : IServices<T> where T : class
    {
        // This service is for connecting to Books Fake API

        private readonly HttpClient http;
        private readonly string url = "https://fakerestapi.azurewebsites.net/api/v1/Books";

        // BookServices Constructor
        public BookServices()
        {
            http = new HttpClient();
        }

        // Add one Book Async
        public async Task<bool> AddAsync(T Entity)
        {
            var json = JsonConvert.SerializeObject(Entity);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var ServerResponse = await http.PostAsync(url, data);
            var result = ServerResponse.Content.ReadAsStringAsync();

            if (result.IsCompletedSuccessfully)
            {
                return true;
            }

            return false;
        }

        // Delete Book Async
        public async Task<bool> DeleteAsync(int Id)
        {
            var response = await http.DeleteAsync($"{url}/{Id}");
            var result = response.Content.ReadAsStringAsync();

            if (result.IsCompletedSuccessfully)
            {
                return true;
            }

            return false;
        }

        // Get just one Book by its ID Async
        public async Task<T> GetByIdAsync(int Id)
        {
            var response = await http.GetStringAsync($"{url}/{Id}");
            var data = JsonConvert.DeserializeObject<T>(response);

            return data;
        }

        // Get all Books Async
        public async Task<IEnumerable<T>> GetListAsync()
        {
            var response = await http.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<List<T>>(response);

            return data;
        }

        // Update one Book Async
        public async Task<bool> UpdateAsync(int Id, T Entity)
        {
            var json = JsonConvert.SerializeObject(Entity);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var ServerResponse = await http.PutAsync($"{url}/{Id}", data);
            var result = ServerResponse.Content.ReadAsStringAsync();

            if (result.IsCompletedSuccessfully)
            {
                return true;
            }

            return false;
        }
    }
}
