using Cloud_System_dev_ops.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cloud_System_dev_ops.Repo
{
  public class LocalHostReSaleService : IReSaleRepositry
    {
         private readonly HttpClient _client;

        public LocalHostReSaleService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:5001");
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        
        public async Task<ReSaleMetaData> SetReSale(ReSaleMetaData reSale)
        {
            string uri = "api/ReSale/EditReSale/";
            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync(uri, reSale);
            string responseContent = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReSaleMetaData>(responseContent);
            
        }
    }       
}
